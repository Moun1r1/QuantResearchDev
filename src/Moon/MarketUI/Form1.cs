using Moon.MarketWatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Moon.Resources.Management;
using Moon.Global;
namespace MarketUI
{
    public partial class Form1 : Form
    {
        public Statistics Market = new Statistics();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PlanifiedOperation GetAllpairsContent = new PlanifiedOperation();
            //GetAllpairsContent.TypeOFApproach = Operation.ForceOperation;
            //GetAllpairsContent.Start = DateTime.Now.AddSeconds(5);
            //GetAllpairsContent.OperationName = "Core : Get All Binance market candles data";
            //GetAllpairsContent.Every = new TimeSpan(00, 05, 00);
            //GetAllpairsContent.OperationCode = new Action(() =>
            //{

            Statistics CurrentMarketSts = new Statistics();
            CurrentMarketSts.ConnectBinance(Moon.Global.Shared.IncomingBinance);
           

            //Task.Run(() =>
            //{
            //    RunAllPairSuscriber();
            //});

            //});
            //Shared.Manager.ToManage.Add(GetAllpairsContent);
            //Shared.IncomingBinance.RegisterAllMarket();
            //Market.SetAllBinancePairWatcher(Shared.IncomingBinance);
        }
        public async Task RunAllPairSuscriber()
        {
            while(true)
            {
                var data = await Shared.IncomingBinance.bclient.Client.GetAllPricesAsync();
                data.Data.ToList().ForEach(y =>
                {
                    //y.Price
                    //y.Symbol
                    //FormUtils.SetTextBoxContentMuliLine(textBox4, y.Symbol + Environment.NewLine);
                    ListViewItem itm;
                    listView1.Invoke((MethodInvoker)delegate
                    {
                        var found = false;
                        if(listView1.Items.Count > 1)
                        {
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Text == y.Symbol)
                                {
                                    found = true;
                                    var indexof = listView1.Items.IndexOf(item);
                                    if(double.Parse(listView1.Items[indexof].SubItems[1].Text.ToString()) < double.Parse(y.Price.ToString()))
                                    {
                                        listView1.Items[indexof].SubItems[1].Text = y.Price.ToString();
                                        listView1.Items[indexof].BackColor = Color.Green;

                                    }
                                    else
                                    {
                                        listView1.Items[indexof].SubItems[1].Text = y.Price.ToString();
                                        listView1.Items[indexof].BackColor = Color.Red;

                                    }

                                }
                            }
                        }


                        if(!found)
                        {
                            var newlstitm = new ListViewItem();
                            newlstitm.Text = y.Symbol;
                            newlstitm.SubItems.Add(y.Price.ToString());
                            listView1.Items.Add(newlstitm);



                        }
                    });


                });
                System.Threading.Thread.Sleep(new TimeSpan(0, 1, 0));

            }

        }
        private void LoadMarketTA()
        {
            //PlanifiedOperation GetMarketDataOperation = new PlanifiedOperation();
            //GetMarketDataOperation.TypeOFApproach = Operation.ForceOperation;
            //GetMarketDataOperation.Start = DateTime.Now.AddSeconds(2);
            //GetMarketDataOperation.OperationName = "Market Watcher : Update Average TA";
            //GetMarketDataOperation.Every = new TimeSpan(0, 0, 10);
            //GetMarketDataOperation.ContiniousOperation = true;
            //GetMarketDataOperation.OperationCode = new Action(() =>
            //{
            //    FormUtils.SetLabelText(binancebnbpairmove, string.Format("BNB Average Pairs Move: {0:P2}", Market.binancebnbpairmove.ToString()));
            //    FormUtils.SetLabelText(binancebtcpairmove, string.Format("BTC Average Pairs Move: {0:P2}", Market.binancebtcpairmove.ToString()));
            //    FormUtils.SetLabelText(binanceethpairmove, string.Format("ETH Average Pairs Move: {0:P2}", Market.binanceethpairmove.ToString()));
            //    FormUtils.SetLabelText(binanceusdtpairmove, string.Format("USDT Average Pairs Move: {0:P2}", Market.binanceusdtpairmove.ToString()));

            //    if (Market.binance_mostliquid_btc.Count() > 0)
            //    {
            //        FormUtils.SetLabelText(mostliquidbtc, string.Format("BTC Most Active : {0}", Market.binance_mostliquid_btc.First()));
            //        FormUtils.SetLabelText(mostliquideth, string.Format("ETH Most Active: {0}", Market.binance_mostliquid_eth.First()));
            //        FormUtils.SetLabelText(mostliquidusdt, string.Format("USDT Most Active: {0}", Market.binance_mostliquid_usdt.First()));
            //        FormUtils.SetLabelText(mostliquidbnb, string.Format("BNB Most Active: {0}", Market.binance_mostliquid_bnb.First()));

            //    }




            //});
            //GetMarketDataOperation.ContiniousAction = GetMarketDataOperation.OperationCode;
            //Global.Shared.Manager.ToManage.Add(GetMarketDataOperation);


        }


    }
}
