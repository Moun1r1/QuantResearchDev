using Binance.Net.Objects;
using Microsoft.WindowsAzure.Storage.Table;
using Moon.Data.Bacher;
using Moon.Data.Exchanger;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Moon.Data.Provider
{
    public enum ProviderMode
    {
        CandleOnly,
        AllTicks,
        TicksAndTrades,
        All
    }
    public class Core: Root
    {

        public ObservableCollection<BinanceStreamKlineData> BData { get; set; } = new ObservableCollection<BinanceStreamKlineData>();
        public ObservableCollection<BinanceStreamTick[]> BAllPairsData { get; set; } = new ObservableCollection<BinanceStreamTick[]>();
        public ObservableCollection<BinanceStreamOrderBook> BBookData { get; set; } = new ObservableCollection<BinanceStreamOrderBook>();
        public Grouper DataOrganizer { get; set; } = new Grouper();
        public ObservableCollection<BinanceStreamTrade> BDataTradeSeller { get; set; } = new ObservableCollection<BinanceStreamTrade>();
        public ObservableCollection<BinanceStreamTrade> BDataTradeBuyer { get; set; } = new ObservableCollection<BinanceStreamTrade>();
        public ObservableCollection<BinanceCandle> CandlesTable { get; set; } = new ObservableCollection<BinanceCandle>();
        public WebSocket Sender { get; set; }
        public bool UseSender { get; set; } = false;
        public ObservableCollection<BinanceCandle> Candles { get; set; } = new ObservableCollection<BinanceCandle>();
        public binance bclient { get; set; } = new binance();
        public List<BinanceCandle> GenericCandle = new List<BinanceCandle>();
        public ProviderMode Mode { get; set; } = ProviderMode.All;
        public string Jscontainer { get; set; }

        public Core()
        {
            if(UseSender)
            {
                this.Sender = new WebSocket(string.Format("ws://localhost:1345/{0}", Moon.Global.shared.ConfigUri.CandleMarketPath));
                this.Sender.Connect();

            }
            if (Global.shared.table != null)
            {
                LoadAlldata();
            }
            BData.CollectionChanged += BData_CollectionChanged;
            Candles.CollectionChanged += Candles_CollectionChanged;
            BDataTradeSeller.CollectionChanged += BDataTrade_CollectionChanged;


        }
            private void BDataTrade_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
        }

        /// <summary>
        /// Reinject all patterns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Candles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && Candles.Count() > 2)
            {

               // will be migrated to TA Node

                //var RawData = Candles.Select(y => y.Candle).ToList();
                //var indexdcandles = new IndexedCandle(RawData, RawData.Count() - 1);
                //var LastBinanceCandle = (BinanceCandle)e.NewItems[0];
                //LastBinanceCandle.Properties.Add("Bearish",indexdcandles.IsBearish());
                //LastBinanceCandle.Properties.Add("IsBullish", indexdcandles.IsBullish());
                //LastBinanceCandle.Properties.Add("IsAccumDistBearish", indexdcandles.IsAccumDistBearish());
                //LastBinanceCandle.Properties.Add("IsAccumDistBullish", indexdcandles.IsAccumDistBullish());
                //LastBinanceCandle.Properties.Add("ClosePricePercentageChange", indexdcandles.ClosePricePercentageChange());
                //LastBinanceCandle.Properties.Add("ClosePriceChange", indexdcandles.ClosePriceChange());
                //LastBinanceCandle.Properties.Add("IsBreakingHistoricalHighestClose", indexdcandles.IsBreakingHistoricalHighestClose());
                //LastBinanceCandle.Properties.Add("IsBreakingHistoricalHighestHigh", indexdcandles.IsBreakingHistoricalHighestHigh());
                //LastBinanceCandle.Properties.Add("IsBreakingHistoricalLowestLow", indexdcandles.IsBreakingHistoricalLowestLow());
                //LastBinanceCandle.Properties.Add("IsObvBearish", indexdcandles.IsObvBearish());
                //LastBinanceCandle.Properties.Add("IsObvBullish", indexdcandles.IsObvBullish());
            }


        }

        private void LoadAlldata()
        {
            try
            {
                if(Global.shared.table != null)
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            TableQuery<BinanceCandle> query = new TableQuery<BinanceCandle>();

                            var content = Global.shared.table.ExecuteQuery(query);
                            if (content.Count() > 0)
                            {
                                foreach (var oldcandles in content)
                                {
                                    var olddata = Newtonsoft.Json.JsonConvert.DeserializeObject<BinanceCandle>(oldcandles.ConcatainedData);
                                    CandlesTable.Add(olddata);
                                    Console.WriteLine("Loaded : {0} on History Table", CandlesTable.Count());
                                }

                            }

                        }
                        catch (Exception ex)
                        {

                        }

                    });

                }
            }
            catch
            {

            }
        }

        public void RegisterAllMarket()
        {
            Task.Run(() =>
            {
                var tick = this.bclient.Socket.SubscribeToAllSymbolTickerAsync((data) =>
                {
                    BAllPairsData.Add(data);
                    //decimal testpercent = 0;
                    //Console.WriteLine("Debug - Provider Core - Receiving data from ticker socket : {0}", data);
                    //foreach(var symbol in data)
                    //{
                    //    testpercent += symbol.PriceChangePercentage;
                    //    if(this.Sender.IsAlive)
                    //    {
                    //        this.Sender.Send(string.Format("Pair Name : {0} - Price : {1} - Change : {2} ", symbol.Symbol, symbol.WeightedAverage, symbol.PriceChangePercentage));

                    //    }
                    //    else
                    //    {
                    //        this.Sender.Connect();
                    //        this.Sender.Send(string.Format("Pair Name : {0} - Price : {1} - Change : {2} ", symbol.Symbol, symbol.WeightedAverage, symbol.PriceChangePercentage));
                    //    }
                    //}
                });

                while (Global.shared.Running)
                {
                    System.Threading.Thread.Sleep(100);
                }
            });



        }


        /// <summary>
        /// Receive Raw Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Data caching and moving to Azure Data Table logic goes here 
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var Candle = (BinanceStreamKlineData)e.NewItems[0];
                    //Remove Extra
                    var sourcedata = new Trady.Core.Candle(Candle.Data.CloseTime, Candle.Data.Open, Candle.Data.High, Candle.Data.Low, Candle.Data.Close, Candle.Data.Volume);
                    BinanceCandle Standardize = new BinanceCandle(sourcedata);
                   
                    Standardize.Name = Candle.Symbol;
                    Standardize.Candle = sourcedata;
                    Type myType = Candle.Data.GetType();

                    //Extract all exchanger candle properties
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    foreach (PropertyInfo prop in props)
                    {
                        object propValue = prop.GetValue(Candle.Data, null);
                        Standardize.Properties.Add(prop.Name, propValue);
                    }
                    Standardize.ConcatainedData = Newtonsoft.Json.JsonConvert.SerializeObject(Standardize);
                    Candles.Add(Standardize);
                    Console.WriteLine("Candle is added to dictionary");
                    //Until fix
                    try
                    {
                        if (UseSender) { this.Sender.Send(Standardize.ConcatainedData); }
                        DataOrganizer.SourceData.Add(Standardize);
                        //if (Moon.Global.shared.table != null)
                        //{
                        //    TableOperation insertOperation = TableOperation.Insert(Standardize);

                        //    // Execute the insert operation.
                        //    Moon.Global.shared.table.Execute(insertOperation);

                        //}


                    }
                    catch (Exception ex) {
                    }

                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
            }
        }



        /// <summary>
        /// Subscripte to KLine Stream
        /// </summary>
        /// <param name="Pair"></param>
        public void SubscribeTo(string Pair)
        {
            Task.Run(() =>
            {
                var tick = this.bclient.Socket.SubscribeToKlineStreamAsync(Pair, KlineInterval.OneMinute, (data) =>
                {
                    //Console.WriteLine("Debug - Provider Core - Receiving data from ticker socket : {0}",data.Symbol);
                    BData.Add(data);
                });

                while (Global.shared.Running)
                {
                    System.Threading.Thread.Sleep(100);
                }
            });

            Task.Run(() =>
            {
                var book = this.bclient.Socket.SubscribeToPartialBookDepthStreamAsync(Pair, 10, (data) =>
                 {
                     BBookData.Add(data);
                     if (BBookData.Count > 2) BBookData.RemoveAt(0);

                 });
                while (Global.shared.Running)
                {
                    System.Threading.Thread.Sleep(1000);
                }


            });

            Task.Run(() =>
            {
                var trades = this.bclient.Socket.SubscribeToTradesStreamAsync(Pair, (data) =>
                {
                    //Console.WriteLine("Debug - Provider Core - Receiving data from trade socket : {0}", data.Symbol);
                    if (!data.BuyerIsMaker)
                    {
                        BDataTradeSeller.Add(data);
                    }
                    else
                    {
                        BDataTradeBuyer.Add(data);
                    }
                });
                while (Global.shared.Running)
                {
                    System.Threading.Thread.Sleep(100);
                }

            });


        }

        public void UnsubscribeAllStreams()
        {
            this.bclient.Socket.UnsubscribeAllStreams();
        }

        public void Update()
        {
            this.Jscontainer = Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
