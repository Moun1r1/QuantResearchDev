using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public interface Pair
    {
        string Name { get; set; }
        string Exchanger { get; set; }
        DateTime CollectedDate { get; set; }
        Dictionary<string, dynamic> Properties { get; set; }
        Trady.Core.Candle Candle { get;set; }
    }
    public class BinanceCandle : Pair
    {
        
        public BinanceCandle()
        {
          
        }
        public Trady.Core.Candle Candle { get; set; }
        public FactorDefintion Factor { get; set; }
        public string Name { get; set; }
        public string Exchanger { get; set; } = "Binance";
        public DateTime CollectedDate { get; set; } = DateTime.Now;
        public Dictionary<string, dynamic> Properties { get; set; } = new Dictionary<string, dynamic>();
    }
}
