using Moon.Data.Accounting;
using Moon.Data.Extender;
using Moon.Data.Model;
using Moon.Data.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Provider
{
    
    public enum TypeOfSignal
    {
        Buy,
        Sell,
        IncreasePositive,
        DecreasePositive,
        IncreaseNegative,
        DecreaseNegative,
        MedianMove,
        Leave,
        Keep,
        NewLow, 
        NewHigh,
        ExitNow,
        ExitOnIdeal,
        ExitOnLessWorst
    }
    public enum OrderProfitType
    {
        Auto,
        Manual
    }
    public enum OrderType
    {
        Buy,
        Sell,
        BuyLimit,
        SellLimit,
        Hodle,
        Update
    }
    public enum StrategyOrderStatus
    {
        Inprogress,
        Failed,
        Done
    }
    public class StrategyOrder
    {
        public OrderType TypeOfOrder { get; set; }
        public OrderProfitType TypeOfProfit { get; set; }
        public string Symbol { get; set; }
        public Account account { get; set; }
        public double Profit { get; set; }
        public double ProfitThreeShold { get; set; }
        public double PNL { get; set; }
        public Exchange Platform { get; set; }
        public DateTime BuyTime { get; set; }
        public DateTime SellTime { get; set; }
        public double LotSize { get; set; }
        public double InvestedInTotal { get; set; }
        public double InvestedInTotalWithProfit { get; set; }
        public double ProfitLot { get; set; }
        public CandlesSeries DataProvider { get; set; }
        public bool HasBought { get; set; } = false;
        public double BuyPrice { get; set; }
        public bool Live { get; set; } = false;
        public double SellPrice { get; set; }
        public int waiter = 0;
        public double RRM { get; set; } = 0;
        public StrategyOrderStatus status { get; set; }
        public Guid OperationID { get; set; } = Guid.NewGuid();
        public TradeItem LinkedTrades { get; set; } = new TradeItem();
        public StrategyOrder(CandlesSeries _source)
        {
            this.DataProvider = _source;
            this.waiter = 0;

        }
        public void Buy()
        {
            if (this.Live &&! this.HasBought)
            {
                this.account.Buy(this.DataProvider.Candles.Last(), this.Symbol, Binance.Net.Objects.OrderType.Limit, this.OperationID, this.LotSize.ChangeType<decimal>());
                LinkedTrades = this.account.ExecutedOrders.Where(y => y.OperationID == this.OperationID).First();
                bool waitfortrade = true;
                while (waitfortrade)
                {
                    var order = this.account.ExecutedOrders.Where(y => y.OperationID == this.OperationID).First();
                    if (order.State == OrderExecutionStatus.Filled)
                    {
                        this.BuyPrice = order.BoughtPrice.ChangeType<double>();
                        this.BuyTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                        this.HasBought = true;
                        this.status = StrategyOrderStatus.Inprogress;
                        waitfortrade = false;
                    }
                    if (order.State == OrderExecutionStatus.Error)
                    {
                        this.HasBought = false;
                        this.status = StrategyOrderStatus.Failed;
                        waitfortrade = false;

                    }
                    System.Threading.Thread.Sleep(3000);
                }

               
            }
            //Backtest mode
            else{

                this.BuyPrice = this.DataProvider.Candles.Last().Candle.High.ChangeType<double>();
                this.BuyTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;

                this.HasBought = true;
                //place order here
                this.status = StrategyOrderStatus.Inprogress;

            }
        }
        public void Sell()
        {
            if(this.status == StrategyOrderStatus.Inprogress)
            {

                if (this.Live && this.HasBought)
                {
                    this.account.Sell(this.DataProvider.Candles.Last(), this.Symbol, Binance.Net.Objects.OrderType.Limit, this.OperationID, this.LotSize.ChangeType<decimal>());
                    LinkedTrades = this.account.ExecutedOrders.Where(y => y.OperationID == this.OperationID).First();
                    bool waitfortrade = true;
                    while (waitfortrade)
                    {
                        var order = this.account.ExecutedOrders.Where(y => y.OperationID == this.OperationID).First();
                        if(order.State == OrderExecutionStatus.Filled)
                        {
                            this.Profit = this.BuyPrice.FindDifference(order.Soldprice.ChangeType<double>());
                            this.Profit -= 0.075 * 2;
                            this.InvestedInTotalWithProfit = (this.InvestedInTotal * this.Profit);
                            this.ProfitLot = (this.LotSize + this.Profit);
                            this.SellTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                            this.SellPrice = this.DataProvider.Low.Last();
                            this.HasBought = false;
                            this.status = StrategyOrderStatus.Done;
                            this.PNL = (this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>() / this.LotSize -
                                this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>());
                            waitfortrade = false;
                        }
                        if(order.State == OrderExecutionStatus.Error)
                        {
                            this.status = StrategyOrderStatus.Inprogress;

                            waitfortrade = false;
                        }
                        System.Threading.Thread.Sleep(3000);
                    }
                }
                else
                {
                    this.Profit = this.BuyPrice.FindDifference(this.DataProvider.Close.Last());
                    this.Profit -= 0.075 * 2;
                    this.InvestedInTotalWithProfit = (this.InvestedInTotal * this.Profit);
                    this.ProfitLot = (this.LotSize + this.Profit);
                    this.SellTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                    this.SellPrice = this.DataProvider.Low.Last();
                    this.HasBought = false;
                    this.status = StrategyOrderStatus.Done;
                    this.PNL = (this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>() / this.LotSize -
                        this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>());

                }

            }
        }
        public void Report()
        {
            if (this.status == StrategyOrderStatus.Inprogress)
            {
                this.Profit = this.BuyPrice.FindDifference(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>());
                this.Profit -= 0.075 * 2;

                this.waiter++;
                //if (this.waiter > 2 && this.Profit > 0.2)
                //{
                //    Console.WriteLine("TP Activated");

                //    this.Sell();
                //}
                //if (this.waiter > 2 && this.Profit < -2)
                //{
                //    Console.WriteLine("SL Activated");
                //    this.Sell();
                //}

                //if (this.Profit > 1)
                //{
                //    Console.WriteLine("Take Profit : {0}", this.Profit);
                //    Sell();
                //}
                //if (this.Profit < -5)
                //{
                //    Console.WriteLine("Take Loose : {0}", this.Profit);
                //    Sell();

                //}

            }

        }
        public void Update()
        {
            if (this.HasBought)
            {
                Report();
            }

        }

    }

    public class SignalsManager : Signals , IRoot, ISignalProvider
    {
        public TypeOfSignal SignalDirection { get; set; }
        public SignalSource Source { get; set; }
        public string Symbol { get; set; }
        public TimeSpan SignalDuration { get; set; }
        public bool Continious { get; set; } = false;
        public bool OneShot { get; set; } = true;
        public bool FillOrDie { get; set; } = false;
        public bool HasBought { get; set; } = false;
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public double PNL { get; set; }
        public double Profit { get; set; }
        public double CumulatedProfit { get; set; }
        public double CumulatedFund { get; set; }
        public double Volume { get; set; }
        public bool LiveMode { get; set; }
        public Account Fund { get; set; }
        public CandlesSeries DataProvider { get; set; }
        public List<StrategyOrder> SignalsOnging { get; set; } = new List<StrategyOrder>();
        public SignalsManager(CandlesSeries _source)
        {
            this.DataProvider = _source;
        }
        public void PutNewSignal(StrategyOrder Order)
        {
           if(this.SignalsOnging.Count() > 1)
           {
              if(this.SignalsOnging.Last().status == StrategyOrderStatus.Inprogress)
              {
                    this.SignalsOnging.Last().Report();
              }
              else
              {
                    this.SignalsOnging.Add(Order);
                    this.SignalsOnging.Last().account = this.Fund;
                    this.SignalsOnging.Last().Live = this.LiveMode;
                    if (this.SignalsOnging.Last().RRM !=0)
                    {
                        this.SignalsOnging.Last().LotSize = this.Fund.CalculateLotSize(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>(), GetCurrency(this.Symbol),this.Symbol, this.SignalsOnging.Last().RRM);

                    }
                    else
                    {
                        this.SignalsOnging.Last().LotSize = this.Fund.CalculateLotSize(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>(), GetCurrency(this.Symbol), this.Symbol);

                    }
                    this.SignalsOnging.Last().InvestedInTotal = this.SignalsOnging.Last().BuyPrice * this.SignalsOnging.Last().LotSize;
                    this.SignalsOnging.Last().Buy();
                    this.HasBought = this.SignalsOnging.Last().HasBought;
                    this.SignalsOnging.Last().BuyTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                    this.SignalsOnging.Last().Update();
              }
           }
            else
            {
                this.SignalsOnging.Add(Order);
                this.SignalsOnging.Last().account = this.Fund;
                this.SignalsOnging.Last().Live = this.LiveMode;
                if (this.SignalsOnging.Last().RRM != 0)
                {
                    this.SignalsOnging.Last().LotSize = this.Fund.CalculateLotSize(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>(), GetCurrency(this.Symbol), this.Symbol, this.SignalsOnging.Last().RRM);

                }
                else
                {
                    this.SignalsOnging.Last().LotSize = this.Fund.CalculateLotSize(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>(), GetCurrency(this.Symbol), this.Symbol);

                }
                this.SignalsOnging.Last().InvestedInTotal = this.SignalsOnging.Last().BuyPrice * this.SignalsOnging.Last().LotSize;
                this.SignalsOnging.Last().Buy();
                this.HasBought = this.SignalsOnging.Last().HasBought;
                this.SignalsOnging.Last().BuyTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                this.SignalsOnging.Last().Update();
            }
        }
        public string GetCurrency(string symbol)
        {
            if(symbol.ToUpper().EndsWith("BTC")) { return "BTC"; }
            if(symbol.ToUpper().EndsWith("ETH")) { return "ETH"; }
            if(symbol.ToUpper().EndsWith("USDT")) { return "USDT"; }
            if(symbol.ToUpper().EndsWith("BTC")) { return "BTC"; }
            if(symbol.ToUpper().EndsWith("BNB")) { return "BNB"; }

            return string.Empty;
        }
        public void UpdateLastSignal(OrderType Action)
        {
            switch(Action)
            {
                case OrderType.Buy:
                    this.SignalsOnging.Last().Live = this.LiveMode;
                    this.SignalsOnging.Last().account = this.Fund;
                    this.SignalsOnging.Last().LotSize = this.Fund.CalculateLotSize(this.DataProvider.Candles.Last().Candle.Close.ChangeType<double>(), GetCurrency(this.Symbol), this.Symbol);
                    this.SignalsOnging.Last().InvestedInTotal = this.SignalsOnging.Last().BuyPrice * this.SignalsOnging.Last().LotSize;
                    this.SignalsOnging.Last().BuyTime = this.DataProvider.Candles.Last().Candle.DateTime.DateTime;
                    this.SignalsOnging.Last().Buy();
                    this.HasBought = this.SignalsOnging.Last().HasBought;
                    this.SignalsOnging.Last().Update();
                    break;
                case OrderType.Sell:
                    this.CumulatedProfit = this.SignalsOnging.Where(a => a.status == StrategyOrderStatus.Done).Select(y => y.Profit).Sum();
                    this.CumulatedFund = this.SignalsOnging.Where(a => a.status == StrategyOrderStatus.Done).Select(y => y.InvestedInTotalWithProfit).Sum();
                    Console.WriteLine("Symbol {0} - Cumulated Profit :{1}",this.Symbol, this.CumulatedProfit);
                    Console.WriteLine("Symbol {0} - Cumulated Fund :{1}", this.Symbol, this.CumulatedFund);
                    this.SignalsOnging.Last().Live = this.LiveMode;
                    this.SignalsOnging.Last().account = this.Fund;
                    this.SignalsOnging.Last().Sell();
                    this.HasBought = this.SignalsOnging.Last().HasBought;
                    this.SignalsOnging.Last().Update();
                    this.HasBought = false;
                    break;
                case OrderType.Update:
                    this.SignalsOnging.Last().Report();
                    this.SignalsOnging.Where(y => y.status != StrategyOrderStatus.Done).ToList().ForEach(y => y.Report());
                    if(this.SignalsOnging.Last().status == StrategyOrderStatus.Done)
                    {
                        this.CumulatedProfit = this.SignalsOnging.Where(a => a.status == StrategyOrderStatus.Done).Select(y => y.Profit).Sum();
                        this.HasBought = this.SignalsOnging.Last().HasBought;

                    }
                    break;
            }
        }

    }
    public class Signals : IRoot, ISignalProvider
    {
        public string Jscontainer { get; set; }
        public string TypeOfData { get; set; } = "Signal";
        public Action CustomBlockOfCode { get; set; }
        public SignalSource Providers { get; set; }
        public Signals(SignalSource getfrom)
        {
            this.Providers = getfrom;
        }

        public Signals()
        {
        }

        public void Update()
        {
            
        }
    }
}
