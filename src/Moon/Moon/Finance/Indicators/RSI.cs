using Moon.Data.Extender;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Finance.Indicators
{
    public class RSI : RootTA, IIndicator, IIndicatorStats
    {
        public string Name { get; set; } = "RSI";
        public string Author { get; set; } = "Mounir LABAIED";
        public decimal Version { get; set; } = 0.01m;
        public bool MultiTimeFrame { get; set; } = false;
        public bool MultiInput { get; set; } = false;
        public bool NeedExtraData { get; set; } = false;
        public int Period { get; set; } = 12;
        public bool ShowErrors { get; set; } = true;
        public bool OutputData { get; set; } = true;
        public CandlesSeries DataSource { get; set; }
        public List<double> Output { get; set; } = new List<double>();
        public RSI(CandlesSeries DataProvider)
        {
            this.DataSource = DataProvider;
            this.DataSource.CandleUpdate += DataProvider_CandleUpdate;
            Console.WriteLine("Custom TA - Starting :  {0}", this.Name);
        }

        private void DataProvider_CandleUpdate(object sender, CandleEventArg e)
        {
            Console.WriteLine("RSI Receiving data from : {0}",e.ComingCandle.Name);
            Compute();
        }

        public override void Compute()
        {
            if(this.DataSource.HasPeriod(this.Period))
            {

                double sumGain = 0;
                double sumLoss = 0;
                for (int i = 1; i < this.DataSource.Index; i++)
                {
                    var difference = this.DataSource.Low[i] - this.DataSource.Low[i - 1];
                    if (difference >= 0)
                    {
                        sumGain += difference;
                    }
                    else
                    {
                        sumLoss -= difference;
                    }
                }
                var relativeStrength = sumGain / sumLoss;
                var result = 100.0 - (100.0 / (1 + relativeStrength));
                this.Output.Add(result);
                //Console.WriteLine("\tBuilt In  - Extender Mean : {0}", this.DataSource.Low.RemoveNan().Mean());
                //Console.WriteLine("\tBuilt In  - Extender Avg : {0}", this.DataSource.Low.RemoveNan().Average());
                var test = this.DataSource.Close.MaxByPeriod();
                var testl = this.DataSource.Close.MinByPeriod();
                if(this.Output.HasDeviantValues())
                {
                    var Spacer = this.Output.IndexOf(this.Output.RemoveNan().RemoveSpecifiedMinAndMax().Min());

                }
                Console.WriteLine("\tRSI - Result {0}", result);
            }
        }
        public override void FillData()
        {
            base.FillData();
        }
        public override bool HasEnoughData()
        {
            return base.HasEnoughData();
        }
    }
}
