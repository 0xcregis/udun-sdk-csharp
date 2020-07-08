using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Udun.Core.Entity;
using Udun.Core.Http.Client;
using Udun.Core.Utils;

namespace Udun.Api.Controller
{
    /// <summary>
    /// CallbackController 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://udunapi.io/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class CallbackController : System.Web.Services.WebService
    {
        string _callBackUrl;
        string callBackUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_callBackUrl))
                    _callBackUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["CallBackUrl"].ToString();
                //_callBackUrl = System.Configuration.ConfigurationManager.AppSettings["CallBackUrl"];
                return _callBackUrl;
            }
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        UdunClient _udunClient;
        UdunClient udunClient
        {
            get
            {
                if (_udunClient == null)
                {
                    string gateway = System.Web.Configuration.WebConfigurationManager.AppSettings["Gateway"].ToString();
                    string merchantId = System.Web.Configuration.WebConfigurationManager.AppSettings["MerchantId"].ToString();
                    string merchantKey = System.Web.Configuration.WebConfigurationManager.AppSettings["MerchantKey"].ToString();
                    _udunClient = new UdunClient(gateway, merchantId, merchantKey);
                }
                return _udunClient;
            }
        }

        log4net.ILog _logger;
        log4net.ILog logger
        {
            get
            {
                if (_logger == null)
                    _logger = log4net.LogManager.GetLogger(this.GetType());
                return _logger;
            }
        }

        /**
         * 处理币付网关回调信息，包括充币（不管业务方返回的是什么值，这里是error和success，只要回调通信成功，优盾都将会认为回调成功）
         * @param timestamp
         * @param nonce
         * @param body
         * @param sign
         * @return
         * @throws Exception
         */
        [WebMethod]
        public string TradeCallback(string timestamp, string nonce, string body, string sign)
        {
            logger.InfoFormat("timestamp:{0},nonce:{1},sign:{},body:{2}", timestamp, nonce, sign, body);
            if (!OperateUtil.CheckSign(udunClient.GetMerchantKey(), timestamp, nonce, body, sign))
            {
                return "error";
            }
            Trade trade = Newtonsoft.Json.JsonConvert.DeserializeObject<Trade>(body);
            logger.InfoFormat("trade:{0}", trade);
            //TODO 业务处理
            if (trade.tradeType == 1)
            {
                logger.Info("=====收到充币通知======");
                logger.InfoFormat("address:{0},amount:{1},mainCoinType:{2},fee:{3}", trade.address, trade.amount, trade.mainCoinType, trade.fee);
                //金额为最小单位，需要转换,包括amount和fee字段
                BigDecimal amount = new BigDecimal(trade.amount).divide(BigDecimal.TEN.pow(trade.decimals), 8, RoundingMode.DOWN);
                BigDecimal fee = new BigDecimal(trade.fee).divide(BigDecimal.TEN.pow(trade.decimals), 8, RoundingMode.DOWN);
                logger.InfoFormat("amount={0},fee={1}", amount.toPlainString(), fee.toPlainString());
            }
            else if (trade.tradeType == 2)
            {
                logger.Info("=====收到提币处理通知=====");
                logger.InfoFormat("address:{0},amount:{1},mainCoinType:{2},businessId:{3}", trade.address, trade.amount, trade.mainCoinType, trade.businessId);
                if (trade.status == 1)
                {
                    logger.Info("审核通过，转账中");
                    //TODO: 提币交易已发出，理提币订单状态，扣除提币资金
                }
                else if (trade.status == 2)
                {
                    logger.Info("审核不通过");
                    //TODO: 处理提币订单状态，订单号为 businessId
                }
                else if (trade.status == 3)
                {
                    logger.Info("提币已到账");
                    //TODO: 提币已到账，可以向提币用户发出通知
                }
            }
            return "success";
        }
    }
}
