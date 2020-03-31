using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Entity
{
    public class Trade
    {
        //交易Id
        public string txId { get; set; }
        //交易流水号
        public string tradeId { get; set; }
        //交易地址
        public string address { get; set; }
        //币种类型
        public string mainCoinType { get; set; }
        //代币类型，erc20为合约地址
        public string coinType { get; set; }
        //交易金额
        public string amount { get; set; }
        //交易类型  1-充值 2-提款(转账)
        public int tradeType { get; set; }
        //交易状态 0-待审核 1-成功 2-失败,充值无审核
        public int status { get; set; }
        //旷工费
        public string fee { get; set; }
        public int decimals { get; set; }
        //提币申请单号
        public string businessId { get; set; }
        //备注
        public string memo { get; set; }

        public bool isErcToken()
        {
            return mainCoinType != coinType &&
                    this.mainCoinType == "60";
        }

        public bool isUsdt()
        {
            return this.mainCoinType == "0"
                    && this.coinType == "31";
        }

        public bool isTrcToken()
        {
            return mainCoinType != coinType &&
                    this.mainCoinType == "195";
        }
    }
}
