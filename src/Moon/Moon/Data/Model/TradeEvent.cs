using Moon.Data.Extender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public class TradeEventArg : EventArgs
    {
        public TradeEventEventType EventType { get; set; }
        public DateTime Collected { get; set; }
        public TradesSeries Source { get; set; }
        public IPair ComingCandle { get; set; }
        public TradeEventArg(TradeEventEventType _eventtype, TradesSeries _tradescopy,DateTime _colletionDate)
        {
            this.EventType = _eventtype;
            this.Collected = _colletionDate;
            this.Source = _tradescopy;
        }

    }
    public enum TradeEventEventType
    {
        BuyOrder_RSI,
        SellOrder_RSI,
        Book_L0,
        Book_L1,
        Book_L2,
        Book_L3,
        Book_L4,
        Book_L5,
        Book_L6,
        Book_L7,
        Book_L8,
        Book_L9
    }
}
