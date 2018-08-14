using Moon.Data.Model;
using Moon.Nodes.service.core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Trady;
using Trady.Analysis;
using Trady.Analysis.Extension;
using System.Reflection;
using Trady.Analysis.Indicator;
using Trady.Analysis.Candlestick;

namespace Moon.Nodes
{


    class Program
    {
        static void Main(string[] args)
        {
            //Config Loader

            //Market Watcher Start


            Moon.Nodes.shared.Static.wssv = new WebSocketServer(1346);
            Moon.Nodes.shared.Static.wssv.AddWebSocketService<ServiceCandleMarket>("/CandleMarket");
            Moon.Nodes.shared.Static.wssv.Start();
            Console.ReadKey();

            Console.ReadKey(true);

        }

    }
}
