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
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Serilog;
using Bitmex.Client.Websocket.Requests;
using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket;
using Bitmex.Client.Websocket.Websockets;
using Moon.Data.Extender;

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
                    Moon.Global.Shared.Config = items;
                }
            }
            catch
            {
               //throw new Exception("Config file should placed on a folder named Config with the config.json on the same bin path..");

            }


            try
            {
                //Get-Azure DataTable 
                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://127.0.0.1;");
                //CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                //Moon.Global.shared.table = tableClient.GetTableReference("kline");

                // Create the table if it doesn't exist.
                //Moon.Global.shared.table.CreateIfNotExists();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Not Using Azure Data Table due to error : {0}", ex.Message);
            }


            //Configure Api Key for svc management

            //Global.shared.IncomingBinance.bclient.SetApiKeys("API Key", "Api Secret");
            Global.Shared.IncomingBinance.SubscribeTo("BTCUSDT");
            CandlesSeries seriehandle = new CandlesSeries();
            seriehandle.ConnectBinance(Global.Shared.IncomingBinance);
            //Application.EnableVisualStyles();
            //Application.Run(new Chart()); // or whatever




            Console.ReadKey();

            //while (true)
            //{
            //    System.Threading.Thread.Sleep(500);
            //}
        }
    }
}
