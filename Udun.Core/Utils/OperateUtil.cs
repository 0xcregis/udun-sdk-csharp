using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Utils
{
    public class OperateUtil
    {
        /// <summary>
        /// 获取当前毫秒时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        public static Dictionary<string, string> WrapperParams(string key, string body)
        {
            string timestamp = GetTimeStamp().ToString();
            string nonce = GetNonceString(8);
            String sign = SignUtil.sign(key, timestamp, nonce, body);
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("body", body);
            map.Add("sign", sign);
            map.Add("timestamp", timestamp);
            map.Add("nonce", nonce);
            return map;
        }

        //static Dictionary<int, string> _numberDictionary;
        //static Dictionary<int, string> numberDictionary
        //{
        //    get
        //    {
        //        if (_numberDictionary == null)
        //        {
        //            _numberDictionary = new Dictionary<int, string>();
        //            _numberDictionary.Add(1, "1");
        //            _numberDictionary.Add(2, "2");
        //            _numberDictionary.Add(3, "3");
        //            _numberDictionary.Add(4, "4");
        //            _numberDictionary.Add(5, "5");
        //            _numberDictionary.Add(6, "6");
        //            _numberDictionary.Add(7, "7");
        //            _numberDictionary.Add(8, "8");
        //            _numberDictionary.Add(9, "9");
        //            _numberDictionary.Add(0, "0");
        //        }
        //        return _numberDictionary;
        //    }
        //}

        static Random random = new Random(10);
        public static string GetNonceString(int len)
        {
            string tmp = "";
            for (int i = 0; i < len; i++)
            {
                int seed = random.Next(0, 10);
                tmp += seed.ToString();
            }
            return tmp;
        }

        public static bool CheckSign(string key, string timestamp, string nonce, string body, string sign)
        {
            string checkSign = SignUtil.sign(key, timestamp, nonce, body);
            return checkSign == sign;
        }
    }
}
