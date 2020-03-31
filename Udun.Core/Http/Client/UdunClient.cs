using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Udun.Core.Constant;
using Udun.Core.Entity;
using Udun.Core.Http;
using Udun.Core.Http.Base;
using Udun.Core.Http.Client;
using Udun.Core.Utils;

namespace Udun.Core.Http.Client
{
    public class RequestConfig
    {
        public int connectTimeout { get; set; }
        public int requestTimeout { get; set; }
        public bool redirectEnabled { get; set; }
    }
    public class UdunClient
    {
        private string gateway;
        public log4net.ILog logger;

        public string GetMerchantKey()
        {
            return merchantKey;
        }
        private string merchantId;
        private string merchantKey;

        public UdunClient()
        {
            this.logger = log4net.LogManager.GetLogger(this.GetType());
        }

        public UdunClient(string gateway, string merchantId, string key)
        {
            this.logger = log4net.LogManager.GetLogger(this.GetType());
            this.gateway = gateway;
            this.merchantId = merchantId;
            this.merchantKey = key;
        }

        public ResponseMessage<T> OperateResult<T, T1>(T1 requestParameters, string operateUrl)
        {
            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestParameters);
                body = "[" + body + "]";
                string sendStr = Newtonsoft.Json.JsonConvert.SerializeObject(OperateUtil.WrapperParams(this.merchantKey, body));
                string host = this.gateway.EndsWith("/") ? this.gateway : (this.gateway + "/");
                string textReg = HttpProvider.HttpPostRaw(host + operateUrl, sendStr);
                ResponseMessage<T> result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMessage<T>>(textReg);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ResponseMessage<T> OperateResult<T>(Dictionary<string, object> requestParameters, string operateUrl, bool needArry = true)
        {
            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestParameters);
                if (needArry)
                    body = "[" + body + "]";
                string sendStr = Newtonsoft.Json.JsonConvert.SerializeObject(OperateUtil.WrapperParams(this.merchantKey, body));
                string host = this.gateway.EndsWith("/") ? this.gateway : (this.gateway + "/");
                string textReg = HttpProvider.HttpPostRaw(host + operateUrl, sendStr);
                ResponseMessage<T> result = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMessage<T>>(textReg);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /**
         * 创建地址
         * @param mainCoinType
         * @param callbackUrl
         * @return
         * @throws Exception
         */
        public ResponseMessage<Address> CreateCoinAddress(string mainCoinType, string callbackUrl, string alias, string walletId)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("merchantId", this.merchantId);
            requestParameters.Add("coinType", mainCoinType);
            requestParameters.Add("callUrl", callbackUrl);
            if (alias != null && alias != "")
            {
                requestParameters.Add("alias", alias);
            }
            if (walletId != null && walletId != "")
            {
                requestParameters.Add("walletId", walletId);
            }
            return OperateResult<Address>(requestParameters, API.CREATE_ADDRESS);
        }

