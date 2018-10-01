using Moon.Data.Bacher;
using Moon.Data.Model;
using Moon.Data.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Analysis.Indicator;
using Trady.Core.Period;

namespace Moon.Data.Extender
{
    public enum CandleCollectionMode
    {
        Consolidated,
        AllTicks
    }
    public interface ICandleFactory
    {
        CandleCollectionMode Mode { get; set; }
    }
    public class CandlesSeries : ICandleFactory
    {
        public List<double> PercentChange { get; set; } = new List<double>();
        public List<double> Open { get; set; } = new List<double>();
        public List<double> High { get; set; } = new List<double>();
        public List<double> Low { get; set; } = new List<double>();
        public List<double> Close { get; set; } = new List<double>();
        public List<double> Volume { get; set; } = new List<double>();
        public List<double> Pivot { get; set; } = new List<double>();
        public int DefaultPatternPeriod { get; set; } = 12;
        public int DefaultPatternLongPeriod { get; set; } = 24;

        public List<double> Min { get; set; } = new List<double>();
        public IndexedCandle IndexedCandle { get; set; }
        public List<double> LowestLow { get; set; } = new List<double>();
        public List<double> HighestHigh { get; set; } = new List<double>();
        public int IndexLowestLow { get; set; } = 0;
        public int IndexHighestHigh { get; set; } = 0;

        public Dictionary<int, List<KeyValuePair<string, bool>>> DetectedPatterns { get; set; } = new Dictionary<int, List<KeyValuePair<string, bool>>>();
        public event EventHandler<CandleEventArg> CandleUpdate;
        public event EventHandler<PatternEvent> PatternUpdate;
        public int Index {
            get;set;        
        }
        public TimeRange RangeOfTime { get; set; }
        public ObservableCollection<IPair> DataSource { get; set; } = new ObservableCollection<IPair>();
        public CandleCollectionMode Mode { get; set; } = CandleCollectionMode.Consolidated;
        public List<IPair> Candles { get; set; } = new List<IPair>();

