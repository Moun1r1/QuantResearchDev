using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Nodes.service.core
{
   public class Corenode
    {
        public Moon.Data.Provider.Core core = new Moon.Data.Provider.Core();
        public Corenode()
        {
            core.SubscribeTo("BTCUSDT");

        }
    }
}
