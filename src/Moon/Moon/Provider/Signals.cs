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
        Median,
        Leave,
        Keep,
        ExitNow,
        ExitOnIdeal,
        ExitOnLessWorst
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
