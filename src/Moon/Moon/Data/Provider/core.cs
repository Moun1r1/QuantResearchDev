using Binance.Net.Objects;
using Moon.Data.Exchanger;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trady.Analysis;
using Trady.Analysis.Extension;

namespace Moon.Data.Provider
{
    public enum ProviderMode
    {
        CandleOnly,
        AllTicks,
        TicksAndTrades,
        All
    }
    class Core
    {

        public ObservableCollection<BinanceStreamKlineData> BData { get; set; } = new ObservableCollection<BinanceStreamKlineData>();
        public ObservableCollection<BinanceStreamTrade> BDataTradeSeller { get; set; } = new ObservableCollection<BinanceStreamTrade>();
        public ObservableCollection<BinanceStreamTrade> BDataTradeBuyer { get; set; } = new ObservableCollection<BinanceStreamTrade>();

        public ObservableCollection<BinanceCandle> Candles { get; set; } = new ObservableCollection<BinanceCandle>();
        public binance bclient { get; set; } = new binance();
        public List<BinanceCandle> GenericCandle = new List<BinanceCandle>();
        public ProviderMode Mode { get; set; } = ProviderMode.All;
        public Core()
        {
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
                var RawData = Candles.Select(y => y.Candle).ToList();
                var indexdcandles = new IndexedCandle(RawData, RawData.Count() - 1);
                var LastBinanceCandle = (BinanceCandle)e.NewItems[0];
                LastBinanceCandle.Properties.Add("Bearish",indexdcandles.IsBearish());
                LastBinanceCandle.Properties.Add("IsBullish", indexdcandles.IsBullish());
                LastBinanceCandle.Properties.Add("IsAccumDistBearish", indexdcandles.IsAccumDistBearish());
                LastBinanceCandle.Properties.Add("IsAccumDistBullish", indexdcandles.IsAccumDistBullish());
                LastBinanceCandle.Properties.Add("ClosePricePercentageChange", indexdcandles.ClosePricePercentageChange());
                LastBinanceCandle.Properties.Add("ClosePriceChange", indexdcandles.ClosePriceChange());
                LastBinanceCandle.Properties.Add("IsBreakingHistoricalHighestClose", indexdcandles.IsBreakingHistoricalHighestClose());
                LastBinanceCandle.Properties.Add("IsBreakingHistoricalHighestHigh", indexdcandles.IsBreakingHistoricalHighestHigh());
                LastBinanceCandle.Properties.Add("IsBreakingHistoricalLowestLow", indexdcandles.IsBreakingHistoricalLowestLow());
                LastBinanceCandle.Properties.Add("IsObvBearish", indexdcandles.IsObvBearish());
                LastBinanceCandle.Properties.Add("IsObvBullish", indexdcandles.IsObvBullish());
            }


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
                    BinanceCandle Standardize = new BinanceCandle();
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

                    //Until fix
                    try
                    {
                        Candles.Add(Standardize);

                    }
                    catch { }

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
                    Console.WriteLine("Debug - Provider Core - Receiving data from ticker socket : {0}",data.Symbol);
                    BData.Add(data);
                });

                while (Global.shared.Running)
                {
                    System.Threading.Thread.Sleep(100);
                }
            });
            Task.Run(() =>
            {
                var trades = this.bclient.Socket.SubscribeToTradesStreamAsync(Pair, (data) =>
                {
                    Console.WriteLine("Debug - Provider Core - Receiving data from trade socket : {0}", data.Symbol);
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

    }
}
