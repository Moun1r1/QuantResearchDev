using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public enum Exchange
    {
        Binance,
        Bitmex,
        Bitfinex
    }
    public interface IExchanger
    {
        Exchange Platform { get; set; }
        string Name { get; set; }
        string Provider { get; set; }


        DateTime UpateSince { get; set; }
        DateTime StartedSince { get; set; }

        bool Init();
        bool Connect();
        bool Update();
        bool Close();
        bool GetByTime(DateTime Start, DateTime End, string symbol);
        

    }
}
