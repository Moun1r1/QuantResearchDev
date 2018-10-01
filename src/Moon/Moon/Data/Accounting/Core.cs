using Binance.Net;
using Binance.Net.Objects;
using Moon.Data.Extender;
using Moon.Data.Model;
using Moon.Data.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Accounting
{
    public enum OrderExecutionStatus
    {
        Ready =0,
        Missed,
        Submited,
        Waiting,
        Filled,
        PartialFilled,
        NotFilled,
        Error,
        Market
    }

    public class TradeItem
    {
        public OrderExecutionStatus State { get; set; }
        public DateTime Started { get; set; } = DateTime.Now;
        public string Symbol { get; set; }
        public decimal BoughtPrice { get; set; }
        public long OrderID { get; set; }
        public decimal Soldprice { get; set; }
        public bool TPActivated { get; set; }
        public Guid OperationID { get; set; }
        public BinancePlacedOrder BuyLinkedOrder { get; set; }
        public BinancePlacedOrder SellLinkedOrder { get; set; }

        public TradeItem()
        {

        }
    }

    /// <summary>
    /// Root Accounting Class
    /// </summary>
    public class Account : IRoot
    {
        public Exchange Platform { get; set; } = Exchange.Binance;
        public string Name { get; set; } = "Unknown";
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public List<TradeItem> ExecutedOrders { get; set; } = new List<TradeItem>();
        public decimal TotalBalance { get; set; } = decimal.MinValue;

        public List<BinanceBalance> Balances { get; set; } = new List<BinanceBalance>();
        public double USDTAvailable = 0;
        public double ETHAvailable = 0;
        public double BTCAvailable = 0;
        public double BNBAvailable = 0;
        public double BCCAvailable = 0;
        public Dictionary<string, Tuple<List<BinanceRecentTrade>, decimal, decimal>> Holding = new Dictionary<string, Tuple<List<BinanceRecentTrade>, decimal, decimal>>();
        public double Risk = 90;


        public Moon.Data.Exchanger.BinanceExchanger Service { get; set; }
        public string Jscontainer { get; set; }
        public string TypeOfData { get; set; } = "Accounting";
        public List<BinanceSymbol> Pairs { get; set; }
        public Account(Moon.Data.Exchanger.BinanceExchanger svc)
        {
            this.Service = svc;
            
        }

        public void FreeDust()
        {
            var ndust = this.Service.Client.GetDustLog();
        }

        public void ReportLastOrder()
        {
            var VETbcc = this.Service.Client.GetAllOrders("VETBNB");
            var NEObcc = this.Service.Client.GetAllOrders("NEOBNB");
            var BCCbcc = this.Service.Client.GetAllOrders("BCCBNB");
            var TRXbcc = this.Service.Client.GetAllOrders("TRXBNB");
        }
        public void CloseCurrentOpenOrder()
        {
            var pending = this.Service.Client.GetOpenOrders();
            foreach(var ord in pending.Data)
            {
                var rslt = this.Service.Client.CancelOrder(ord.Symbol,ord.OrderId);
            }

        }

        public double AvailableForOrder(string currency,double risk)
        {
            risk = 100 - risk;
            if (currency.ToUpper() == "ETH") { return (this.ETHAvailable / risk); }
            if (currency.ToUpper() == "BNB") { return (this.BNBAvailable / risk); }
            if (currency.ToUpper() == "BTC") { return (this.BTCAvailable / risk); }
            if (currency.ToUpper() == "USDT") { return (this.USDTAvailable / risk); }
            return double.NaN;
        }
        public double AvailableForOrder(string currency)
        {
            if(currency.ToUpper() == "ETH") { return (this.ETHAvailable / this.Risk); }
            if(currency.ToUpper() == "BNB") { return (this.BNBAvailable / this.Risk); }
            if(currency.ToUpper() == "BTC") { return (this.BTCAvailable / this.Risk); }
            if(currency.ToUpper() == "USDT") { return (this.USDTAvailable / this.Risk); }
            return double.NaN;
        }
        public double CalculateLotSize(double price,string currency,string fullquotename, double risk =0)
        {
            var MySymbolConf = this.Pairs.Where(y => y.Name == fullquotename).First();
            var MinimalLot = MySymbolConf.Filters.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();
            var minNotionalFilter = MySymbolConf.Filters.OfType<BinanceSymbolMinNotionalFilter>().SingleOrDefault();

            var PriceSize = MySymbolConf.Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();
            
            var AdjustedPrice = BinanceHelpers.ClampPrice(PriceSize.MinPrice, PriceSize.MaxPrice, PriceSize.TickSize, price.ChangeType<decimal>());
            var definedsize = (AvailableForOrder(currency) / price).ChangeType<decimal>();
            var AdjustedLotSize = BinanceHelpers.ClampQuantity(MinimalLot.MinQuantity, MinimalLot.MaxQuantity, MinimalLot.StepSize, (AvailableForOrder(currency) / price).ChangeType<decimal>()).ChangeType<double>();
            var Validate = AdjustedLotSize * price> minNotionalFilter.MinNotional.ChangeType<double>();
            if(Validate)
            {
                return AdjustedLotSize;
            }
            else
            {
                return minNotionalFilter.MinNotional.ChangeType<double>();
            }
        }
        public void UpdateAccount()
        {

            var info = this.Service.Client.GetAccountInfo().Data;
            this.Balances = info.Balances.ToList().Where(y => y.Free > 0).ToList().OrderBy(y => y.Free).ToList();
            var converted = this.Balances.ToList().Select(y => y.Total.ChangeType<double>());
            this.Balances.OrderByDescending(y => y.Total);
            this.USDTAvailable = this.Balances.Where(y => (y.Asset.ToUpper() == "USDT")).First().Free.ChangeType<double>();
            this.ETHAvailable = this.Balances.Where(y => (y.Asset.ToUpper() == "ETH")).First().Free.ChangeType<double>();
            this.BTCAvailable = this.Balances.Where(y => (y.Asset.ToUpper() == "BTC")).First().Free.ChangeType<double>();
            this.BCCAvailable = this.Balances.Where(y => (y.Asset.ToUpper() == "BCC")).First().Free.ChangeType<double>();

            this.BNBAvailable = this.Balances.Where(y => (y.Asset.ToUpper() == "BNB")).First().Free.ChangeType<double>();
            this.Pairs = Service.Client.GetExchangeInfo().Data.Symbols.ToList();

            //this.Balances.ForEach(y =>
            //{
            //    if (y.Asset.ToUpper() != "USDT" && y.Asset.ToUpper() != "ETH" && y.Asset.ToUpper() != "BNB" && y.Asset.ToUpper() != "BTC")
            //    {
            //        try
            //        {

            //            var pairdef = this.Pairs.Where(p => p.Name == string.Format("{0}BNB", y.Asset)).First().Filters.OfType<BinanceSymbolLotSizeFilter>().First();

            //            var calculatequantity = BinanceHelpers.ClampQuantity(pairdef.MinQuantity, pairdef.MaxQuantity, pairdef.StepSize, y.Free);
            //            LiquidateFromTo(y.Asset, string.Format("{0}BTC", y.Asset.ToUpper()), calculatequantity);

            //        }
            //        catch
            //        {

            //        }
            //    }
            //});
            //    //if (y.Asset != "BTC")
            //    //{
            //    //    try
            //    //    {
            //    //        var FormatedSymbol = string.Format("{0}BTC", y.Asset.ToUpper());
            //    //        if (y.Asset.ToUpper() == "USDT")
            //    //        {
            //    //            USDTAvailable = y.Free.ChangeType<double>();
            //    //        }
            //    //        var currentprice = this.Service.Client.GetPrice(FormatedSymbol);
            //    //        var trade = this.Service.Client.GetRecentTrades(FormatedSymbol, 2);
            //    //        var Profit = (currentprice.Data.Price - trade.Data.Last().Price) / currentprice.Data.Price;
            //    //        Holding.Add(FormatedSymbol, new Tuple<List<BinanceRecentTrade>, decimal, decimal>(trade.Data.ToList(), currentprice.Data.Price, Profit));

            //    //    }
            //    //    catch (Exception ex)
            //    //    { }

            //    //}
            //    //this.TotalBalance += y.Total;


            //    //Console.WriteLine("Pair {0} - had balance of {1}", y.Asset, y.Free);
            //});
        }
        public void LiquidateFromTo(string SourcePair, string Target, decimal _quantity)
        {
            try
            {


                var Order = this.Service.Client.PlaceOrder(Target, OrderSide.Sell, OrderType.Market, _quantity);
                Console.WriteLine("Liquidation from {0} to {1} - Success = {2} - Error {3}", SourcePair, Target, Order.Success,Order.Error);
            }
            catch (Exception ex)
            {

            }

        }
        public void Buy(IPair SourceCandle, string TargetPair, OrderType TypeOforder, Guid Operation, decimal _quantity = 0 )
        {
            Console.WriteLine("Order Management starting for : {0}",TargetPair);
            try
            {
                var pivotalbuy = SourceCandle.Candle.Close.ChangeType<decimal>();
                var MySymbolConf = this.Pairs.Where(y => y.Name == TargetPair).First();
                var MinimalLot = MySymbolConf.Filters.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();
                var minNotionalFilter = MySymbolConf.Filters.OfType<BinanceSymbolMinNotionalFilter>().SingleOrDefault();

                var PriceSize = MySymbolConf.Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();

                var AdjustedPrice = BinanceHelpers.ClampPrice(PriceSize.MinPrice, PriceSize.MaxPrice, PriceSize.TickSize, pivotalbuy);
                var AdjustedLotSize = (MinimalLot .MinQuantity/ BinanceHelpers.ClampQuantity(MinimalLot.MinQuantity, MinimalLot.MaxQuantity, MinimalLot.StepSize, _quantity));

                while ((_quantity - MinimalLot.MinQuantity) % MinimalLot.StepSize != 0)
                {
                    _quantity += MinimalLot.StepSize;
                }
                var test = (_quantity - MinimalLot.MinQuantity) % MinimalLot.StepSize == 0;

                while((_quantity * AdjustedPrice) < minNotionalFilter.MinNotional)
                {
                    _quantity += MinimalLot.StepSize;

                }

                var Trade = new TradeItem
                {
                    BoughtPrice = AdjustedPrice,
                    Soldprice = 0m,
                    Symbol = TargetPair,
                    OperationID = Operation,
                    State = OrderExecutionStatus.Ready,
                };
                var DenyOperation = ExecutedOrders.Count() > 1 && ExecutedOrders.Where(y => y.OperationID == Operation).ToList().Count() > 1;
                if (TypeOforder == OrderType.Limit &&! DenyOperation)
                {

                    var result = this.Service.Client.PlaceOrder(TargetPair, OrderSide.Buy, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: _quantity 
                       , price: AdjustedPrice);
                    ExecutedOrders.Add(Trade);
                    if (!result.Success)
                    {
                        Console.WriteLine("Order not filled due to : {0}",result.Error.Message);
                        ExecutedOrders.Where(y => y.OperationID == Operation).First().State = OrderExecutionStatus.Error;
                    }
                    else
                    {
                        bool checker = false;
                        int pass = 0;
                        //Update order price
                        ExecutedOrders.Where(y => y.OperationID == Operation).First().BuyLinkedOrder = result.Data;
                        ExecutedOrders.Where(y => y.OperationID == Operation).First().BoughtPrice = AdjustedPrice;
                        ExecutedOrders.Where(y => y.OperationID == Operation).First().OrderID = result.Data.OrderId;
                        ExecutedOrders.Where(y => y.OperationID == Operation).First().State =  OrderExecutionStatus.Submited;
                        while (!checker)
                        {
                            ExecutedOrders.Where(y => y.OperationID == Operation).First().State = OrderExecutionStatus.Waiting;
                            pass++;
                            if (pass > 60) { this.Service.Client.CancelOrder(TargetPair, result.Data.OrderId);
                                ExecutedOrders.Where(y => y.OperationID == Operation).First().State = OrderExecutionStatus.Error;
                                checker = true;
                            }
                            Console.WriteLine("Checking if order filled for pair - {0}", Pairs);
                            var orderrslt = this.Service.Client.QueryOrder(TargetPair, result.Data.OrderId);
                            if (orderrslt.Data.Status == OrderStatus.Filled)
                            {
                                ExecutedOrders.Where(y => y.OperationID == Operation).First().State = OrderExecutionStatus.Filled;
                                Console.WriteLine("Pair {0} - Buy Order passed : {1} at quantity {2} price {3} ", TargetPair, result.Success, _quantity, AdjustedPrice);
                                Console.WriteLine("Order filled");
                                checker = true;
                            }
                            System.Threading.Thread.Sleep(5000);

                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Sell(IPair SourceCandle, string TargetPair, OrderType TypeOforder,Guid Operation, decimal _quantity = 0)
        {
            Console.WriteLine("Order SVC starting for sell : {0}", TargetPair);
            try
            {
                var pivotalbuy = SourceCandle.Candle.Close.ChangeType<decimal>();
                var MySymbolConf = this.Pairs.Where(y => y.Name == TargetPair).First();
                var MinimalLot = MySymbolConf.Filters.OfType<BinanceSymbolLotSizeFilter>().FirstOrDefault();
                var minNotionalFilter = MySymbolConf.Filters.OfType<BinanceSymbolMinNotionalFilter>().SingleOrDefault();

                var PriceSize = MySymbolConf.Filters.OfType<BinanceSymbolPriceFilter>().FirstOrDefault();

                var AdjustedPrice = BinanceHelpers.ClampPrice(PriceSize.MinPrice, PriceSize.MaxPrice, PriceSize.TickSize, pivotalbuy);
                var AdjustedLotSize = (MinimalLot.MinQuantity / BinanceHelpers.ClampQuantity(MinimalLot.MinQuantity, MinimalLot.MaxQuantity, MinimalLot.StepSize, _quantity));

                while ((_quantity - MinimalLot.MinQuantity) % MinimalLot.StepSize != 0)
                {
                    _quantity += MinimalLot.StepSize;
                }
                var test = (_quantity - MinimalLot.MinQuantity) % MinimalLot.StepSize == 0;

                while ((_quantity * AdjustedPrice) < minNotionalFilter.MinNotional)
                {
                    _quantity += MinimalLot.StepSize;

                }

                var cost = AdjustedLotSize * AdjustedPrice;
                if (TypeOforder == OrderType.Limit)
                {

                    var result = this.Service.Client.PlaceOrder(TargetPair, OrderSide.Sell, OrderType.Limit, timeInForce: TimeInForce.GoodTillCancel, quantity: _quantity
                       , price: AdjustedPrice);
                    if (!result.Success)
                    {

                    }
                    else
                    {
                        bool checker = false;
                        int pass = 0;
                        while (!checker )
                        {
                            Console.WriteLine("Checking if order filled for pair - {0}",Pairs);
                            pass++;
                            if (pass > 60)
                            {
                                this.Service.Client.CancelOrder(TargetPair, result.Data.OrderId);
                                //this.Service.Client.PlaceOrder(TargetPair, OrderSide.Sell, OrderType.Market, timeInForce: TimeInForce.GoodTillCancel, quantity: _quantity);
                                checker = true;

                            }
                            var orderrslt = this.Service.Client.QueryOrder(TargetPair, result.Data.OrderId);
                            if (orderrslt.Data.Status == OrderStatus.Filled)
                            {

                                Console.WriteLine("Pair {0} - Sell Order passed test api : {1} at quantity {2} price {3} ", TargetPair, result.Success, _quantity, AdjustedPrice);
                                var SourceOrder = this.ExecutedOrders.Find(y => y.OperationID == Operation);
                                var IndexOfOrder = this.ExecutedOrders.IndexOf(SourceOrder);
                                SourceOrder.Soldprice = AdjustedPrice;
                                SourceOrder.State = OrderExecutionStatus.Submited;
                                SourceOrder.SellLinkedOrder = result.Data;
                                this.ExecutedOrders[IndexOfOrder] = SourceOrder;

                                Console.WriteLine("Order filled");
                                checker = true;
                            }
                            System.Threading.Thread.Sleep(5000);

                        }


                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
    }


}
