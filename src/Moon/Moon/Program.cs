using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moon.Data.Provider;
using Binance.Net.Objects;
using System.Windows.Forms;
using Moon.Visualizer;
using CoinMarketCap;
using CoinMarketCap.Core;
using Moon.MarketWatcher;
using System.IO;
using Newtonsoft.Json;
using Moon.Config;

namespace Moon
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            try
            {
                using (StreamReader r = new StreamReader(@".\Config\Config.json"))
                {
                    string json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject<ConfigGlobal>(json);
                    Moon.Global.shared.Config = items;
                }
            }
            catch
            {
               throw new Exception("Config file should placed on a folder named Config with the config.json on the same bin path..");

            }



            //Core IncomingBinance = new Core();


            ////IncomingBinance.SubscribeTo("ETHBTC");
            Application.EnableVisualStyles();
            Application.Run(new Chart()); // or whatever

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
