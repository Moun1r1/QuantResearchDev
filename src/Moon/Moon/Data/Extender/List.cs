using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Extender
{
    public static class List
    {
        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static double Mean(this List<double> values, int start, int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }


        public static ObservableCollection<T> Purge<T>(this ObservableCollection<T> list)
        {
            if (list.Count() > Moon.Global.shared.MaxLiveListTick)
            {
                list.ToList().RemoveRange(Global.shared.MaxLiveListTick -1, list.Count());
            }

            return list;
        }

        public static List<T> Purge<T>(this List<T> list)
        {
            if(list.Count() > Moon.Global.shared.MaxLiveListTick)
            {
                list.RemoveRange(Global.shared.MaxLiveListTick, list.Count() - 1);
            }

            return list;
        }

        public static decimal Mean(this List<decimal> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }

        public static decimal Mean(this List<decimal> values, int start, int end)
        {
            decimal s = 0;

            for (int i = start; i < end; i++)
            {
                s += values[i];
            }

            return s / (end - start);
        }

    }
}
