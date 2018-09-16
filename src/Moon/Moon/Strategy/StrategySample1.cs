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
        public SignalsOrder Actions { get; set; } 
        public int RiskRatioMinimal { get; set; } = 0;
        public MyStrategyTest(CandlesSeries source, TradesSeries tradein)
        {
            this.CandleProvider = source;
            this.TradesProvider = tradein;
            this.CandleProvider.CandleUpdate += CandleProvider_CandleUpdate;
            this.TradesProvider.Update += TradesProvider_Update;
            this.CandleProvider.PatternUpdate += CandleProvider_PatternUpdate;
            this.Actions = new SignalsOrder(source);
        }

        private void CandleProvider_PatternUpdate(object sender, PatternEvent e)
        {
            Console.WriteLine("Strategy received pattern detection  : {0} for candle time : {1}", e.EventType,e.ComingCandle.CollectedDate);
            switch(e.EventType)
            {
                case PatternType.IsBearish:
                    this.Actions.SellPrice = this.CandleProvider.Close.Last();
                    Console.WriteLine("Strategy selling at : {0}", this.Actions.SellPrice);
                    this.Actions.Sell();
                    this.Actions.Update();
                    break;
                case PatternType.IsBullish:
                    if(!this.Actions.HasBought)
                    {
                        this.Actions.BuyPrice = this.CandleProvider.Close.Last();
                        this.Actions.HasBought = true;
                        this.Actions.Update();
                        Console.WriteLine("Strategy buying at : {0}", this.Actions.BuyPrice);


                    }
                    break;

            }
        }

        private void TradesProvider_Update(object sender, TradeEventArg e)
        {
            switch (e.EventType)
            {
                case TradeEventEventType.Book_L0:
                    //Console.WriteLine("Strategy received an order Quantity Book_L0 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[0].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L0 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[0].Price);

                    break;
                case TradeEventEventType.Book_L1:
                    //Console.WriteLine("Strategy received an order Quantity Book_L1 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[1].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L1 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[1].Price);

                    break;
                case TradeEventEventType.Book_L2:
                    //Console.WriteLine("Strategy received an order Quantity Book_L2 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[2].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L2 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[2].Price);

                    break;
                case TradeEventEventType.Book_L3:
                    //Console.WriteLine("Strategy received an order Quantity Book_L3 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[3].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L3 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[3].Price);

                    break;
                case TradeEventEventType.Book_L4:
                    //Console.WriteLine("Strategy received an order Quantity Book_L4 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[4].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L4 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[4].Price);

                    break;
                case TradeEventEventType.Book_L5:
                    //Console.WriteLine("Strategy received an order Quantity Book_L5 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[5].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L5 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[5].Price);

                    break;
                case TradeEventEventType.Book_L6:
                    //Console.WriteLine("Strategy received an order Quantity Book_L6 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[6].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L6 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[6].Price);

                    break;
                case TradeEventEventType.Book_L7:
                    //Console.WriteLine("Strategy received an order Quantity Book_L7 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[7].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L7 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[7].Price);

                    break;
                case TradeEventEventType.Book_L8:
                    //Console.WriteLine("Strategy received an order Quantity Book_L8 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[8].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L8 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[8].Price);

                    break;
                case TradeEventEventType.Book_L9:
                    //Console.WriteLine("Strategy received an order Quantity Book_L9 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[9].Quantity);
                    //Console.WriteLine("Strategy received an order Price Book_L9 update  : {0}", e.Source.Asks_BinanceStreamOrderBook[9].Price);

                    break;

                case TradeEventEventType.BuyOrder_RSI:
                    //Console.WriteLine("Strategy received order RSI calculation for Buyer Price at  : {0}", e.Source.BuyerRSI_Price.Last());
                    //Console.WriteLine("Strategy received order RSI calculation for Buyer Quantity at  : {0}", e.Source.BuyerRSI_Quantity.Last());
                    break;
                case TradeEventEventType.SellOrder_RSI:
                    //Console.WriteLine("Strategy received order RSI calculation for Seller Price at  : {0}", e.Source.SellerRSI_Price.Last());
                    //Console.WriteLine("Strategy received order RSI calculation for Seller Quantity at  : {0}", e.Source.SellerRSI_Quantity.Last());
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
            this.Actions.Update();

        }

        public override Signals GetSignal()
        {
            return base.GetSignal();
        }

    }
}
