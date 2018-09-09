using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public class CandleEvent
    {
    }
    public class CandleEventArg :EventArgs
    {
        public CandleEventType EventType { get; set; }
        public DateTime Collected { get; set; }
        public IPair ComingCandle { get; set; }
        public CandleEventArg(CandleEventType _eventtype,IPair Candle)
        {
            this.EventType = _eventtype;
            this.ComingCandle = Candle;
            this.Collected = Candle.CollectedDate;
        }
    }
    public enum CandleEventType
    {
        NewCandle,
        NewConsolidatedCandle,
        NewCandleExtraProps,
        NewTAUpdate
    }
}
