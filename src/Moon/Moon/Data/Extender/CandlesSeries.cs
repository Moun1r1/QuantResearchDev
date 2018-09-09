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
        public List<decimal> Open { get; set; } = new List<decimal>();
        public List<decimal> High { get; set; } = new List<decimal>();
        public List<decimal> Low { get; set; } = new List<decimal>();
        public List<decimal> Close { get; set; } = new List<decimal>();
        public List<decimal> Volume { get; set; } = new List<decimal>();
        public event EventHandler<CandleEventArg> CandleUpdate;
        public int Index {
            get { return this.index; }
            set
            {
                if (value != this.index)
                {
                    this.Index = index;
                    NotifyPropertyChanged();
                }
            }            
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
                        this.Open.Add(DataComing.Candle.Open);
                        this.High.Add(DataComing.Candle.High);
                        this.Low.Add(DataComing.Candle.Low);
                        this.Close.Add(DataComing.Candle.Close);
                        this.Volume.Add(DataComing.Candle.Volume);
                        this.Index = this.Open.Count();
                        this.Candles.Add(DataComing);
                        CandleUpdate?.Invoke(this, new CandleEventArg(CandleEventType.NewCandle,DataComing));
                        Console.WriteLine("Candle Serie SVC - Index updated to : {0}", this.Index);
                        Console.WriteLine("Candle Serie SVC - Built-in Extension 1 : {0}",this.Open.Mean());
                        break;
                }
            }

        }

    }
}