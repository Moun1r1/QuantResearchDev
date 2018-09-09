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
        public List<double> Open { get; set; } = new List<double>();
        public List<double> High { get; set; } = new List<double>();
        public List<double> Low { get; set; } = new List<double>();
        public List<double> Close { get; set; } = new List<double>();
        public List<double> Volume { get; set; } = new List<double>();
        public List<double> Pivot { get; set; } = new List<double>();
        public event EventHandler<CandleEventArg> CandleUpdate;
        public int Index {
            get;set;        
        }
        public TimeRange RangeOfTime { get; set; }
        public ObservableCollection<IPair> DataSource { get; set; } = new ObservableCollection<IPair>();
        public CandleCollectionMode Mode { get; set; } = CandleCollectionMode.AllTicks;
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

        private void DataComing(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var DataComing = (IPair)e.NewItems[0];

                switch (this.Mode)
                {
                    case CandleCollectionMode.Consolidated:
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
}