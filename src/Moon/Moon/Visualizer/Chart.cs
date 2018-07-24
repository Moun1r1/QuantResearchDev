using LiveCharts;
using LiveCharts.Wpf;
using Moon.Data.Model;
using Moon.Data.Provider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;

namespace Moon.Visualizer
{
    public partial class Chart : Form
    {
        Random r = new Random();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        DataTable table = new DataTable("TestTable");
        DateTime date = new DateTime(2013, 1, 1);
        IList tableDataSource = null;
        Core IncomingBinance = new Core();

        public Chart()
        {
            InitializeComponent();
        }
        void timer_Tick(object sender, EventArgs e)
        {
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Core IncomingBinance = new Core();
            IncomingBinance.SubscribeTo("BTCUSDT");
            IncomingBinance.SubscribeTo("ETHBTC");

            IncomingBinance.Candles.CollectionChanged += Candles_CollectionChanged;
        }
        delegate void DelegateInCandle(DateTime date,decimal open,decimal close);

        private void Candles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var candle = (BinanceCandle)e.NewItems[0];
            string[] row = {candle.Name, candle.CollectedDate.ToLongTimeString(), candle.Candle.Open.ToString(), candle.Candle.Close.ToString(),
                candle.Candle.Low.ToString(),
                candle.Candle.High.ToString(),
                candle.Candle.Volume.ToString()
            };
            var listViewItem = new ListViewItem(row);
            listView1.Items.Add(listViewItem);


        }
    }

}
