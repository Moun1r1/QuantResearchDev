
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public enum RunTradingType
    {
        Paper,
        Backtest,
        Live
    }
    public class TradingLogicAttribute : Attribute
    {
        public RunTradingType RunKind { get; set; }
        public TradingLogicAttribute(RunTradingType _kind)
        {
            this.RunKind = _kind;
        }
    }
    public interface ITradingType
    {

    }
}
