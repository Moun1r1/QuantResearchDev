using Binance.Net.Objects;
using Moon.Data.Extender;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Accounting
{
    /// <summary>
    /// Root Accounting Class
    /// </summary>
    public class Account
    {
        public Exchange Platform { get; set; } = Exchange.Binance;
        public string Name { get; set; } = "Unknown";
        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public decimal TotalBalance { get; set; } = decimal.MinValue;

        public List<BinanceBalance> Balances { get; set; } = new List<BinanceBalance>();

        public Dictionary<string, Tuple<List<BinanceRecentTrade>, decimal, decimal>> Holding = new Dictionary<string, Tuple<List<BinanceRecentTrade>, decimal, decimal>>();



        public Moon.Data.Exchanger.BinanceExchanger Service { get; set; }

        public Account(Moon.Data.Exchanger.BinanceExchanger svc)
        {
            this.Service = svc;
        }
        public void UpdateAccount()
        {
            
            var info = this.Service.Client.GetAccountInfo().Data;
            this.Balances = info.Balances.ToList().Where(y => y.Free > 0).ToList();
            var converted = this.Balances.ToList().Select(y => y.Total.ChangeType<double>());
            this.Balances.OrderByDescending(y => y.Total);
            this.Balances.ForEach(y =>
            {
                //if(y.Asset != "BTC")
                //{
                //    try
                //    {
                //        var FormatedSymbol = string.Format("{0}BTC", y.Asset.ToUpper());
                //        var currentprice = this.Service.Client.GetPrice(FormatedSymbol);
                //        var trade = this.Service.Client.GetRecentTrades(FormatedSymbol, 2);
                //        var Profit = (currentprice.Data.Price - trade.Data.Last().Price) / currentprice.Data.Price;
                //        Holding.Add(FormatedSymbol, new Tuple<List<BinanceRecentTrade>, decimal, decimal>(trade.Data.ToList(), currentprice.Data.Price, Profit));

                //    }
                //    catch(Exception ex)
                //    { }

                //}
                //this.TotalBalance += y.Total;
               

                Console.WriteLine("Pair {0} - had balance of {1}", y.Asset, y.Free);
            });
        }
    }


}
