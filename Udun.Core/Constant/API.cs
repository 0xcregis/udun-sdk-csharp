using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Constant
{
    public class API
    {
        public static string CREATE_ADDRESS = "mch/address/create";
        public static string WITHDRAW = "mch/withdraw";
        public static string TRANSACTION = "mch/transaction";
        public static string AUTO_WITHDRAW = "mch/withdraw/proxypay";
        public static string SUPPORT_COIN = "mch/support-coins";
        public static string CHECK_PROXY = "mch/check-proxy";
        public static string CHECK_ADDRESS = "mch/check/address";
        public static string CREATE_BATCH_ADDRESS = "mch/address/create/batch";
    }
}
