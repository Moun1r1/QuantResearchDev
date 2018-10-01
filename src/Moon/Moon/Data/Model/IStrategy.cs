using Moon.Data.Bacher;
using Moon.Data.Model;
using Moon.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public enum StrategyType
    {
        Mono,
        Multi,
        Market
    }

    /// <summary>
    /// Extra default period needed
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class StratTimeRangeOptionnal : Attribute
    {
        public TimeRange PeriodNeed { get; set; }
        public StratTimeRangeOptionnal(TimeRange time)
        {
            this.PeriodNeed = time;
        }
    }




    /// <summary>
    /// Type of Strategy
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class DefaultStrategyConf : Attribute
    {
        public StrategyType StratyType { get; set; }
        public string Name { get; set;}
        public Exchange Exchanger { get; set; }
        public bool AutoStart { get; set; } = false;
        public string Revision { get; set; } = "v1";
        public SignalSource SignalFrom { get; set; }
        public DefaultStrategyConf(StrategyType _StratType,Exchange platform,string name,bool autostart, SignalSource getfrom)
        {
            this.StratyType = _StratType;
            this.Name = name;
            this.Exchanger = Exchange.Binance;
            this.AutoStart = autostart;
            this.SignalFrom = getfrom;
        }
        

    }


    public class StrategyCore
    {

        public virtual Signals GetSignal()
        {
            return new Signals();
        }
        public virtual Signals Calculate()
        {
            return new Signals();
        }
        public virtual void DataIn(IPair Candle)
        {

        }
    }
    public interface IStrategy 
    {
        string Name { get; set; }
        double RiskRatioMinimal { get; set; }
    }
}
