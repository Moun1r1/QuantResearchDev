using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{
    public class PatternEvent
    {

    }
    public enum PatternType
    {
        IsBearish,
        IsBullish,
        IsAccumDistBearish,
        IsAccumDistBullish,
        IsBreakingHistoricalHighestClose,
        IsBreakingHistoricalHighestHigh,
        IsBreakingHistoricalLowestLow,
        IsBreakingHistoricalLowestClose,
        IsObvBullish,
        IsObvBearish

    }
}
