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
    public class TypeOfStrat : Attribute
    {
        public StrategyType StratyType { get; set; }
        public TypeOfStrat(StrategyType _StratType)
        {
            this.StratyType = _StratType;
        }
        

    }
    [TypeOfStrat(StrategyType.Mono)]
    [StratTimeRangeOptionnal(TimeRange.Minute5)]
    public class MyStrategyTest : StrategyCore , IStrategy
    {
        public string Name { get; set; } = "Sample";
        public int RiskRatioMinimal { get; set; } = 0;
        public override Signals GetSignal()
        {
            return base.GetSignal();
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
        int RiskRatioMinimal { get; set; }
    }
}
