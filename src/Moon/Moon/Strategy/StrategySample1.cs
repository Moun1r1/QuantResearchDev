using Moon.Data.Bacher;
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
        public int RiskRatioMinimal { get; set; } = 0;
        public override Signals GetSignal()
        {
            return base.GetSignal();
        }
    }
}
