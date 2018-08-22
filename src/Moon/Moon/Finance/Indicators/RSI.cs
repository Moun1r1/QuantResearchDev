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
        public bool ShowErrors { get; set; } = true;
        public bool OutputData { get; set; } = true;
        public ObservableCollection<Pair> Data = new ObservableCollection<Pair>();
        public RSI()
        {
            Console.WriteLine("Custom TA - Starting :  {0}", this.Name);
        }
        public override void Compute()
        {
            base.Compute();
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
