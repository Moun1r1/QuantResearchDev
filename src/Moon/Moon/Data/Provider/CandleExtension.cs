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
    }
}
