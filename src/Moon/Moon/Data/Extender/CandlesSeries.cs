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
    public class CandlesSeries : ICandleFactory, INotifyPropertyChanged
    {
        private int index = 0;
        public List<double> PercentChange { get; set; } = new List<double>();
        public List<double> Open { get; set; } = new List<double>();
        public List<double> High { get; set; } = new List<double>();
        public List<double> Low { get; set; } = new List<double>();
        public List<double> Close { get; set; } = new List<double>();
        public List<double> Volume { get; set; } = new List<double>();
        public List<double> Pivot { get; set; } = new List<double>();
        public List<double> Min { get; set; } = new List<double>();
        public IndexedCandle IndexedCandle { get; set; }
        public List<double> TALow { get; set; } = new List<double>();
        public List<double> TAHigh { get; set; } = new List<double>();
        public Dictionary<int, List<KeyValuePair<string, bool>>> DetectedPatterns { get; set; } = new Dictionary<int, List<KeyValuePair<string, bool>>>();
        public event EventHandler<CandleEventArg> CandleUpdate;
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

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //Raise event on new index for TA folow ?
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void ConnectBinance(BinanceProvier Source)
        {
            Source.Candles.CollectionChanged += DataComing;
        }
        private void GenerateLow()
        {
            var LowData = new LowestLow(this.Candles.Select(y => y.Candle), 12).Compute();
            if(LowData.Last().Tick != null)
            {
                this.TALow.Add(LowData.Last().Tick.ChangeType<double>());
                if(this.TALow.Count() >1)
                {
                    switch (this.TALow.HasChange())
                    {
                        ///Raise signal
                        case List.GenericChangeType.Down:
                            break;
                        case List.GenericChangeType.Up:
                            break;
                        case List.GenericChangeType.Same:
                            break;
                    }
                }
            }
        }
        private void GenerateHigh()
        {
            var HighData = new HighestHigh(this.Candles.Select(y => y.Candle), 12).Compute();
            if (HighData.Last().Tick != null)
            {
                this.TAHigh.Add(HighData.Last().Tick.ChangeType<double>());
                if (this.TAHigh.Count() > 1)
                {
                    switch(this.High.HasChange())
                    {
                        ///Raise signal
                        case List.GenericChangeType.Down:
                            break;
                        case List.GenericChangeType.Up:
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
                var AnyOfPatternTrue = DetectedPatterns[Index].Where(y => y.Value == true);
                Console.WriteLine("Candle close: {0}", Close[Index -1]);
                foreach (var psignalin in AnyOfPatternTrue)
                {
                    Console.WriteLine("Detected Pattern : {0}", psignalin.Key);
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
                            CandleUpdate?.Invoke(this, new CandleEventArg(CandleEventType.NewCandle, DataComing));
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
                        CandleUpdate?.Invoke(this, new CandleEventArg(CandleEventType.NewCandle,DataComing));
                        break;
                }
            }

        }

    }

    ///

}