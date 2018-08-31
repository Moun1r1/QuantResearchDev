using Moon.Data.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Provider
{
    public static class CandleExtension
    {
        public static double Dpivot (this Trady.Core.Candle candle)
        {
            double Pivot = (double.Parse(candle.High.ToString()) + double.Parse(candle.Low.ToString()) + double.Parse(candle.Open.ToString())) / 3;
            return Pivot;
        }
        public static double DOpen(this Trady.Core.Candle candle)
        {
            return candle.Open.ChangeType<double>();
        }
        public static double DClose(this Trady.Core.Candle candle)
        {
            return candle.Close.ChangeType<double>();
        }
        public static double DHigh(this Trady.Core.Candle candle)
        {
            return candle.High.ChangeType<double>();
        }
        public static double DLow(this Trady.Core.Candle candle)
        {
            return candle.Low.ChangeType<double>();
        }
        public static double DVolume(this Trady.Core.Candle candle)
        {
            return candle.Volume.ChangeType<double>();
        }
    }
}
