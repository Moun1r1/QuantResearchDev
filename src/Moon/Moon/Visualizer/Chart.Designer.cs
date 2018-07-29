namespace Moon.Visualizer
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.solidGauge4 = new LiveCharts.WinForms.SolidGauge();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.label3 = new System.Windows.Forms.Label();
            this.solidGauge3 = new LiveCharts.WinForms.SolidGauge();
            this.label2 = new System.Windows.Forms.Label();
            this.solidGauge2 = new LiveCharts.WinForms.SolidGauge();
            this.label1 = new System.Windows.Forms.Label();
            this.solidGauge1 = new LiveCharts.WinForms.SolidGauge();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
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
            this.MarketRefresh = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1743, 1057);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.solidGauge4);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.cartesianChart2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.solidGauge3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.solidGauge2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.solidGauge1);
            this.tabPage1.Controls.Add(this.cartesianChart1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1735, 1031);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Live WebSocket";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(1313, 534);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 26);
            this.label5.TabIndex = 25;
            this.label5.Text = "Live Orders";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(1282, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 26);
            this.label4.TabIndex = 24;
            this.label4.Text = "Seller  Volume";
            // 
            // solidGauge4
            // 
            this.solidGauge4.Location = new System.Drawing.Point(1260, 225);
            this.solidGauge4.Name = "solidGauge4";
            this.solidGauge4.Size = new System.Drawing.Size(200, 100);
            this.solidGauge4.TabIndex = 23;
            this.solidGauge4.Text = "solidGauge4";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1090, 574);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(628, 204);
            this.textBox1.TabIndex = 22;
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.BackColor = System.Drawing.SystemColors.Menu;
            this.cartesianChart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart2.Location = new System.Drawing.Point(0, 609);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(1067, 165);
            this.cartesianChart2.TabIndex = 21;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(1540, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 26);
            this.label3.TabIndex = 20;
            this.label3.Text = "Buyer  Volume";
            // 
            // solidGauge3
            // 
            this.solidGauge3.Location = new System.Drawing.Point(1518, 225);
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
            this.label2.Location = new System.Drawing.Point(1282, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 26);
            this.label2.TabIndex = 18;
            this.label2.Text = "Volume Intensity";
            // 
            // solidGauge2
            // 
            this.solidGauge2.Location = new System.Drawing.Point(1260, 49);
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
            this.label1.Location = new System.Drawing.Point(1540, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 26);
            this.label1.TabIndex = 16;
            this.label1.Text = "Trade Intensity";
            // 
            // solidGauge1
            // 
            this.solidGauge1.Location = new System.Drawing.Point(1518, 49);
            this.solidGauge1.Name = "solidGauge1";
            this.solidGauge1.Size = new System.Drawing.Size(200, 100);
            this.solidGauge1.TabIndex = 15;
            this.solidGauge1.Text = "solidGauge1";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.BackColor = System.Drawing.SystemColors.Menu;
            this.cartesianChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart1.Location = new System.Drawing.Point(0, 0);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1067, 603);
            this.cartesianChart1.TabIndex = 14;
            this.cartesianChart1.Text = "cartesianChart1";
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
            this.label7.Location = new System.Drawing.Point(1294, 242);
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
            this.columnHeader17.Width = 77;
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
            this.ClientSize = new System.Drawing.Size(1742, 1057);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Chart";
            this.Text = "Moon - Visualizer";
            this.Load += new System.EventHandler(this.Chart_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private LiveCharts.WinForms.SolidGauge solidGauge4;
        private System.Windows.Forms.TextBox textBox1;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private System.Windows.Forms.Label label3;
        private LiveCharts.WinForms.SolidGauge solidGauge3;
        private System.Windows.Forms.Label label2;
        private LiveCharts.WinForms.SolidGauge solidGauge2;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.SolidGauge solidGauge1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
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
    }
}