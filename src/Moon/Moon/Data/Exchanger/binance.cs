using Binance.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Exchanger
{
    public class binance
    {
        public BinanceClient Client { get; set; } = new Binance.Net.BinanceClient();
        public BinanceSocketClient Socket { get; set; } = new BinanceSocketClient();
        public binance()
        {
            

        }
    }
}
