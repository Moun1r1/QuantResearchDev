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

namespace Moon.Nodes
{

    public class CandleMarket : WebSocketBehavior
    {
        Corenode Starter = new Corenode();
        WebSocketSharp.Server.WebSocketSessionManager Manager;
        public CandleMarket()
        {
            Starter.core.Candles.CollectionChanged += BData_CollectionChanged;
            this.Manager = this.Sessions;

        }
        protected override void OnOpen()
        {
            base.OnOpen();
            this.Manager = this.Sessions;
        }
        private void BData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
               var candle = (BinanceCandle)e.NewItems[0];
                var TPC = (candle.Candle.High + candle.Candle.Low + candle.Candle.Close) / 3;
                var TPVM = TPC * candle.Candle.Volume;
                var RawData = Starter.core.Candles.Select(y => y.Candle).ToList();
                int countIndex = RawData.Count - 1;
                var indexdcandles = new IndexedCandle(RawData, RawData.Count() - 1);
                //public static class OhlcvExtension
                var subclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(OhlcvExtension))
                select type;

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
                    var minusDI = new MinusDirectionalIndicator(RawData,12);
                    var truerange = new TrueRange(RawData);
                    var efratio = new EfficiencyRatio(RawData, 12);
                    var obv = new OnBalanceVolume(RawData);
                    var sthrsi = new StochasticsRsiOscillator(RawData, 12);
                    if (rsi.Count()  > 0)
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

                        Console.WriteLine("RSI : {0}", rsi.Last().Tick);
                        Console.WriteLine("BB : {0}", bb.Last().Tick);
                        Console.WriteLine("MACD : {0}", macd.Last().Tick);

                        Console.WriteLine("bearish : {0}", indexdcandles.IsBearish());
                        Console.WriteLine("isbullish : {0}", indexdcandles.IsBullish());
                        Console.WriteLine("isaccumdistbearish : {0}", indexdcandles.IsAccumDistBearish());
                        Console.WriteLine("isaccumdistbullish : {0}", indexdcandles.IsAccumDistBullish());
                        Console.WriteLine("closepricepercentagechange : {0}", indexdcandles.ClosePricePercentageChange());
                        Console.WriteLine("closepricechange : {0}", indexdcandles.ClosePriceChange());
                        Console.WriteLine("isbreakinghistoricalhighestclose : {0}", indexdcandles.IsBreakingHistoricalHighestClose());
                        Console.WriteLine("isbreakinghistoricalhighesthigh : {0}", indexdcandles.IsBreakingHistoricalHighestHigh());
                        Console.WriteLine("isbreakinghistoricallowestlow: {0} ", indexdcandles.IsBreakingHistoricalLowestLow());
                        Console.WriteLine("isobvbearish: {0}", indexdcandles.IsObvBearish());
                        Console.WriteLine("isobvbullish: {0}", indexdcandles.IsObvBullish());
                        candle.UpdateContainer();
                    }
                    
                }
                catch { }
                var LastBinanceCandle = (BinanceCandle)e.NewItems[0];

                Console.WriteLine("Sending candle to suscribers");
               Send(candle.Jscontainer);
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
                    switch(result is Moon.Data.Model.BinanceCandle)
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

    class Program
    {
        static void Main(string[] args)
        {
            //Config Loader

            Corenode Starter = new Corenode();
            var wssv = new WebSocketServer(1346);
            wssv.AddWebSocketService<CandleMarket>("/CandleMarket");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();

        }

    }
}
