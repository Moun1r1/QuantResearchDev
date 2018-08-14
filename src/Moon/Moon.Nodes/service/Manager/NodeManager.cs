using Moon.Nodes.service.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace Moon.Nodes.service
{
    public enum NodeType
    {
        CoreNode,
        MarketWatcher,
        NN,
        TA,
        DWN,
        RSKMDL
    }
    public class NodeManager
    { 
        public string NodeName { get; set; } = Environment.MachineName;
        public bool AutoFixer { get; set; } = true;

        public void HandleIssue(NodeType node)
        {
            switch(node)
            {
                case NodeType.CoreNode:
                    try
                    {
                        Moon.Nodes.shared.Static.wssv.Stop();
                        Moon.Nodes.shared.Static.wssv = new WebSocketServer(1346);
                        Moon.Nodes.shared.Static.wssv.AddWebSocketService<ServiceCandleMarket>("/CandleMarket");
                        Moon.Nodes.shared.Static.wssv.Start();

                    }
                    catch { }
                    break;
            }

        }
    }
}
