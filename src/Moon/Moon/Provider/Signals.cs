using Moon.Data.Extender;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
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


    public class SignalsOrder : Signals , IRoot, ISignalProvider
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
        public double Profit { get; set; }
        public double Volume { get; set; }
        public CandlesSeries DataProvider { get; set; }
        public SignalsOrder(CandlesSeries _source)
        {
            this.DataProvider = _source;
        }
        public void Buy()
        {

        }
        public void Sell()
        {

            this.BuyPrice = 0;
            this.SellPrice = 0;
            this.HasBought = false;

        }

        public new void Update()
        {
            if(this.HasBought)
            {
                ReportProfit();
            }
        }
        public void ReportProfit()
        {
            var profit = (this.DataProvider.Close.Last()) - this.BuyPrice / this.BuyPrice;
            Console.WriteLine("Has bought at :{0} price is :{1}", this.BuyPrice, this.DataProvider.Close.Last());
            Console.WriteLine("Profit : {0}", profit);

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
