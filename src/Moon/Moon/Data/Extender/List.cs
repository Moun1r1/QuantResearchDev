using Moon.Data.Model;
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
        public static bool HasPeriod(this CandlesSeries values, int period)
        {
            return (values.Index >= period);
        }
        public static double Mean(this List<double> values)
        {
            return values.Count == 0 ? 0 : values.Mean(0, values.Count);
        }
        public static void Sort<T>(this IList<T> list) where T : IComparable
        {
            Sort(list, 0, list.Count - 1);
        }

        public static void Sort<T>(this IList<T> list,
               int startIndex, int endIndex) where T : IComparable
        {
            for (int i = startIndex; i < endIndex; i++)
                for (int j = endIndex; j > i; j--)
                    if (list[j].IsLesserThan(list[j - 1]))
                        list.Exchange(j, j - 1);
        }

        private static void Exchange<T>(this IList<T> list, int index1, int index2)
        {
            T tmp = list[index1];
            list[index1] = list[index2];
            list[index2] = tmp;
        }

        private static bool IsLesserThan(this IComparable value, IComparable item)
        {
            return value.CompareTo(item) < 0;
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
            var temp = list;
            try
            {
                if (temp.Count() > Moon.Global.Shared.MaxLiveListTick)
                {
                    for (int i = Global.Shared.MaxLiveListTick; i < temp.Count(); i++)
                    {
                        temp.RemoveAt(0);
                    }
                }

            }
            catch (Exception ex) { }


            return temp;
        }

        public static List<T> Purge<T>(this List<T> list)
        {
            if (list.Count() > Moon.Global.Shared.MaxLiveListTick)
            {
                for (int i = Global.Shared.MaxLiveListTick; i < list.Count(); i++)
                {
                    list.RemoveAt(0);
                }
            }

            return list;

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