        /**
        * 创建批量地址(弃用)
        * @param mainCoinType
        * @param callbackUrl
        * @param count
        * @return
        * @throws Exception
        */
        public ResponseMessage<List<Address>> CreateBatchCoinAddress(string mainCoinType, string callbackUrl, int count)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("merchantId", this.merchantId);
            requestParameters.Add("coinType", mainCoinType);
            requestParameters.Add("callUrl", callbackUrl);
            requestParameters.Add("count", count);
            return OperateResult<List<Address>>(requestParameters, API.CREATE_BATCH_ADDRESS);
        }


        /**
     * 转账
     * @param orderId
     * @param amount ----- string 类型，标准单位 如 转 0.1 BTC， 为 0.10000000
     * @param mainCoinType
     * @param subCoinType
     * @param address
     * @param callbackUrl
     * @return
     * @throws Exception
     */
        public ResponseMessage<string> TransferAmt(string orderId, string amount, string mainCoinType, string subCoinType, string address, string callbackUrl, string memo)
        {
            Transfer transfer = new Transfer();
            transfer.address = address;
            transfer.mainCoinType = mainCoinType;
            transfer.coinType = subCoinType;
            transfer.businessId = orderId;
            transfer.merchantId = this.merchantId;
            transfer.amount = amount;
            transfer.callUrl = callbackUrl;
            transfer.memo = memo;
            return OperateResult<string, Transfer>(transfer, API.WITHDRAW);
        }

        /// <summary>
        /// 模拟回调
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string CallBack(Trade trade, string callBackUrl, bool error)
        {
            return OperateCallBaceResult(trade, callBackUrl, error);
        }

        /**
 * 代付
 * @param orderId
 * @param amount
 * @param mainCoinType
 * @param subCoinType
 * @param address
 * @param callbackUrl
 * @return
 * @throws Exception
 */
        public ResponseMessage<string> AutoTransfer(string orderId, string amount, string mainCoinType, string subCoinType, string address, string callbackUrl, string memo)
        {
            Transfer transfer = new Transfer();
            transfer.address = address;
            transfer.mainCoinType = mainCoinType;
            transfer.coinType = subCoinType;
            transfer.businessId = orderId;
            transfer.merchantId = this.merchantId;
            transfer.amount = amount;
            transfer.callUrl = callbackUrl;
            transfer.memo = memo;
            return OperateResult<string, Transfer>(transfer, API.AUTO_WITHDRAW);
        }

        public string OperateCallBaceResult(object requestParameters, string operateUrl, bool error)
        {
            try
            {
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(requestParameters);
                string timestamp = OperateUtil.GetTimeStamp().ToString();
                string nonce = OperateUtil.GetNonceString(8);
                String sign = SignUtil.sign(this.merchantKey, timestamp, nonce, body);
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (error)
                    sign = "12312312qwed";
                map.Add("body", body);
                map.Add("sign", sign);
                map.Add("timestamp", timestamp);
                map.Add("nonce", nonce);

                Dictionary<string, string> Spaheader = new Dictionary<string, string>();
                Dictionary<string, string> myParameters = new Dictionary<string, string>();
                HttpProvider httpProvider = new HttpProvider();

                HttpResponseParameter reponseParameter = httpProvider.Excute(new HttpRequestParameter
                {
                    Url = operateUrl,
                    IsPost = true,
                    Encoding = Encoding.UTF8,
                    Parameters = map,
                    HeaderParameters = map,
                }, "", "");

                string textReg = reponseParameter.Body;
                return textReg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /**
     * 检查是否内部地址
     * @param address
     * @return
     * @throws Exception
     */
        public bool CheckAddress(string mainCoinType, string address)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("merchantId", this.merchantId);
            requestParameters.Add("address", address);
            requestParameters.Add("mainCoinType", mainCoinType);
            if (OperateResult<string>(requestParameters, API.CHECK_ADDRESS).code == 200)
                return true;
            else
                return false;
        }

        /**
        * 获取支持的币种和对应余额
        * @return
        * @throws Exception
        */
        public List<SupportCoin> GetSupportCoin(bool showBalance)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("merchantId", this.merchantId);
            requestParameters.Add("showBalance", showBalance);
            ResponseMessage<List<SupportCoin>> response = OperateResult<List<SupportCoin>>(requestParameters, API.SUPPORT_COIN, false);
            return response.data;
        }

        /**
        * 检查是否支持该币种
        * @param coinName
        * @return
        * @throws Exception
        */
        public bool CheckSupport(string coinName)
        {
            bool supported = false;
            List<SupportCoin> supportCoinList = GetSupportCoin(false);
            foreach (SupportCoin supportCoin in supportCoinList)
            {
                if (supportCoin.name == coinName)
                {
                    supported = true;
                    break;
                }
            }
            return supported;
        }

        /**
 * 是否支持代付条件
 * @param amount
 * @param mainCoinType
 * @param subCoinType
 * @return
 * @throws Exception
 */
        public bool CheckProxy(string amount, string mainCoinType, string subCoinType)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("merchantId", this.merchantId);
            requestParameters.Add("mainCoinType", mainCoinType);
            requestParameters.Add("subCoinType", subCoinType);
            requestParameters.Add("coinType", subCoinType);//兼容处理下
            requestParameters.Add("amount", amount);
            ResponseMessage<string> response = OperateResult<string>(requestParameters, API.CHECK_PROXY);
            return Convert.ToBoolean(response.data);
        }


        /**
 * 查询交易记录
 * @param mainCoinType
 * @param coinType
 * @param tradeId
 * @param tradeType
 * @param address
 * @param startTimestamp 时间戳 毫秒
 * @param endTimestamp 时间戳 毫秒
 * @return
 * @throws Exception
 */
        public List<Transaction> QueryTransaction(string mainCoinType, string coinType, string tradeId,
                                                  int tradeType, string address, string startTimestamp, string endTimestamp)
        {
            Dictionary<string, object> requestParameters = new Dictionary<string, object>();
            requestParameters.Add("mainCoinType", mainCoinType);
            requestParameters.Add("coinType", coinType);
            requestParameters.Add("tradeId", tradeId);
            requestParameters.Add("tradeType", tradeType);
            requestParameters.Add("address", address);
            requestParameters.Add("startTimestamp", startTimestamp);
            requestParameters.Add("endTimestamp", endTimestamp);
            ResponseMessage<List<Transaction>> response = OperateResult<List<Transaction>>(requestParameters, API.TRANSACTION);
            List<Transaction> trades = response.data;
            return trades;
        }

    }
}

