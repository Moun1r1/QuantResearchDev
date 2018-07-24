using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Config
{

    public enum Mode
    {
        Conf_Live = 0,
        Conf_Backtest = 1,
        Conf_Collectors = 2,
        Conf_ExchangerNode = 3,
    }
    class Configuration
    {
        public Configuration()
        {
            //Load and set
        }
    }
    public static class Variables
    {
        public const bool Vars_ShouldBuy = false;
        public const bool Vars_ShouldSell = false;
        public const bool Vars_ShouldMonitor = false;
        public const bool Vars_ShouldCollectAndArchive = false;
        public const bool Vars_ShouldNNodes = false;
    }
}
