using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Config
{
        public class ConfigGlobalVar
    {
        public bool Vars_ShouldBuy { get; set; }
        public bool? Vars_ShouldSell { get; set; }
        public bool? Vars_ShouldMonitor { get; set; }
        public bool? Vars_ShouldCollectAndArchive { get; set; }
        public bool? Vars_ShouldNNodes { get; set; }
    }
    public class SocketPair
    {
        public bool Symbol { get; set; }
        public string Period { get; set; }
        public string Exchanger { get; set; }
    }
    public class NewsSource
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public bool LoadSummary { get; set; }
    }
    public class ConfigMarketWatcherVar
    {
        public int KeysPairsToLoad { get; set; }
    }

    public class ConfigGlobal
    {
        public List<ConfigMarketWatcherVar> Config_MarketWatcher_vars { get; set; }
        public List<ConfigGlobalVar> Config_global_vars { get; set; }
        public List<NewsSource> NewsSource { get; set; }
        public List<SocketPair> SocketPairs { get; set; }

    }
}
