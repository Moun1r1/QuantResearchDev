using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public enum SignalSource
    {
        External,
        TA,
        MarketAndTA,
        CustomPeriod,
        ALL
    }
    public interface ISignalProvider
    {
         SignalSource Providers { get; set; }
    }
}
