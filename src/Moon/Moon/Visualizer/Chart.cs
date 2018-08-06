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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
using static Moon.Resources.Management;

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
        public ChartValues<ObservableValue> Close { get; set; } = new ChartValues<ObservableValue>();

        public ChartValues<ObservableValue> Buyer { get; set; } = new ChartValues<ObservableValue>();
        public string LastUID = string.Empty;
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

            PlanifiedOperation GetMarketDataOperation = new PlanifiedOperation();
            GetMarketDataOperation.TypeOFApproach = Operation.ForceOperation;
            GetMarketDataOperation.Start = DateTime.Now.AddSeconds(2);
            GetMarketDataOperation.OperationName = "Market Watcher : Get Market Data";
            GetMarketDataOperation.Every = new TimeSpan(0, 5, 0);
            GetMarketDataOperation.ContiniousOperation = true;
            GetMarketDataOperation.OperationCode = new Action(() =>
            {
                Market = new Statistics();
                #region "Load Market Data"
                FormUtils.SetLabelText(BTCMarketCap, string.Format("BTC Market Cap: {0} %", Market.Market.BTCPercentageOfMarketCap));
                FormUtils.SetLabelText(marketupdate, string.Format("Last Update : {0}", DateTime.Now));

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
                    FormUtils.AddListItem(KeyPairsListView, marktitm);


                }
                try
                {
                    FormUtils.SetJaugeText(MarketSent, double.Parse(overallchange.ToString()));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // send to status bar
                }
                #endregion


            });
            GetMarketDataOperation.ContiniousAction = GetMarketDataOperation.OperationCode;
            Global.shared.Manager.ToManage.Add(GetMarketDataOperation);


        }
        private void LoadMarketNews()
        {

            PlanifiedOperation GetMarketNewsOperation = new PlanifiedOperation();
            GetMarketNewsOperation.TypeOFApproach = Operation.ForceOperation;
            GetMarketNewsOperation.Start = DateTime.Now.AddSeconds(2);
            GetMarketNewsOperation.OperationName = "Market Watcher : Get Market News";
            GetMarketNewsOperation.Every = new TimeSpan(0, 5, 0);
            GetMarketNewsOperation.ContiniousOperation = true;
            GetMarketNewsOperation.OperationCode = new Action(() =>
            {
                try
                {
                    FormUtils.ClearListItem(marketnews);
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
                                FormUtils.AddListItem(marketnews, newsitm);

                            }
                            else
                            {
                                string[] row = {
                            source.Name,
                            item.PublishDate.ToString(),
                            item.Title.Text.ToString()
                        };
                                var newsitm = new ListViewItem(row);
                                FormUtils.AddListItem(marketnews, newsitm);

                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception :", ex.Message);
                    //return ex on status box
                }
            });
            GetMarketNewsOperation.ContiniousAction = GetMarketNewsOperation.OperationCode;
            Global.shared.Manager.ToManage.Add(GetMarketNewsOperation);


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
                        new LiveCharts.Wpf.LineSeries
                        {
                            Title = "Buyer",
                            Values = High,
                            StrokeThickness = 1,
                            AreaLimit = 0,

                            PointGeometry = DefaultGeometries.Square,
                            Fill = System.Windows.Media.Brushes.Transparent
                        },
                        new LiveCharts.Wpf.LineSeries
                        {
                            Title = "Seller",
                            StrokeThickness = 2,
                            Values = High,
                            AreaLimit = 0,

                            PointGeometry = DefaultGeometries.Square,
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
                        new LiveCharts.Wpf.LineSeries
                        {
                            Title = "High",
                            Values = High,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        },
                        new LiveCharts.Wpf.LineSeries
                        {
                            Title = "Low",
                            Values = Low,
                            AreaLimit = 0,
                            PointGeometry = null,
                            Fill = System.Windows.Media.Brushes.Transparent
                        },
                         new LiveCharts.Wpf.LineSeries
                        {
                            Title = "Close",
                            Values = Close,
                            PointGeometry = DefaultGeometries.Square,
                            AreaLimit = 0,
                            Fill = System.Windows.Media.Brushes.Transparent
                        }

                    };
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
                    {
                        Interval = 300
                    };
                    timer.Tick += TimerOnTick;
                    //timer.Start();
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
                FormUtils.SetTextBoxContent(textBox1, string.Format("Buyer with : {0} for price {1}" + Environment.NewLine, IncomingBuyer.Quantity.ToString(), IncomingBuyer.Price));
            }
            catch { }
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
                FormUtils.SetTextBoxContent(textBox1, string.Format("Seller with : {0} for price {1}" + Environment.NewLine, IncomingSeller.Quantity.ToString(), IncomingSeller.Price));
                
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
                var IsLast = candle.Properties.Where(y => y.Key.ToString().Contains("Final")).First().Value;
                if (IsLast)
                {
                    listView1.Items.Clear();
                    IncomingBinance.BData.Clear();
                    cartesianChart1.Series.Clear();

                    cartesianChart2.Series[0].Values.Clear(); Buyer.Clear();
                    cartesianChart2.Series[1].Values.Clear(); Seller.Clear();
                    cartesianChart2.Series[2].Values.Clear(); Close.Clear();

                }
                if (candle != null && LastUID != candle.UID)
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
                        
                    

                        //lets only use the last 60 values - To remove by Util function !
                        if (candlesvalues.Count > 60) candlesvalues.RemoveAt(0);
                        if (High.Count > 60) High.RemoveAt(0);
                        if (Low.Count > 60) Low.RemoveAt(0);
                        if (Buyer.Count > 60) Buyer.RemoveAt(0);
                        if (Seller.Count > 60) Seller.RemoveAt(0);
                        if (Close.Count > 60) Close.RemoveAt(0);

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
                        Close.Add(new ObservableValue(double.Parse(candle.Candle.Close.ToString())));
                        cartesianChart2.Series[0].Values = Buyer;
                        cartesianChart2.Series[1].Values = Seller;
                        cartesianChart2.Series[2].Values = Close;

                    LastUID = candle.UID;

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
            FormUtils.AddListItem(listView1, listViewItem);

            if (candle != null )
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

                //Thread Safe Caller for Carte 1
                cartesianChart1.Invoke((MethodInvoker)delegate {
                    cartesianChart1.Series[0].Values = candlesvalues;
                    cartesianChart1.Series[1].Values = High;
                    cartesianChart1.Series[2].Values = Low;
                });



                //lets only use the last 60 values - To remove by Util function !
                if (candlesvalues.Count > 60) candlesvalues.RemoveAt(0);
                if (High.Count > 60) High.RemoveAt(0);
                if (Low.Count > 60) Low.RemoveAt(0);
                if (Buyer.Count > 60) Buyer.RemoveAt(0);
                if (Seller.Count > 60) Seller.RemoveAt(0);
                if (Close.Count > 60) Close.RemoveAt(0);
                solidGauge1.Invoke((MethodInvoker)delegate
                {
                    solidGauge1.Value = candle.Properties.Where(y => y.Key.ToString().Contains("TradeCount")).First().Value;
                    solidGauge1.To = IncomingBinance.BData.Select(y => y.Data.TradeCount).Max();
                });

                solidGauge2.Invoke((MethodInvoker)delegate
                {
                    solidGauge2.Value = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("Volume")).First().Value.ToString());
                    solidGauge2.To = Double.Parse(IncomingBinance.BData.Select(y => y.Data.Volume).Max().ToString());

                });
                Double TakerVolume = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("TakerBuyBaseAssetVolume")).First().Value.ToString());
                Double TotalVolume = Double.Parse(candle.Properties.Where(y => y.Key.ToString().Contains("Volume")).First().Value.ToString());

                solidGauge3.Invoke((MethodInvoker)delegate
                {
                    solidGauge3.Value = TakerVolume;
                    solidGauge3.To = TotalVolume;

                });

                solidGauge4.Invoke((MethodInvoker)delegate
                {
                    solidGauge4.Value = TotalVolume - TakerVolume;
                    solidGauge4.To = TotalVolume;

                });


                Buyer.Add(new ObservableValue(double.Parse(TakerVolume.ToString())));
                Seller.Add(new ObservableValue(double.Parse((TotalVolume - TakerVolume).ToString())));
                Close.Add(new ObservableValue(double.Parse(candle.Candle.Close.ToString())));

                cartesianChart2.Invoke((MethodInvoker)delegate
                {
                    cartesianChart2.Series[0].Values = Buyer;
                    cartesianChart2.Series[1].Values = Seller;
                    //cartesianChart2.Series[2].Values = Close;

                });

                LastUID = candle.UID;

            }


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
            //    KeyPairsListView.Items.Clear();

            //Task.Run(() =>
            //    {
            //        LoadMarketData();
            //    });
        }

        private void DataLoader_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            switch(current.Name)
            { 
                case "tabPage3":
                    break;
                case "tabPage2":
                    break;
               

            }
        }
        public PlotModel PlotModel { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Data_Datestart.Value.ToString() != Data_DateEnd.Value.ToString())
            {
                PlotModel = new PlotModel();
                OxyPlot.Series.LineSeries CSS = new OxyPlot.Series.LineSeries();
                OxyPlot.Series.LineSeries High = new OxyPlot.Series.LineSeries();
                OxyPlot.Series.LineSeries low = new OxyPlot.Series.LineSeries();

                CSS.Title = string.Format("Pair : " + textBox2.Text);
                var data =  IncomingBinance.bclient.Client.GetKlines(textBox2.Text, KlineInterval.OneHour, Data_Datestart.Value, Data_DateEnd.Value, int.MaxValue);

                for(int i =0 ;i < data.Data.Length;i++)
                {
                    CSS.Points.Add(new OxyPlot.DataPoint(i, double.Parse(data.Data[i].Close.ToString())));
                    High.Points.Add(new OxyPlot.DataPoint(i, double.Parse(data.Data[i].High.ToString())));
                    low.Points.Add(new OxyPlot.DataPoint(i, double.Parse(data.Data[i].Low.ToString())));

                    //CSS.Items.Add(new OxyPlot.Series.LineSeries()
                    //{
                    //    X = i,
                    //    Close = double.Parse(data.Data[i].Close.ToString()),
                    //    High = double.Parse(data.Data[i].High.ToString()),
                    //    Low = double.Parse(data.Data[i].Close.ToString()),
                    //    Open = double.Parse(data.Data[i].Close.ToString())
                    //});
                }
                Parallel.ForEach(data.Data, candle =>
                {


                    //Data_Output.Text += candle.Close;
                });
                PlotModel.Series.Add(CSS);
                PlotModel.Series.Add(High);
                PlotModel.Series.Add(low);
                plotView1.Model = PlotModel;

            }
            else
            {
                MessageBox.Show("Starting Date Should not be the same of ending date");
            }
        }

        private void plotView1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            PlanifiedOperation GetAllpairsContent = new PlanifiedOperation();
            GetAllpairsContent.TypeOFApproach = Operation.ForceOperation;
            GetAllpairsContent.Start = DateTime.Now.AddSeconds(5);
            GetAllpairsContent.OperationName = "Core : Get All Binance market candles data";
            GetAllpairsContent.Every = new TimeSpan(00, 05, 00);
            GetAllpairsContent.OperationCode = new Action(() =>
            {
                RunAllPairSuscriber().GetAwaiter().GetResult();

            });
            Global.shared.Manager.ToManage.Add(GetAllpairsContent);

        }

        public async Task RunAllPairSuscriber()
        {
            var data = await IncomingBinance.bclient.Client.GetAllPricesAsync();
            data.Data.ToList().ForEach(y =>
            {
                //y.Price
                //y.Symbol
                FormUtils.SetTextBoxContentMuliLine(textBox4, y.Symbol + Environment.NewLine);
            });
        }
    }

}
