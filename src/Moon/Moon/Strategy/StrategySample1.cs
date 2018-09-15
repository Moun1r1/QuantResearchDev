using Moon.Data.Bacher;
using Moon.Data.Extender;
using Moon.Data.Model;
using Moon.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Strategy
{
    [DefaultStrategyConf(StrategyType.Mono,Exchange.Binance,"Sample1",true,SignalSource.CustomPeriod)]
    [TradingLogicAttribute(RunTradingType.Paper)]
    [StratTimeRangeOptionnal(TimeRange.Minute5)]
    public class MyStrategyTest : StrategyCore, IStrategy
    {
        public string Name { get; set; } = "Sample";
        public CandlesSeries CandleProvider { get; set; } = new CandlesSeries();
        public TradesSeries  TradesProvider { get; set; }
        public int RiskRatioMinimal { get; set; } = 0;
        public MyStrategyTest(CandlesSeries source, TradesSeries tradein)
        {
            this.CandleProvider = source;
            this.TradesProvider = tradein;
            this.CandleProvider.CandleUpdate += CandleProvider_CandleUpdate;
            this.TradesProvider.Update += TradesProvider_Update;
        }

        private void TradesProvider_Update(object sender, TradeEventArg e)
        {
            switch(e.EventType)
            {
                case TradeEventEventType.BuyOrder_RSI:
                    Console.WriteLine("Strategy received order RSI calculation for Buyer Price at  : {0}", e.Source.BuyerRSI_Price.Last());
                    Console.WriteLine("Strategy received order RSI calculation for Buyer Quantity at  : {0}", e.Source.BuyerRSI_Quantity.Last());
                    break;
                case TradeEventEventType.SellOrder_RSI:
                    Console.WriteLine("Strategy received order RSI calculation for Seller Price at  : {0}", e.Source.SellerRSI_Price.Last());
                    Console.WriteLine("Strategy received order RSI calculation for Seller Quantity at  : {0}", e.Source.SellerRSI_Quantity.Last());
                    break;

            }
        }

        private void CandleProvider_CandleUpdate(object sender, CandleEventArg e)
        {
            Console.WriteLine("Strategy : {0} received candle for {1}", this.Name, e.Collected);

            //Incoming Candle on strategy
            var GetComingCandle = e.ComingCandle;

            //All Candles used on strategy
            var AllCandles = e.Candlescopy.Candles;
           
            //Does something here;

        }

        public override Signals GetSignal()
        {
            return base.GetSignal();
        }

    }
}
