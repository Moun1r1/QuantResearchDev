using Moon.Data.Extender;
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
        public CandlesSeries Candlescopy { get; set; }
        public CandleEventArg(CandleEventType _eventtype,IPair Candle, CandlesSeries _candlescopy)
        {
            this.EventType = _eventtype;
            this.ComingCandle = Candle;
            this.Collected = Candle.CollectedDate;
            this.Candlescopy = _candlescopy;
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
