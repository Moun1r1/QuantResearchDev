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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.solidGauge1 = new LiveCharts.WinForms.SolidGauge();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.solidGauge2 = new LiveCharts.WinForms.SolidGauge();
            this.label3 = new System.Windows.Forms.Label();
            this.solidGauge3 = new LiveCharts.WinForms.SolidGauge();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.solidGauge4 = new LiveCharts.WinForms.SolidGauge();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            this.listView1.Location = new System.Drawing.Point(12, 784);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1718, 265);
            this.listView1.TabIndex = 0;
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
            // cartesianChart1
            // 
            this.cartesianChart1.BackColor = System.Drawing.SystemColors.Menu;
            this.cartesianChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart1.Location = new System.Drawing.Point(12, 0);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1067, 603);
            this.cartesianChart1.TabIndex = 1;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // solidGauge1
            // 
            this.solidGauge1.Location = new System.Drawing.Point(1530, 49);
            this.solidGauge1.Name = "solidGauge1";
            this.solidGauge1.Size = new System.Drawing.Size(200, 100);
            this.solidGauge1.TabIndex = 2;
            this.solidGauge1.Text = "solidGauge1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Location = new System.Drawing.Point(1552, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Trade Intensity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(1294, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Volume Intensity";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // solidGauge2
            // 
            this.solidGauge2.Location = new System.Drawing.Point(1272, 49);
            this.solidGauge2.Name = "solidGauge2";
            this.solidGauge2.Size = new System.Drawing.Size(200, 100);
            this.solidGauge2.TabIndex = 4;
            this.solidGauge2.Text = "solidGauge2";
            this.solidGauge2.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.solidGauge2_ChildChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label3.Location = new System.Drawing.Point(1552, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "Buyer  Volume";
            // 
            // solidGauge3
            // 
            this.solidGauge3.Location = new System.Drawing.Point(1530, 225);
            this.solidGauge3.Name = "solidGauge3";
            this.solidGauge3.Size = new System.Drawing.Size(200, 100);
            this.solidGauge3.TabIndex = 6;
            this.solidGauge3.Text = "solidGauge3";
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.BackColor = System.Drawing.SystemColors.Menu;
            this.cartesianChart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cartesianChart2.Location = new System.Drawing.Point(12, 609);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(1067, 165);
            this.cartesianChart2.TabIndex = 8;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1102, 574);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(628, 204);
            this.textBox1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label4.Location = new System.Drawing.Point(1294, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "Seller  Volume";
            // 
            // solidGauge4
            // 
            this.solidGauge4.Location = new System.Drawing.Point(1272, 225);
            this.solidGauge4.Name = "solidGauge4";
            this.solidGauge4.Size = new System.Drawing.Size(200, 100);
            this.solidGauge4.TabIndex = 10;
            this.solidGauge4.Text = "solidGauge4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(1325, 534);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 26);
            this.label5.TabIndex = 12;
            this.label5.Text = "Live Orders";
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1742, 1061);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.solidGauge4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cartesianChart2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.solidGauge3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.solidGauge2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.solidGauge1);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.listView1);
            this.Name = "Chart";
            this.Text = "Chart";
            this.Load += new System.EventHandler(this.Chart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private LiveCharts.WinForms.SolidGauge solidGauge1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private LiveCharts.WinForms.SolidGauge solidGauge2;
        private System.Windows.Forms.Label label3;
        private LiveCharts.WinForms.SolidGauge solidGauge3;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private LiveCharts.WinForms.SolidGauge solidGauge4;
        private System.Windows.Forms.Label label5;
    }
}