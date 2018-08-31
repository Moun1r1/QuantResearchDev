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

            Console.WriteLine("Starting on : {0}", Environment.MachineName);

            //Config Loader

            //Market Watcher Start
            Type PType = typeof(IndexedIOhlcvDataExtension);
            Type SType = typeof(OhlcvExtension);
            MethodInfo[] SmethodInfos = SType
                                       .GetMethods(
                                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            MethodInfo[] PmethodInfos = PType
                           .GetMethods(
                            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            SmethodInfos.ToList().ForEach(y => Console.WriteLine("Trady - Loaded TA : {0}", y.Name));
            PmethodInfos.ToList().ForEach(y => Console.WriteLine("Trady - Loaded Patterns : {0}", y.Name));

            Moon.Nodes.shared.Static.wssv = new WebSocketServer(1346);
            Moon.Nodes.shared.Static.wssv.AddWebSocketService<ServiceCandleMarket>("/CandleMarket");
            Moon.Nodes.shared.Static.wssv.Start();
            Console.ReadKey();

            Console.ReadKey(true);

        }

    }
}
