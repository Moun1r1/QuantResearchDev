using Binance.Net.Objects;
using Moon.Data.Exchanger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Provider
{
    public enum ProviderMode
    {
        CandleOnly,
        AllTicks,
        TicksAndTrades,
        All
    }
    class Core
    {

        public ObservableCollection<BinanceStreamKlineData> BData { get; set; } = new ObservableCollection<BinanceStreamKlineData>();
        public binance bclient { get; set; } = new binance();
        public ProviderMode Mode { get; set; } = ProviderMode.All;
        public Core()
        {
            BData.CollectionChanged += BData_CollectionChanged;

        }

        private void BData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //Data caching and moving to Azure Data Table logic goes here 
            switch(e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var Candle = (BinanceStreamKlineData)e.NewItems[0];
                    Console.WriteLine("Binance Receiving data from : {0}", Candle.Symbol);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        public void SubscribeTo(string Pair)
        {
            Task.Run(() =>
            {
                var tick = this.bclient.Socket.SubscribeToKlineStreamAsync(Pair, KlineInterval.OneMinute, (data) =>
                {
                    BData.Add(data);
                    Console.WriteLine("Data Open : {0}", data.Data.Open);
                });
                while (true)
                {
                    System.Threading.Thread.Sleep(500);
                }
            });



        }

    }
}
