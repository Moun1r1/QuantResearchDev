using Binance.Net.Objects;
using Microsoft.WindowsAzure.Storage.Table;
using Moon.Data.Bacher;
using Moon.Data.Exchanger;
using Moon.Data.Extender;
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
    public class BinanceProvier: IRoot
    {

        public ObservableCollection<BinanceStreamKlineData> BData { get; set; } = new ObservableCollection<BinanceStreamKlineData>();
        public ObservableCollection<BinanceStreamTick[]> BAllPairsData { get; set; } = new ObservableCollection<BinanceStreamTick[]>();
        //public ObservableCollection<BinanceStreamOrderBook> BBookData { get; set; } = new ObservableCollection<BinanceStreamOrderBook>();
        public Grouper DataOrganizer { get; set; } = new Grouper();
        public ObservableCollection<BinanceStreamTrade> BDataTradeSeller { get; set; } = new ObservableCollection<BinanceStreamTrade>();
        public ObservableCollection<BinanceStreamTrade> BDataTradeBuyer { get; set; } = new ObservableCollection<BinanceStreamTrade>();
        public ObservableCollection<BinanceCandle> CandlesTable { get; set; } = new ObservableCollection<BinanceCandle>();
        public WebSocket Sender { get; set; }
        public bool UseSender { get; set; } = false;
        public ObservableCollection<BinanceCandle> Candles { get; set; } = new ObservableCollection<BinanceCandle>();
        public BinanceExchanger bclient { get; set; } = new BinanceExchanger();
        public List<BinanceCandle> GenericCandle = new List<BinanceCandle>();
        public ProviderMode Mode { get; set; } = ProviderMode.All;
        public string Jscontainer { get; set; }
        public string TypeOfData { get; set; } = "BinanceCore";

        public BinanceProvier()
        {
            if(UseSender)
            {
                this.Sender = new WebSocket(string.Format("ws://localhost:1345/{0}", Moon.Global.Shared.ConfigUri.CandleMarketPath));
                this.Sender.Connect();

            }
            if (Global.Shared.table != null)
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
        /// Get Data From Date X to Date Y on Symbol V
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Symbol"></param>
        public void GetDataFromTo(DateTime From, DateTime To, string Symbol, KlineInterval interval = KlineInterval.OneMinute)
        {
            Console.WriteLine("Core - Loading data for : {0}", Symbol);

            if (From > To) { throw new Exception(string.Format("From : {0} is superior to To : {1}", From, To));  }



            var CandleMin = this.bclient.Client.GetKlines(Symbol, interval, startTime: From, endTime: To,limit:int.MaxValue);
            foreach(var data in CandleMin.Data)
            {
                BinanceStreamKlineData formated = new BinanceStreamKlineData();
                formated.Data = new BinanceStreamKline();
                formated.EventTime = data.CloseTime;
                formated.Symbol = Symbol;
                formated.Data.Close = data.Close;
                formated.Data.CloseTime = data.CloseTime;
                formated.Data.Final = true;
                formated.Data.High = data.High;
                formated.Data.Low = data.Low;
                formated.Data.OpenTime = data.OpenTime;
                formated.Data.Symbol = Symbol;
                formated.Data.Open = data.Open;
                formated.Data.Volume = data.Volume;
                formated.Data.TakerBuyBaseAssetVolume = data.TakerBuyBaseAssetVolume;
                formated.Data.TakerBuyQuoteAssetVolume = data.TakerBuyQuoteAssetVolume;
                formated.Data.TradeCount = data.TradeCount;
                var sourcedata = new Trady.Core.Candle(formated.Data.CloseTime, formated.Data.Open, formated.Data.High, formated.Data.Low, formated.Data.Close, formated.Data.Volume);
                BinanceCandle Standardize = new BinanceCandle(sourcedata);
                Standardize.Last = formated.Data.Final;
                Standardize.Name = formated.Symbol;
                Standardize.Candle = sourcedata;
                Type myType = formated.Data.GetType();

                //Extract all exchanger candle properties
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(formated.Data, null);
                    Standardize.Properties.Add(prop.Name, propValue);
                }
                Standardize.Update();
                this.Candles.Add(Standardize);


            }
            Console.WriteLine("Core - Finished to load from {0} to {1} for symbol {2}", From, To, Symbol);
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

            }
        }

        private void LoadAlldata()
        {
            throw new NotImplementedException();
            //try
            //{
            //    if(Global.Shared.table != null)
            //    {
            //        Task.Run(() =>
            //        {
            //            //try
            //            //{
            //            //    TableQuery<BinanceCandle> query = new TableQuery<BinanceCandle>();

            //            //    var content = Global.Shared.table.ExecuteQuery(query);
            //            //    if (content.Count() > 0)
            //            //    {
            //            //        foreach (var oldcandles in content)
            //            //        {
            //            //            var olddata = Newtonsoft.Json.JsonConvert.DeserializeObject<BinanceCandle>(oldcandles.Jscontainer);
            //            //            CandlesTable.Add(olddata);
            //            //            Console.WriteLine("Loaded : {0} on History Table", CandlesTable.Count());
            //            //        }

            //            //    }

            //            //}
            //            //catch (Exception ex)
            //            //{

            //            //}

            //        });

            //    }
            //}
            //catch
            //{

            //}
        }

        public void RegisterAllMarket()
        {
            Task.Run(() =>
            {
                var tick = this.bclient.Socket.SubscribeToAllSymbolTickerAsync((data) =>
                {
                    if(BAllPairsData.Count() > 2)
                    {
                        BAllPairsData.Clear();
                    }

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

                while (Global.Shared.Running)
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

                    Standardize.Last = Candle.Data.Final;
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
                    Standardize.Update();
                    try
                    {
                        Candles.Add(Standardize);

                    }
                    catch(Exception ex)
                    {
                        
                        Console.WriteLine("Exception during core candle add (computed) : {0} ", ex.Message); 
                    }
                    //Until fix
                    try
                    {
                        if (UseSender) { this.Sender.Send(Standardize.Jscontainer); }
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
        public void SubscribeTo(string Pair, KlineInterval interval = KlineInterval.OneMinute)
        {
            Console.WriteLine("Core - Starting thread for Kline Stream for : {0}", Pair);

            Task.Run(() =>
            {
                var tick = this.bclient.Socket.SubscribeToKlineStreamAsync(Pair, interval, (data) =>
                {
                    BData.Add(data);

                });

                while (Global.Shared.Running)
                {
                    System.Threading.Thread.Sleep(100);
                }
            });
            Console.WriteLine("Core - Starting thread for OrderBook Stream for : {0}", Pair);

            Task.Run(() =>
            {
                var book = this.bclient.Socket.SubscribeToPartialBookDepthStreamAsync(Pair, 10, (data) =>
                 {
                     //BBookData.Add(data);
                     //if (BBookData.Count > 2) BBookData.RemoveAt(0);

                 });
                while (Global.Shared.Running)
                {
                    System.Threading.Thread.Sleep(1000);
                }


            });
            Console.WriteLine("Core - Starting thread for Trade Stream for : {0}", Pair);

            Task.Run(() =>
            {
                var trades = this.bclient.Socket.SubscribeToTradesStreamAsync(Pair, (data) =>
                {

                    if (!data.BuyerIsMaker)
                    {
                        BDataTradeSeller.Add(data);
                    }
                    else
                    {
                        BDataTradeBuyer.Add(data);
                    }
                });
                while (Global.Shared.Running)
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

            this.Jscontainer = this.ToJson();
        }
    }
}
