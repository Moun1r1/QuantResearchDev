﻿namespace Moon.Visualizer
{
    partial class Chart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DataLoader = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.PairStr = new System.Windows.Forms.TextBox();
            this.PairName = new System.Windows.Forms.Button();
            this.cartesianChart3 = new LiveCharts.WinForms.CartesianChart();
            this.label8 = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.solidGauge4 = new LiveCharts.WinForms.SolidGauge();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.label3 = new System.Windows.Forms.Label();
            this.solidGauge3 = new LiveCharts.WinForms.SolidGauge();
            this.label2 = new System.Windows.Forms.Label();
            this.solidGauge2 = new LiveCharts.WinForms.SolidGauge();
            this.label1 = new System.Windows.Forms.Label();
            this.solidGauge1 = new LiveCharts.WinForms.SolidGauge();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.marketnews = new System.Windows.Forms.ListView();
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.marketupdate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MarketSent = new LiveCharts.WinForms.SolidGauge();
            this.KeyPairsListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BTCMarketCap = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.plotView1 = new OxyPlot.WindowsForms.PlotView();
            this.Data_Output = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Data_DateEnd = new System.Windows.Forms.DateTimePicker();
            this.Data_Datestart = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mostliquidbnb = new System.Windows.Forms.Label();
            this.mostliquidusdt = new System.Windows.Forms.Label();
            this.mostliquideth = new System.Windows.Forms.Label();
            this.mostliquidbtc = new System.Windows.Forms.Label();
            this.binancebnbpairmove = new System.Windows.Forms.Label();
            this.binanceusdtpairmove = new System.Windows.Forms.Label();
            this.binanceethpairmove = new System.Windows.Forms.Label();
            this.binancebtcpairmove = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.MarketRefresh = new System.Windows.Forms.Timer(this.components);
            this.DataLoader.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataLoader
            // 
            this.DataLoader.Controls.Add(this.tabPage1);
            this.DataLoader.Controls.Add(this.tabPage2);
            this.DataLoader.Controls.Add(this.tabPage3);
            this.DataLoader.Controls.Add(this.tabPage4);
            this.DataLoader.Controls.Add(this.tabPage5);
            this.DataLoader.Controls.Add(this.tabPage6);
            this.DataLoader.Controls.Add(this.tabPage7);
            this.DataLoader.Dock = System.Windows.Forms.DockStyle.Left;
            this.DataLoader.Location = new System.Drawing.Point(0, 0);
            this.DataLoader.Name = "DataLoader";
            this.DataLoader.SelectedIndex = 0;
            this.DataLoader.Size = new System.Drawing.Size(1743, 1057);
            this.DataLoader.TabIndex = 13;
            this.DataLoader.SelectedIndexChanged += new System.EventHandler(this.DataLoader_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.checkBox2);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.PairStr);
            this.tabPage1.Controls.Add(this.PairName);
            this.tabPage1.Controls.Add(this.cartesianChart3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.PriceLabel);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.solidGauge4);
            this.tabPage1.Controls.Add(this.cartesianChart2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.solidGauge3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.solidGauge2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.solidGauge1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Live WebSocket";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1281, 511);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 34;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(1156, 540);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 17);
            this.checkBox3.TabIndex = 33;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(1156, 517);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 17);
            this.checkBox2.TabIndex = 32;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1157, 494);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // PairStr
            // 
            this.PairStr.Location = new System.Drawing.Point(1156, 467);
            this.PairStr.Name = "PairStr";
            this.PairStr.Size = new System.Drawing.Size(100, 20);
            this.PairStr.TabIndex = 30;
            this.PairStr.Text = "BTCUSDT";
            // 
            // PairName
            // 
            this.PairName.Location = new System.Drawing.Point(1281, 465);
            this.PairName.Name = "PairName";
            this.PairName.Size = new System.Drawing.Size(75, 23);
            this.PairName.TabIndex = 29;
            this.PairName.Text = "Start";
            this.PairName.UseVisualStyleBackColor = true;
            this.PairName.Click += new System.EventHandler(this.button3_Click);
            // 
            // cartesianChart3
            // 
            this.cartesianChart3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cartesianChart3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart3.Location = new System.Drawing.Point(8, 6);
            this.cartesianChart3.Name = "cartesianChart3";
            this.cartesianChart3.Size = new System.Drawing.Size(1142, 597);
            this.cartesianChart3.TabIndex = 28;
            this.cartesianChart3.Text = "cartesianChart3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label8.Location = new System.Drawing.Point(1272, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 26);
            this.label8.TabIndex = 27;
            this.label8.Text = "Real Time Price : ";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLabel.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.PriceLabel.Location = new System.Drawing.Point(1463, 6);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(24, 26);
            this.PriceLabel.TabIndex = 26;
            this.PriceLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(1178, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 26);
            this.label4.TabIndex = 24;
            this.label4.Text = "Seller  Volume";
            // 
            // solidGauge4
            // 
            this.solidGauge4.Location = new System.Drawing.Point(1156, 276);
            this.solidGauge4.Name = "solidGauge4";
            this.solidGauge4.Size = new System.Drawing.Size(200, 100);
            this.solidGauge4.TabIndex = 23;
            this.solidGauge4.Text = "solidGauge4";
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cartesianChart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart2.Location = new System.Drawing.Point(0, 609);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(1150, 165);
            this.cartesianChart2.TabIndex = 21;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(1436, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 26);
            this.label3.TabIndex = 20;
            this.label3.Text = "Buyer  Volume";
            // 
            // solidGauge3
            // 
            this.solidGauge3.Location = new System.Drawing.Point(1414, 276);
            this.solidGauge3.Name = "solidGauge3";
            this.solidGauge3.Size = new System.Drawing.Size(200, 100);
            this.solidGauge3.TabIndex = 19;
            this.solidGauge3.Text = "solidGauge3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(1178, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Volume Intensity";
            // 
            // solidGauge2
            // 
            this.solidGauge2.Location = new System.Drawing.Point(1156, 100);
            this.solidGauge2.Name = "solidGauge2";
            this.solidGauge2.Size = new System.Drawing.Size(200, 100);
            this.solidGauge2.TabIndex = 17;
            this.solidGauge2.Text = "solidGauge2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(1436, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 26);
            this.label1.TabIndex = 16;
            this.label1.Text = "Trade Intensity";
            // 
            // solidGauge1
            // 
            this.solidGauge1.Location = new System.Drawing.Point(1414, 100);
            this.solidGauge1.Name = "solidGauge1";
            this.solidGauge1.Size = new System.Drawing.Size(200, 100);
            this.solidGauge1.TabIndex = 15;
            this.solidGauge1.Text = "solidGauge1";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.Location = new System.Drawing.Point(0, 784);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1718, 251);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Symbol";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Open";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Close";
            this.columnHeader5.Width = 108;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Low";
            this.columnHeader6.Width = 113;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "High";
            this.columnHeader7.Width = 136;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Volume";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.marketnews);
            this.tabPage2.Controls.Add(this.marketupdate);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.MarketSent);
            this.tabPage2.Controls.Add(this.KeyPairsListView);
            this.tabPage2.Controls.Add(this.BTCMarketCap);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Market Watcher";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(1520, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 26);
            this.label7.TabIndex = 6;
            this.label7.Text = "Market News";
            // 
            // marketnews
            // 
            this.marketnews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18});
            this.marketnews.Location = new System.Drawing.Point(1053, 282);
            this.marketnews.Name = "marketnews";
            this.marketnews.Size = new System.Drawing.Size(673, 528);
            this.marketnews.TabIndex = 5;
            this.marketnews.UseCompatibleStateImageBehavior = false;
            this.marketnews.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Source";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Date";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Title";
            this.columnHeader17.Width = 464;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Summary";
            this.columnHeader18.Width = 81;
            // 
            // marketupdate
            // 
            this.marketupdate.AutoSize = true;
            this.marketupdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marketupdate.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.marketupdate.Location = new System.Drawing.Point(8, 242);
            this.marketupdate.Name = "marketupdate";
            this.marketupdate.Size = new System.Drawing.Size(129, 26);
            this.marketupdate.TabIndex = 4;
            this.marketupdate.Text = "Last Update";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(8, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 26);
            this.label6.TabIndex = 3;
            this.label6.Text = "Market 1 Hr Sentiment";
            // 
            // MarketSent
            // 
            this.MarketSent.Location = new System.Drawing.Point(13, 124);
            this.MarketSent.Name = "MarketSent";
            this.MarketSent.Size = new System.Drawing.Size(200, 100);
            this.MarketSent.TabIndex = 2;
            // 
            // KeyPairsListView
            // 
            this.KeyPairsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.KeyPairsListView.Location = new System.Drawing.Point(8, 282);
            this.KeyPairsListView.Name = "KeyPairsListView";
            this.KeyPairsListView.Size = new System.Drawing.Size(1039, 528);
            this.KeyPairsListView.TabIndex = 1;
            this.KeyPairsListView.UseCompatibleStateImageBehavior = false;
            this.KeyPairsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Symbol";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Price";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "1hrchange";
            this.columnHeader10.Width = 77;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "24hrchange";
            this.columnHeader11.Width = 81;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "7dchange";
            this.columnHeader12.Width = 78;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Rank";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "MarketCapUsd";
            this.columnHeader14.Width = 104;
            // 
            // BTCMarketCap
            // 
            this.BTCMarketCap.AutoSize = true;
            this.BTCMarketCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTCMarketCap.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.BTCMarketCap.Location = new System.Drawing.Point(8, 20);
            this.BTCMarketCap.Name = "BTCMarketCap";
            this.BTCMarketCap.Size = new System.Drawing.Size(211, 26);
            this.BTCMarketCap.TabIndex = 0;
            this.BTCMarketCap.Text = "BTC Market Cap : %";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.plotView1);
            this.tabPage3.Controls.Add(this.Data_Output);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.Data_DateEnd);
            this.tabPage3.Controls.Add(this.Data_Datestart);
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DataLoader";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // plotView1
            // 
            this.plotView1.Location = new System.Drawing.Point(401, 33);
            this.plotView1.Name = "plotView1";
            this.plotView1.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView1.Size = new System.Drawing.Size(965, 618);
            this.plotView1.TabIndex = 9;
            this.plotView1.Text = "plotView1";
            this.plotView1.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView1.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView1.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotView1.Click += new System.EventHandler(this.plotView1_Click);
            // 
            // Data_Output
            // 
            this.Data_Output.Location = new System.Drawing.Point(9, 158);
            this.Data_Output.Multiline = true;
            this.Data_Output.Name = "Data_Output";
            this.Data_Output.Size = new System.Drawing.Size(200, 246);
            this.Data_Output.TabIndex = 8;
            this.Data_Output.Text = "BTCUSDT";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(9, 86);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "BTCUSDT";
            // 
            // Data_DateEnd
            // 
            this.Data_DateEnd.Location = new System.Drawing.Point(9, 60);
            this.Data_DateEnd.Name = "Data_DateEnd";
            this.Data_DateEnd.Size = new System.Drawing.Size(200, 20);
            this.Data_DateEnd.TabIndex = 6;
            // 
            // Data_Datestart
            // 
            this.Data_Datestart.Location = new System.Drawing.Point(9, 33);
            this.Data_Datestart.Name = "Data_Datestart";
            this.Data_Datestart.Size = new System.Drawing.Size(200, 20);
            this.Data_Datestart.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "BTCUSDT";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Services Management";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 334);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Collector";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(0, 105);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(656, 194);
            this.textBox4.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(581, 305);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Binance Market Stats";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mostliquidbnb);
            this.groupBox2.Controls.Add(this.mostliquidusdt);
            this.groupBox2.Controls.Add(this.mostliquideth);
            this.groupBox2.Controls.Add(this.mostliquidbtc);
            this.groupBox2.Controls.Add(this.binancebnbpairmove);
            this.groupBox2.Controls.Add(this.binanceusdtpairmove);
            this.groupBox2.Controls.Add(this.binanceethpairmove);
            this.groupBox2.Controls.Add(this.binancebtcpairmove);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.groupBox2.Location = new System.Drawing.Point(8, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1377, 424);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Binance market";
            // 
            // mostliquidbnb
            // 
            this.mostliquidbnb.AutoSize = true;
            this.mostliquidbnb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostliquidbnb.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.mostliquidbnb.Location = new System.Drawing.Point(22, 383);
            this.mostliquidbnb.Name = "mostliquidbnb";
            this.mostliquidbnb.Size = new System.Drawing.Size(204, 26);
            this.mostliquidbnb.TabIndex = 15;
            this.mostliquidbnb.Text = "Load Data Collector";
            // 
            // mostliquidusdt
            // 
            this.mostliquidusdt.AutoSize = true;
            this.mostliquidusdt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostliquidusdt.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.mostliquidusdt.Location = new System.Drawing.Point(22, 332);
            this.mostliquidusdt.Name = "mostliquidusdt";
            this.mostliquidusdt.Size = new System.Drawing.Size(204, 26);
            this.mostliquidusdt.TabIndex = 14;
            this.mostliquidusdt.Text = "Load Data Collector";
            // 
            // mostliquideth
            // 
            this.mostliquideth.AutoSize = true;
            this.mostliquideth.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostliquideth.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.mostliquideth.Location = new System.Drawing.Point(22, 279);
            this.mostliquideth.Name = "mostliquideth";
            this.mostliquideth.Size = new System.Drawing.Size(204, 26);
            this.mostliquideth.TabIndex = 13;
            this.mostliquideth.Text = "Load Data Collector";
            // 
            // mostliquidbtc
            // 
            this.mostliquidbtc.AutoSize = true;
            this.mostliquidbtc.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mostliquidbtc.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.mostliquidbtc.Location = new System.Drawing.Point(22, 229);
            this.mostliquidbtc.Name = "mostliquidbtc";
            this.mostliquidbtc.Size = new System.Drawing.Size(204, 26);
            this.mostliquidbtc.TabIndex = 12;
            this.mostliquidbtc.Text = "Load Data Collector";
            // 
            // binancebnbpairmove
            // 
            this.binancebnbpairmove.AutoSize = true;
            this.binancebnbpairmove.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binancebnbpairmove.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.binancebnbpairmove.Location = new System.Drawing.Point(22, 190);
            this.binancebnbpairmove.Name = "binancebnbpairmove";
            this.binancebnbpairmove.Size = new System.Drawing.Size(204, 26);
            this.binancebnbpairmove.TabIndex = 11;
            this.binancebnbpairmove.Text = "Load Data Collector";
            // 
            // binanceusdtpairmove
            // 
            this.binanceusdtpairmove.AutoSize = true;
            this.binanceusdtpairmove.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binanceusdtpairmove.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.binanceusdtpairmove.Location = new System.Drawing.Point(22, 139);
            this.binanceusdtpairmove.Name = "binanceusdtpairmove";
            this.binanceusdtpairmove.Size = new System.Drawing.Size(204, 26);
            this.binanceusdtpairmove.TabIndex = 10;
            this.binanceusdtpairmove.Text = "Load Data Collector";
            // 
            // binanceethpairmove
            // 
            this.binanceethpairmove.AutoSize = true;
            this.binanceethpairmove.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binanceethpairmove.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.binanceethpairmove.Location = new System.Drawing.Point(22, 86);
            this.binanceethpairmove.Name = "binanceethpairmove";
            this.binanceethpairmove.Size = new System.Drawing.Size(204, 26);
            this.binanceethpairmove.TabIndex = 9;
            this.binanceethpairmove.Text = "Load Data Collector";
            // 
            // binancebtcpairmove
            // 
            this.binancebtcpairmove.AutoSize = true;
            this.binancebtcpairmove.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binancebtcpairmove.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.binancebtcpairmove.Location = new System.Drawing.Point(22, 36);
            this.binancebtcpairmove.Name = "binancebtcpairmove";
            this.binancebtcpairmove.Size = new System.Drawing.Size(204, 26);
            this.binancebtcpairmove.TabIndex = 8;
            this.binancebtcpairmove.Text = "Load Data Collector";
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // MarketRefresh
            // 
            this.MarketRefresh.Enabled = true;
            this.MarketRefresh.Interval = 30000;
            this.MarketRefresh.Tick += new System.EventHandler(this.MarketRefresh_Tick);
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1717, 1057);
            this.Controls.Add(this.DataLoader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Chart";
            this.Text = "Moon - Visualizer";
            this.Load += new System.EventHandler(this.Chart_Load);
            this.DataLoader.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl DataLoader;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView KeyPairsListView;
        private System.Windows.Forms.Label BTCMarketCap;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label label6;
        private LiveCharts.WinForms.SolidGauge MarketSent;
        private System.Windows.Forms.Timer MarketRefresh;
        private System.Windows.Forms.Label marketupdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView marketnews;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker Data_DateEnd;
        private System.Windows.Forms.DateTimePicker Data_Datestart;
        private System.Windows.Forms.TextBox Data_Output;
        private System.Windows.Forms.TextBox textBox3;
        private OxyPlot.WindowsForms.PlotView plotView1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage1;
        private LiveCharts.WinForms.CartesianChart cartesianChart3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label label4;
        private LiveCharts.WinForms.SolidGauge solidGauge4;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private System.Windows.Forms.Label label3;
        private LiveCharts.WinForms.SolidGauge solidGauge3;
        private System.Windows.Forms.Label label2;
        private LiveCharts.WinForms.SolidGauge solidGauge2;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.SolidGauge solidGauge1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox PairStr;
        private System.Windows.Forms.Button PairName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label mostliquidbnb;
        private System.Windows.Forms.Label mostliquidusdt;
        private System.Windows.Forms.Label mostliquideth;
        private System.Windows.Forms.Label mostliquidbtc;
        private System.Windows.Forms.Label binancebnbpairmove;
        private System.Windows.Forms.Label binanceusdtpairmove;
        private System.Windows.Forms.Label binanceethpairmove;
        private System.Windows.Forms.Label binancebtcpairmove;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
    }
}