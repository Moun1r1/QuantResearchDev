using Binance.Net;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Exchanger
{
    public class binance:Root
    {
        public BinanceClient Client { get; set; } = new Binance.Net.BinanceClient();
        public BinanceSocketClient Socket { get; set; } = new BinanceSocketClient();
        public string TypeOfData { get; set; } = "BinanceConfig";
        public string Jscontainer { get; set; }

        public binance()
        {
            

        }

        public void Update()
        {
            this.Jscontainer = Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
