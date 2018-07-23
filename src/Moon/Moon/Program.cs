using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moon.Data.Provider;
using Binance.Net.Objects;

namespace Moon
{
    class Program
    {
        static void Main(string[] args)
        {
            Core IncomingBinance = new Core();
            IncomingBinance.SubscribeTo("ETHBTC");
            //IncomingBinance.SubscribeTo("BTCUSDT");
            //IncomingBinance.SubscribeTo("BTCXLM");

            //var tick = IncomingBinance.bclient.Socket.SubscribeToKlineStreamAsync("ETHBTC", KlineInterval.OneMinute, (data) =>
            //{
            //    Console.WriteLine("Data Open : {0}", data.Data.Open);
            //});
            while (true)
            {
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