        public CandlesSeries()
        {
        }
        public void ConnectBinance(BinanceProvier Source)
        {
            Source.Candles.CollectionChanged += DataComing;
        }
        private void GenerateLow()
        {
            var LowData = new LowestLow(this.Candles.Where(y=> y.Last == true).Select(y => y.Candle), 12).Compute();
            if(LowData.Last().Tick != null)
            {
                this.LowestLow.Add(LowData.Last().Tick.ChangeType<double>());
                if(this.LowestLow.Count() >1)
                {
                    switch (this.LowestLow.HasChange())
                    {
                        ///Raise signal and update index
                        case List.GenericChangeType.Down:
                            IndexLowestLow = this.LowestLow.Count() - 1;
                            break;
                        case List.GenericChangeType.Up:
                            IndexLowestLow = this.LowestLow.Count() - 1;
                            break;
                        case List.GenericChangeType.Same:
                            break;
                    }
                }
            }
        }
        private void GenerateHigh()
        {
            var HighData = new HighestHigh(this.Candles.Where(y => y.Last == true).Select(y => y.Candle), 12).Compute();
            if (HighData.Last().Tick != null)
            {
                this.HighestHigh.Add(HighData.Last().Tick.ChangeType<double>());
                if (this.HighestHigh.Count() > 1)
                {
                    switch(this.High.HasChange())
                    {
                        ///Raise signal and update index
                        case List.GenericChangeType.Down:
                            IndexHighestHigh = this.HighestHigh.Count() - 1;
                            break;
                        case List.GenericChangeType.Up:
                            IndexHighestHigh = this.HighestHigh.Count() - 1;
                            break;
                        case List.GenericChangeType.Same:
                            break;
                    }

                }
            }
        }
        private void GeneratePattern()
        {
            if(Index > 2)
            {


                if(DefaultPatternLongPeriod < Index -1)
                {
                    DetectedPatterns.Add(Index, new List<KeyValuePair<string, bool>>()
                    {
                        new KeyValuePair<string, bool>("IsBearish",this.IndexedCandle.IsBearish()),
                        new KeyValuePair<string, bool>("IsBullish",this.IndexedCandle.IsBullish()),
                        new KeyValuePair<string, bool>("IsAccumDistBearish",this.IndexedCandle.IsAccumDistBearish()),
                        new KeyValuePair<string, bool>("IsAccumDistBullish",this.IndexedCandle.IsAccumDistBullish()),
                        new KeyValuePair<string, bool>("IsBreakingHistoricalHighestClose",this.IndexedCandle.IsBreakingHistoricalHighestClose()),
                        new KeyValuePair<string, bool>("IsBreakingHistoricalHighestHigh",this.IndexedCandle.IsBreakingHistoricalHighestHigh()),
                        new KeyValuePair<string, bool>("IsBreakingHistoricalLowestLow",this.IndexedCandle.IsBreakingHistoricalLowestLow()),
                        new KeyValuePair<string, bool>("IsBreakingHistoricalLowestClose",this.IndexedCandle.IsBreakingHistoricalLowestClose()),
                        new KeyValuePair<string, bool>("IsObvBullish",this.IndexedCandle.IsObvBullish()),
                        new KeyValuePair<string, bool>("IsObvBearish",this.IndexedCandle.IsObvBearish()),
                        new KeyValuePair<string, bool>("IsRsiOverbought",this.IndexedCandle.IsRsiOverbought(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsRsiOversold",this.IndexedCandle.IsRsiOversold(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsSmaBullishCross",this.IndexedCandle.IsSmaBullishCross(DefaultPatternPeriod,DefaultPatternLongPeriod)),
                        new KeyValuePair<string, bool>("IsSmaBearishCross",this.IndexedCandle.IsSmaBearishCross(DefaultPatternPeriod,DefaultPatternLongPeriod)),
                        new KeyValuePair<string, bool>("IsEmaBullish",this.IndexedCandle.IsEmaBullish(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsEmaBearish",this.IndexedCandle.IsEmaBearish(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsAboveEma",this.IndexedCandle.IsAboveEma(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsBelowEma",this.IndexedCandle.IsBelowEma(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsEmaBullishCross",this.IndexedCandle.IsEmaBullishCross(DefaultPatternPeriod,DefaultPatternLongPeriod)),
                        new KeyValuePair<string, bool>("IsEmaBearishCross",this.IndexedCandle.IsEmaBearishCross(DefaultPatternPeriod,DefaultPatternLongPeriod)),
                        new KeyValuePair<string, bool>("IsBreakingHighestHighPeriod",this.IndexedCandle.IsBreakingHighestHigh(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsBreakingHighestClosePeriod",this.IndexedCandle.IsBreakingHighestClose (DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsBreakingLowestLowPeriod",this.IndexedCandle.IsBreakingLowestLow(DefaultPatternPeriod)),
                        new KeyValuePair<string, bool>("IsBreakingLowestClosePeriod",this.IndexedCandle.IsBreakingLowestClose (DefaultPatternPeriod)),

                    });
                }
                else
                {
                    DetectedPatterns.Add(Index, new List<KeyValuePair<string, bool>>()
                {
                    new KeyValuePair<string, bool>("IsBearish",this.IndexedCandle.IsBearish()),
                    new KeyValuePair<string, bool>("IsBullish",this.IndexedCandle.IsBullish()),
                    new KeyValuePair<string, bool>("IsAccumDistBearish",this.IndexedCandle.IsAccumDistBearish()),
                    new KeyValuePair<string, bool>("IsAccumDistBullish",this.IndexedCandle.IsAccumDistBullish()),
                    new KeyValuePair<string, bool>("IsBreakingHistoricalHighestClose",this.IndexedCandle.IsBreakingHistoricalHighestClose()),
                    new KeyValuePair<string, bool>("IsBreakingHistoricalHighestHigh",this.IndexedCandle.IsBreakingHistoricalHighestHigh()),
                    new KeyValuePair<string, bool>("IsBreakingHistoricalLowestLow",this.IndexedCandle.IsBreakingHistoricalLowestLow()),
                    new KeyValuePair<string, bool>("IsBreakingHistoricalLowestClose",this.IndexedCandle.IsBreakingHistoricalLowestClose()),
                    new KeyValuePair<string, bool>("IsObvBullish",this.IndexedCandle.IsObvBullish()),
                    new KeyValuePair<string, bool>("IsObvBearish",this.IndexedCandle.IsObvBearish()),

                });
                }

                //Isolate true cases (false are still available by object copy on event args)
                var AnyOfPatternTrue = DetectedPatterns[Index].Where(y => y.Value == true);

                //Send signal
                foreach (var psignalin in AnyOfPatternTrue)
                {
                    switch(psignalin.Key)
                    {
                        case "IsBearish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBearish, this.Candles.Last(), this));
                            break;
                        case "IsBullish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBullish, this.Candles.Last(), this));
                            break;
                        case "IsAccumDistBearish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsAccumDistBearish, this.Candles.Last(), this));
                            break;
                        case "IsAccumDistBullish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsAccumDistBullish, this.Candles.Last(), this));
                            break;
                        case "IsBreakingHistoricalHighestClose":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBreakingHistoricalHighestClose, this.Candles.Last(), this));
                            break;
                        case "IsBreakingHistoricalHighestHigh":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBreakingHistoricalHighestHigh, this.Candles.Last(), this));
                            break;
                        case "IsBreakingHistoricalLowestLow":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBreakingHistoricalLowestLow, this.Candles.Last(), this));
                            break;
                        case "IsBreakingHistoricalLowestClose":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsBreakingHistoricalLowestClose, this.Candles.Last(), this));
                            break;
                        case "IsObvBullish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsObvBullish, this.Candles.Last(), this));
                            break;
                        case "IsObvBearish":
                            PatternUpdate?.Invoke(this, new PatternEvent(PatternType.IsObvBearish, this.Candles.Last(), this));
                            break;

                    }
                }
            }


        }
        private void DataComing(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var DataComing = (IPair)e.NewItems[0];
                var IsFinal = DataComing.Properties.Select(y => y.Key == "Final").First();

                switch (this.Mode)
                {
                    case CandleCollectionMode.Consolidated:
                        if(DataComing.Last)
                        {
                            this.Open.Add(DataComing.Candle.Open.ChangeType<double>());
                            this.High.Add(DataComing.Candle.High.ChangeType<double>());
                            this.Low.Add(DataComing.Candle.Low.ChangeType<double>());
                            this.Close.Add(DataComing.Candle.Close.ChangeType<double>());
                            this.Volume.Add(DataComing.Candle.Volume.ChangeType<double>());
                            this.Pivot.Add(DataComing.Candle.Dpivot().ChangeType<double>());
                            this.Index = this.Open.Count();
                            this.Candles.Add(DataComing);
                            CandleUpdate?.Invoke(this, new CandleEventArg(CandleEventType.NewCandle, DataComing,this));
                            this.IndexedCandle = new IndexedCandle(this.Candles.Select(y => y.Candle), this.Index - 1);
                            GenerateLow();
                            GenerateHigh();
                            GeneratePattern();
                        }
                        //if final candle
                        break;
                    case CandleCollectionMode.AllTicks:
                        
                        this.Open.Add(DataComing.Candle.Open.ChangeType<double>());
                        this.High.Add(DataComing.Candle.High.ChangeType<double>());
                        this.Low.Add(DataComing.Candle.Low.ChangeType<double>());
                        this.Close.Add(DataComing.Candle.Close.ChangeType<double>());
                        this.Volume.Add(DataComing.Candle.Volume.ChangeType<double>());
                        this.Pivot.Add(DataComing.Candle.Dpivot().ChangeType<double>());
                        this.Index = this.Open.Count();
                        this.Candles.Add(DataComing);
                        CandleUpdate?.Invoke(this, new CandleEventArg(CandleEventType.NewCandle,DataComing,this));
                        break;
                }
            }

        }

    }

    ///

}