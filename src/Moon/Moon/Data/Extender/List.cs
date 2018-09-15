using Binance.Net.Objects;
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
        //TDS
        public enum OrderBookUpdateType
        {
            PriceChange,
            QuantityChange,
            NoChange
        }
        public enum GenericChangeType
        {
            Up,
            Down,
            Same,
            Unknown
        }

        public static GenericChangeType HasChange(this List<double> input)
        {
            var index = input.Count();
            var currentvalue = input[index - 1];
            var minusvalue = input[index -2];
            if(minusvalue > currentvalue)
            {
                return GenericChangeType.Down;
            }
            if (minusvalue < currentvalue)
            {
                return GenericChangeType.Up;
            }
            if(minusvalue == currentvalue) { return GenericChangeType.Same; }
            return GenericChangeType.Unknown;
        }

        public static List<double> GetLastRSI(this List<double> input,int period = 12)
        {
            List<double> output = new List<double>();
            if (input.Count > period)
            {

                double sumGain = 0;
                double sumLoss = 0;
                for (int i = 1; i < input.Count; i++)
                {
                    var difference = input[i] - input[i - 1];
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
                output.Add(result);
            }

            return output;
        }
        public static double GetQuantityChange(this BinanceOrderBookEntry input, BinanceOrderBookEntry compareto)
        {
            return ((input.Quantity.ChangeType<double>() - compareto.Quantity.ChangeType<double>()) / compareto.Quantity.ChangeType<double>() * 100);
        }
        public static double GetPriceChange(this BinanceOrderBookEntry input, BinanceOrderBookEntry compareto)
        {
            return ((input.Price.ChangeType<double>() - compareto.Price.ChangeType<double>()) / compareto.Price.ChangeType<double>() * 100);
        }
        public static OrderBookUpdateType CompareWith(this BinanceOrderBookEntry input, BinanceOrderBookEntry compareto)
        {
            if (input.Quantity != compareto.Quantity)
            {
                return OrderBookUpdateType.QuantityChange;
            }
            if (input.Price != compareto.Price)
            {
                return OrderBookUpdateType.PriceChange;
            }
            return OrderBookUpdateType.NoChange;
        }

        public static bool HasPeriod(this CandlesSeries values, int period)
        {
            return (values.Index >= period);
        }

        public static bool HasDeviantValues(this List<double> list)
        {
            return (list.RemoveNan().Distinct().Count() > 1);
        }

        public static List<double> RemoveNan(this List<double> list)
        {
            list.RemoveAll(item => Double.IsNaN(item));
            return list;
        }
        public static List<double> RemoveSpecifiedMinAndMax(this List<double> list,int low = 0,int high = 100)
        {
            list.RemoveAll(item => item == low || item == high);
            return list;
        }
        public static List<double> MaxByPeriod(this List<double> list, int period = 15)
        {
            list.RemoveNan();
            List<double> MaxLst = new List<double>();
            var max = double.MinValue;
            var lastmaxindex = 0;
            if (list.Count() +1 > period)
            {
                var ListArray = list.ToArray();
                for(int i = list.Count() - period; i < list.Count() -1; i++)
                {
                    if (list[i] >= max)
                    {
                        MaxLst.Add(list[i]);
                        max = list[i];
                        Console.WriteLine("Max TA : {0}", list[i]);
                        lastmaxindex = i;
                    }
                }
            }
            return MaxLst;
        }
        public static List<double> MinByPeriod(this List<double> list, int period = 15)
        {
            list.RemoveNan();
            List<double> MinLst = new List<double>();
            var min = double.MaxValue;
            var lastmaxindex = 0;
            if (list.Count() + 1 > period)
            {
                var ListArray = list.ToArray();
                for (int i = list.Count() - period; i < list.Count() - 1; i++)
                {
                    if (list[i] <= min)
                    {
                        MinLst.Add(list[i]);
                        min = list[i];
                        Console.WriteLine("Min TA : {0}", list[i]);
                        lastmaxindex = i;
                    }
                }
            }
            return MinLst;
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
