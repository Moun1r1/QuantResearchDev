using Binance.Net.Objects;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using Moon.Data.Model;
using Moon.Data.Provider;
using Moon.MarketWatcher;
using Moon.Visualizer.Winforms.Cartesian.ConstantChanges;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;
using System.Xml;

namespace Moon.Visualizer
{

    namespace Winforms.Cartesian.ConstantChanges
    {
        public class MeasureModel
        {
            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
            public double Open { get; set; }
        }
    }


    public partial class Chart : Form
    {
        public Statistics Market;
        private ObservableValue value1;
        public ChartValues<ObservableValue> High { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Low { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Buyer { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Seller { get; set; } = new ChartValues<ObservableValue>();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Core IncomingBinance = new Core();
        private ChartValues<OhlcPoint> candlesvalues = new ChartValues<OhlcPoint>();
        public LiveCharts.SeriesCollection SeriesCollection { get; set; }
        public Chart()
        {
            InitializeComponent();
        }
        private void LoadMarketData()
        {
            Market = new Statistics();
            #region "Load Market Data"
            SetLabelText(BTCMarketCap, string.Format("BTC Market Cap: {0} %", Market.Market.BTCPercentageOfMarketCap));
            SetLabelText(marketupdate, string.Format("Last Update : {0}", DateTime.Now)) ;

            decimal overallchange = 0;
            foreach (var pair in Market.KeyPairsCapital)
            {
                string[] row = {
                    pair.Symbol,
                    pair.PriceUsd.ToString(),
                    pair.PercentChange1h.ToString(),
                    pair.PercentChange24h.ToString(),
                    pair.PercentChange7d.ToString(),
                    pair.Rank.ToString(),
                    pair.MarketCapUsd.Value.ToString("N"),
                };
                overallchange += decimal.Parse(pair.PercentChange1h.Value.ToString());
                var marktitm = new ListViewItem(row);
                AddListItem(KeyPairsListView, marktitm);


            }
            try
            {
                SetJaugeText(MarketSent, double.Parse(overallchange.ToString()));
                //MarketSent.Value = double.Parse(overallchange.ToString());

            }
            catch { }
            #endregion


        }
        private void LoadMarketNews()
        {
            try
            {
                foreach (var source in Moon.Global.shared.Config.NewsSource)
                {
                    string url = source.Uri;
                    XmlReader reader = XmlReader.Create(url);
                    SyndicationFeed feed = SyndicationFeed.Load(reader);
                    reader.Close();
                    foreach (SyndicationItem item in feed.Items)
                    {

                        if (source.LoadSummary)
                        {
                            string[] row = {
                            source.Name,
                            item.PublishDate.ToString(),
                            item.Title.Text.ToString(),
                            item.Summary.Text.ToString()
                        };
                            var newsitm = new ListViewItem(row);
                            AddListItem(marketnews, newsitm);

                        }
                        else
                        {
                            string[] row = {
                            source.Name,
                            item.PublishDate.ToString(),
                            item.Title.Text.ToString()
                        };
                            var newsitm = new ListViewItem(row);
                            AddListItem(marketnews, newsitm);

                        }

                    }

                }

            }
            catch(Exception ex)
            {
                //return ex on status box
            }



        }
        private void Chart_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Task.Run(() =>
            {
                LoadMarketNews();
                LoadMarketData();

            });
            #region "Load Socket Data"
            cartesianChart2.Series = new LiveCharts.SeriesCollection
                    {
                        new LineSeries
                        {
                            Title = "Buyer",
                            Values = High,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        },
                        new LineSeries
                        {
                            Title = "Seller",
                            Values = High,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        }
                    };
                    cartesianChart1.Series = new LiveCharts.SeriesCollection
                    {
                        new OhlcSeries
                        {
                            Title = "BTCUSDT",
                            Values = candlesvalues
                        },
                        new LineSeries
                        {
                            Title = "High",
                            Values = High,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        },
                        new LineSeries
                        {
                            Title = "Low",
                            Values = Low,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        }

                    };
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
                    {
                        Interval = 500
                    };
                    timer.Tick += TimerOnTick;
                    timer.Start();
                    IncomingBinance.SubscribeTo("BTCUSDT");
                    IncomingBinance.Candles.CollectionChanged += Candles_CollectionChanged;
                    IncomingBinance.BDataTradeSeller.CollectionChanged += BDataTradeSeller_CollectionChanged;
                    IncomingBinance.BDataTradeBuyer.CollectionChanged += BDataTradeBuyer_CollectionChanged;
        #endregion

        }

        private void BDataTradeBuyer_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var IncomingBuyer = (BinanceStreamTrade)e.NewItems[0];
            try
            {
                SetTextBoxContent(textBox1, string.Format("Buyer with : {0} for price {1}" + Environment.NewLine, IncomingBuyer.Quantity.ToString(), IncomingBuyer.Price));
            }
            catch { }
        }

        public void AddListItem(ListView List,ListViewItem item)
        {
            if (List.InvokeRequired)

            {

                List.Invoke((MethodInvoker)delegate {List.Items.Add(item); });

            }

            else

            {

                List.Items.Add(item);
            }


        }

        public void SetTextBoxContent(TextBox Target,string Value)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Text = Value; });

            }

            else

            {

                Target.Text = Value;
            }

        }

        public void SetLabelText(Label Target,string content)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Text = content; });

            }

            else

            {

                Target.Text = content;
            }

        }

        public void SetJaugeText(SolidGauge Target, double content)
        {
            if (Target.InvokeRequired)

            {

                Target.Invoke((MethodInvoker)delegate { Target.Value = content; });

            }

            else

            {

                Target.Value = content;
            }

        }


        private void BDataTradeSeller_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
            }
            var IncomingSeller = (BinanceStreamTrade)e.NewItems[0];
            try
            {
                SetTextBoxContent(textBox1, string.Format("Seller with : {0} for price {1}" + Environment.NewLine, IncomingSeller.Quantity.ToString(), IncomingSeller.Price));
                
            }
            catch { }
        }

        private void SetAxisLimits(System.DateTime now)
        {
            cartesianChart1.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 100ms ahead
            cartesianChart1.AxisX[0].MinValue = now.Ticks - TimeSpan.FromSeconds(8).Ticks; //we only care about the last 8 seconds
        }

        delegate void DelegateInCandle(DateTime date,decimal open,decimal close);

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            var now = System.DateTime.Now;
            try
            {
                var candle = IncomingBinance.Candles.Last();
                if (candle != null)
                {
                    candlesvalues.Add(new OhlcPoint
                    {
                        Close = double.Parse(candle.Candle.Close.ToString()),
                        Open = double.Parse(candle.Candle.Open.ToString()),
                        High = double.Parse(candle.Candle.High.ToString()),
                        Low = double.Parse(candle.Candle.Low.ToString())

                    });
                    High.Add(new ObservableValue(double.Parse(candle.Candle.High.ToString())));
                    Low.Add(new ObservableValue(double.Parse(candle.Candle.Low.ToString())));

                    cartesianChart1.Series[0].Values = candlesvalues;
                    cartesianChart1.Series[1].Values = High;
                    cartesianChart1.Series[2].Values = Low;
                    //lets only use the last 30 values
                    if (candlesvalues.Count > 60) candlesvalues.RemoveAt(0);
                    if (High.Count > 60) High.RemoveAt(0);
                    if (Low.Count > 60) Low.RemoveAt(0);
                    if (Buyer.Count > 60) Buyer.RemoveAt(0);
                    if (Seller.Count > 60) Seller.RemoveAt(0);

                }
                solidGauge1.Value = candle.Properties.Where(y => y.Key.ToString().Contains("TradeCount")).First().Value;
                solidGauge1.To = IncomingBinance.BData.Select(y => y.Data.TradeCount).Max();

                solidGauge2.Value = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("Volume")).First().Value.ToString());
                solidGauge2.To = Double.Parse(IncomingBinance.BData.Select(y => y.Data.Volume).Max().ToString());

                Double TakerVolume = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("TakerBuyBaseAssetVolume")).First().Value.ToString());
                Double TotalVolume = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("Volume")).First().Value.ToString());
                solidGauge3.Value = TakerVolume;
                solidGauge3.To = TotalVolume;

                solidGauge4.Value = TotalVolume - TakerVolume;
                solidGauge4.To = TotalVolume;

                Buyer.Add(new ObservableValue(double.Parse(TakerVolume.ToString())));
                Seller.Add(new ObservableValue(double.Parse((TotalVolume - TakerVolume).ToString())));
                cartesianChart2.Series[0].Values = Buyer;
                cartesianChart2.Series[1].Values = Seller;



                if (candle.Properties.Where(y => y.Key.ToString().Contains("Final")).First().Value == true)
                {
                    listView1.Items.Clear();
                    IncomingBinance.BData.Clear();
                }

            }
            catch
            {

            }
        }



        private void Candles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var candle = (BinanceCandle)e.NewItems[0];
            string[] row = {candle.Name, candle.CollectedDate.ToLongTimeString(), candle.Candle.Open.ToString(), candle.Candle.Close.ToString(),
                candle.Candle.Low.ToString(),
                candle.Candle.High.ToString(),
                candle.Candle.Volume.ToString()
            };
            var listViewItem = new ListViewItem(row);
            if (candle.Candle.Open < candle.Candle.Close) { listViewItem.ForeColor = System.Drawing.Color.Green; }
            else { listViewItem.ForeColor = System.Drawing.Color.Green; }
            AddListItem(listView1, listViewItem);



        }



        private void solidGauge2_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void MarketRefresh_Tick(object sender, EventArgs e)
        {
                KeyPairsListView.Items.Clear();

            Task.Run(() =>
                {
                    LoadMarketData();

                });
        }
    }

}
