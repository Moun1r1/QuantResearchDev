using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Nodes.config
{
    public class Configuration
    {
        public bool Azure_ArchiverRole { get; set; } = false;
        public bool Finance_TAProcessor { get; set; } = false;
        public bool Finance_NNmode { get; set; } = false;
        public bool Core_CandleBroadcast { get; set; } = false;
        public bool Core_MarkeWatcherCollector { get; set; } = true;
        public string NodeName { get; set; } = Environment.MachineName;
        public Configuration()
        {

        }
    }
}
