using Moon.Config;
using Moon.Data.Extender;
using Moon.Data.Model;
using Moon.Finance.Indicators;
using Moon.Strategy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

            var LoadedTypes = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var loaded in LoadedTypes)
            {
                if(!loaded.IsInterface)
                {
                    //Console.WriteLine("Type : {0} - Base Type : {1}", loaded.Name,loaded.BaseType.Name);
                }
            }
            List<Type> GetInternalByType(Type input)
            {
                var found = from type in Assembly.GetExecutingAssembly().GetTypes()
                            where input.IsAssignableFrom(type) && type != input
                            select type;
                return found.ToList();
            }
            var IrootType = GetInternalByType(typeof(IRoot));
            var IExchanger = GetInternalByType(typeof(IExchanger));
            var IPair = GetInternalByType(typeof(IPair));
            var IStrategy = GetInternalByType(typeof(IStrategy));
            var ICandleSeries = GetInternalByType(typeof(ICandleFactory));
            //foreach (var sub in IStrategy)
            //{
            //    Console.WriteLine("Loaded strategy : {0}", sub.Name);
            //    var Typefofstrat = (DefaultStrategyConf) Attribute.GetCustomAttribute(sub, typeof(DefaultStrategyConf));
            //    var TimeRange = (StratTimeRangeOptionnal)Attribute.GetCustomAttribute(sub, typeof(StratTimeRangeOptionnal));
            //    var tradetype = (TradingLogicAttribute)Attribute.GetCustomAttribute(sub, typeof(TradingLogicAttribute));
            //    Console.WriteLine("\tType : {0}", Typefofstrat.StratyType);
            //    Console.WriteLine("\tExchanger  : {0}", Typefofstrat.Exchanger);
            //    Console.WriteLine("\tName  : {0}", Typefofstrat.Name);
            //    Console.WriteLine("\tAutoStart  : {0}", Typefofstrat.AutoStart);
            //    Console.WriteLine("\tRevision  : {0}", Typefofstrat.Revision);
            //    Console.WriteLine("\tSignalFrom  : {0}", Typefofstrat.SignalFrom);
            //    Console.WriteLine("\tTrading Type : {0}", tradetype.RunKind);
            //    Console.WriteLine("\tTimeRange : {0}", TimeRange.PeriodNeed);
            //    var CreateStrategyFromAssembly = Activator.CreateInstance(sub);

            //    //put parameter class from reflexion here

            //}
            //Configure Api Key for svc management

            //Global.shared.IncomingBinance.bclient.SetApiKeys("API Key", "Api Secret");
            CandlesSeries seriehandle = new CandlesSeries();
            TradesSeries tradehandle = new TradesSeries();
            seriehandle.ConnectBinance(Global.Shared.IncomingBinance);
            tradehandle.ConnectBinance(Global.Shared.IncomingBinance);
            Global.Shared.IncomingBinance.GetDataFromTo(DateTime.Now.AddHours(-15),DateTime.Now, "ETHBTC");
            Global.Shared.IncomingBinance.SubscribeTo("ETHBTC");




            //seriehandle.CandleUpdate += Seriehandle_CandleUpdate;
            RSI test = new RSI(seriehandle);

            MyStrategyTest strat = new MyStrategyTest(seriehandle, tradehandle);

            //Application.EnableVisualStyles();
            //Application.Run(new Chart()); // or whatever




            Console.ReadKey();

            //while (true)
            //{
            //    System.Threading.Thread.Sleep(500);
            //}
        }

        private static void Seriehandle_CandleUpdate(object sender, CandleEventArg e)
        {
            Console.WriteLine("Candle Series - Index : {0}", e.Candlescopy.Index);
            Console.WriteLine("\tRSICandle Series - IndexHighestHigh : {0}", e.Candlescopy.IndexHighestHigh);
            Console.WriteLine("\tRSICandle Series - IndexLowestLow : {0}", e.Candlescopy.IndexLowestLow);
            Console.WriteLine("\tCandle Series - Low : {0}", e.Candlescopy.LowestLow[e.Candlescopy.IndexHighestHigh]);
            Console.WriteLine("\tCandle Series - High : {0}", e.Candlescopy.HighestHigh[e.Candlescopy.IndexLowestLow]);
            //Console.WriteLine("\tCandle Series - Last Patterns : {0}", e.Candlescopy.DetectedPatterns.Last().ToJson());
        }
    }
}
