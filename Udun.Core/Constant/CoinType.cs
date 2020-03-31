using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Constant
{
    public class CoinType
    {
        public CoinType(string unit, int code, string fullName)
        {
            this.code = code;
            this.unit = unit;
            this.fullName = fullName;
        }
        public int code { get; set; }
        public string unit { get; set; }
        public string fullName { get; set; }
    }

    public class CoinOperateBase
    {
        static Dictionary<int, CoinType> _coinDictionary;
        static List<CoinType> coinTypes;
        public static Dictionary<int, CoinType> coinDictionary
        {
            get
            {
                if (_coinDictionary == null)
                {
                    coinTypes = new List<CoinType>();
                    _coinDictionary = new Dictionary<int, CoinType>();
                    coinTypes.Add(new CoinType("BTC", 0, "Bitcoin"));
                    coinTypes.Add(new CoinType("LTC", 2, "Litecoin"));
                    coinTypes.Add(new CoinType("DOGE", 3, "Dogecoin"));
                    coinTypes.Add(new CoinType("DSH", 5, "Dash (ex Darkcoin)"));
                    coinTypes.Add(new CoinType("ETH", 60, "Ether"));
                    coinTypes.Add(new CoinType("ETC", 61, "Ether Classic"));
                    coinTypes.Add(new CoinType("STO", 99, "SaveTheOcean"));
                    coinTypes.Add(new CoinType("ZEC", 133, "Zcash"));
                    coinTypes.Add(new CoinType("XRP", 144, "Ripple"));
                    coinTypes.Add(new CoinType("BCH", 145, "Bitcoin Cash"));
                    coinTypes.Add(new CoinType("EOS", 194, "EOS"));
                    coinTypes.Add(new CoinType("TRX", 195, "Tron"));
                    coinTypes.Add(new CoinType("TEC", 206, "TEC"));
                    coinTypes.Add(new CoinType("XNE", 208, "XNE"));
                    coinTypes.Add(new CoinType("GCA", 500, "GCA"));
                    coinTypes.Add(new CoinType("GCB", 501, "GCB"));
                    coinTypes.Add(new CoinType("GCC", 502, "GCC"));
                    coinTypes.Add(new CoinType("NBTC", 503, "NBTC"));
                    coinTypes.Add(new CoinType("TECO", 506, "TECO"));
                    coinTypes.Add(new CoinType("GX", 508, "GX"));
                    coinTypes.Add(new CoinType("CNYT", 509, "CNYT"));
                    coinTypes.Add(new CoinType("CNT", 520, "CNT"));
                    coinTypes.Add(new CoinType("CNY", 2501, "CNY"));
                    coinTypes.Add(new CoinType("CND", 2502, "CND"));
                    coinTypes.Add(new CoinType("USDD", 2503, "USDD"));
                    coinTypes.Add(new CoinType("QTUM", 2301, "QTUM"));
                    coinTypes.Add(new CoinType("VDS", 2303, "VDS"));
                    coinTypes.Add(new CoinType("IOTE", 2304, "IOTE"));
                    coinTypes.Add(new CoinType("LEO", 2504, "LEO"));

                    coinTypes.ForEach(p =>
                    {
                        _coinDictionary.Add(p.code, p);
                    });
                }
                return _coinDictionary;
            }
        }
        public static CoinType CodeOf(int code)
        {
            if (coinDictionary.ContainsKey(code))
                return coinDictionary[code];
            else
                return null;
        }
    }
}
