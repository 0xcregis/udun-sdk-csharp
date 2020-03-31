using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Entity
{
    public class Transfer
    {

        //商户号
        public string merchantId { get; set; }
        //转账地址
        public string address { get; set; }
        //币种类型
        public string mainCoinType { get; set; }
        //子币种类型
        public string coinType { get; set; }
        //转账数量
        public string amount { get; set; }
        //转账回调地址
        public string callUrl { get; set; }
        //提币申请单号
        public string businessId { get; set; }
        //备注
        public string memo { get; set; }
    }
}
