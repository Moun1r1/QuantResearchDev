using Moon.Data.Model;
using Moon.Nodes.service.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Trady;
using Trady.Analysis;
using Trady.Analysis.Extension;
using System.Reflection;
using Trady.Analysis.Indicator;
using Trady.Analysis.Candlestick;
namespace Moon.Nodes.service.core
{
   public class Corenode
    {
        public Moon.Data.Provider.Core core = new Moon.Data.Provider.Core();
        public Corenode()
        {
            core.SubscribeTo("ETHUSDT");

        }
    }
    public class ServiceCandleMarket : WebSocketBehavior
    {
        public Corenode Starter { get; set; } = new Corenode();
        WebSocketSharp.Server.WebSocketSessionManager Manager;
        public ServiceCandleMarket()
        {
            Starter.core.Candles.CollectionChanged += BData_CollectionChanged;
            Starter.core.BDataTradeBuyer.CollectionChanged += BDataTradeBuyer_CollectionChanged;
            Starter.core.BDataTradeSeller.CollectionChanged += BDataTradeSeller_CollectionChanged;
            this.Manager = this.Sessions;

        }
        public void SetLinkTo(Corenode node)
        {
            Starter = node;
            Starter.core.Candles.CollectionChanged += BData_CollectionChanged;
            Starter.core.BDataTradeBuyer.CollectionChanged += BDataTradeBuyer_CollectionChanged;
            Starter.core.BDataTradeSeller.CollectionChanged += BDataTradeSeller_CollectionChanged;

        }
        private void BDataTradeSeller_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var TraderSeller = (Binance.Net.Objects.BinanceStreamTrade)e.NewItems[0];
                Messages Content = new Messages();
                Content.Content = Newtonsoft.Json.JsonConvert.SerializeObject(TraderSeller);
                Content.MessageType = TypeOfContent.Binance_TradesSeller;
                Content.RootType = "BinanceTrade";
                Content.TargetObject = "BinanceStreamTrade";
                Send(Content.ToString());

            }
        }

        private void BDataTradeBuyer_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var TraderBuyer = (Binance.Net.Objects.BinanceStreamTrade)e.NewItems[0];
                Messages Content = new Messages();
                Content.Content = Newtonsoft.Json.JsonConvert.SerializeObject(TraderBuyer);
                Content.MessageType = TypeOfContent.Binance_TradesBuyer;
                Content.RootType = "BinanceTrade";
                Content.TargetObject = "BinanceStreamTrade";
                Send(Content.ToString());

            }
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            this.Manager = this.Sessions;
            Console.WriteLine("New Client : {0}", this.Sessions.Sessions.Last().ID);
        }
        private void BData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var candle = (BinanceCandle)e.NewItems[0];
                var TPC = (candle.Candle.High + candle.Candle.Low + candle.Candle.Close) / 3;
                var TPVM = TPC * candle.Candle.Volume;
                var RawData = Starter.core.Candles.Select(y => y.Candle).ToList();
                int countIndex = RawData.Count - 1;
                var indexdcandles = new IndexedCandle(RawData, RawData.Count() - 1);
                //public static class OhlcvExtension

                //zone to update with TA - Node (temporary here)
                try
                {
                    var rsi = RawData.Rsi(12, 0, countIndex);
                    var bb = RawData.BbWidth(20, 2);
                    var macd = RawData.Macd(7, 10, 12);
                    var chandelier = RawData.Chandlr(14, 22);
                    var ichimoku = RawData.Ichimoku(9, 26, 52);
                    var KAMA = RawData.Kama(21, 12, 32);
                    var Median = RawData.Median(12);
                    var PSAR = RawData.Sar(0.2m, 0.2m);
                    var DM = new DownMomentum(RawData);
                    var bbw = new BollingerBands(RawData, 20, 2);
                    var UM = new UpMomentum(RawData);
                    var Bullish = new Bullish(RawData);
                    var Bearish = new Bearish(RawData);
                    var trendup = new UpTrend(RawData);
                    var trenddown = new DownTrend(RawData);
                    var dmi = new DynamicMomentumIndex(RawData, 14, 30, 12, 70, 30);
                    var minusDI = new MinusDirectionalIndicator(RawData, 12);
                    var truerange = new TrueRange(RawData);
                    var efratio = new EfficiencyRatio(RawData, 12);
                    var obv = new OnBalanceVolume(RawData);
                    var sthrsi = new StochasticsRsiOscillator(RawData, 12);
                    if (rsi.Count() > 0)
                    {

                        //TA
                        candle.Properties.Add("RSI", rsi.Last().Tick);
                        candle.Properties.Add("DOWNMOMENTUM", DM.Compute(12).Last().Tick);
                        candle.Properties.Add("UPMOMENTUM", UM.Compute(12).Last().Tick);
                        candle.Properties.Add("MACD", macd.Last().Tick);
                        candle.Properties.Add("BB", bb.Last().Tick);
                        candle.Properties.Add("bbw", bbw.Compute().Last().Tick);
                        candle.Properties.Add("PSAR", PSAR.Last().Tick);
                        candle.Properties.Add("CHANDELIEREXIT", chandelier.Last().Tick);
                        candle.Properties.Add("ichimoku", ichimoku.Last().Tick);
                        candle.Properties.Add("KAMA", KAMA.Last().Tick);
                        candle.Properties.Add("Median", Median.Last().Tick);
                        candle.Properties.Add("DynamicMomentumIndex", dmi.Compute(12).Last().Tick);
                        candle.Properties.Add("MinusDirectionalIndicator", minusDI.Compute(12).Last().Tick);
                        candle.Properties.Add("TrueRange", truerange.Compute(12).Last().Tick);
                        candle.Properties.Add("EfficiencyRatio", efratio.Compute(12).Last().Tick);
                        candle.Properties.Add("OnBalanceVolume", obv.Compute(12).Last().Tick);
                        candle.Properties.Add("StochasticsRsiOscillator", sthrsi.Compute(12).Last().Tick);

                        //Pattern Periodic
                        candle.Properties.Add("BullishPeriodic_L1", Bullish.Compute(12).Last().Tick);
                        candle.Properties.Add("BearishPeriodic_L1", Bearish.Compute(12).Last().Tick);
                        candle.Properties.Add("UptrendPeriodic_L1", trendup.Compute(12).Last().Tick);
                        candle.Properties.Add("DowntrendPeriodic_L1", trenddown.Compute(12).Last().Tick);

                        //candle.Properties.Add("BullishPeriodic_L2", Bullish.Compute(24).Last().Tick);
                        //candle.Properties.Add("BearishPeriodic_L2", Bearish.Compute(24).Last().Tick);
                        //candle.Properties.Add("UptrendPeriodic_L2", trendup.Compute(24).Last().Tick);
                        //candle.Properties.Add("DowntrendPeriodic_L2", trenddown.Compute(24).Last().Tick);


                        //candle.Properties.Add("BullishPeriodic_L3", Bullish.Compute(36).Last().Tick);
                        //candle.Properties.Add("BearishPeriodic_L3", Bearish.Compute(36).Last().Tick);
                        //candle.Properties.Add("UptrendPeriodic_L3", trendup.Compute(36).Last().Tick);
                        //candle.Properties.Add("DowntrendPeriodic_L3", trenddown.Compute(36).Last().Tick);

                        //PatternInject
                        candle.Properties.Add("bearish", indexdcandles.IsBearish());
                        candle.Properties.Add("isbullish", indexdcandles.IsBullish());
                        candle.Properties.Add("isaccumdistbearish", indexdcandles.IsAccumDistBearish());
                        candle.Properties.Add("isaccumdistbullish", indexdcandles.IsAccumDistBullish());
                        candle.Properties.Add("closepricepercentagechange", indexdcandles.ClosePricePercentageChange());
                        candle.Properties.Add("closepricechange", indexdcandles.ClosePriceChange());
                        candle.Properties.Add("isbreakinghistoricalhighestclose", indexdcandles.IsBreakingHistoricalHighestClose());
                        candle.Properties.Add("isbreakinghistoricalhighesthigh", indexdcandles.IsBreakingHistoricalHighestHigh());
                        candle.Properties.Add("isbreakinghistoricallowestlow", indexdcandles.IsBreakingHistoricalLowestLow());
                        candle.Properties.Add("isobvbearish", indexdcandles.IsObvBearish());
                        candle.Properties.Add("isobvbullish", indexdcandles.IsObvBullish());
                        candle.UpdateContainer();
                    }

                }
                catch { }
                var LastBinanceCandle = (BinanceCandle)e.NewItems[0];

                Console.WriteLine("Market Node - Sending candle : {0} to suscribers",LastBinanceCandle.UID);
                Messages Content = new Messages();
                Content.Content = candle.Jscontainer;
                Content.MessageType = TypeOfContent.Binance_Candles;
                Content.RootType = candle.TypeOfData;
                Content.TargetObject = "BinanceCandle";
                Send(Content.ToString());
            }
        }



        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received client request..");
            if (e.IsText)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(e.Data);
                    switch (result is Moon.Data.Model.BinanceCandle)
                    {
                    }
                    Console.WriteLine("Exchanger : {0}", result.Exchanger);
                    Console.WriteLine("Exchanger : {0}", result.Name);
                    Console.WriteLine("CollectedDate : {0}", result.CollectedDate);
                    Console.WriteLine("Pivot : {0}", result.Pivot);
                }
                catch { }

                Console.WriteLine("Reiceved : {0}", e.Data);
            }
        }



    }

}
