using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Moon.Nodes
{

    public class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received client request..");
            if (e.IsText)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(e.Data);
                    switch(result is Moon.Data.Model.BinanceCandle)
                    { 
}
                    Console.WriteLine("Exchanger : {0}", result.Exchanger);
                    Console.WriteLine("Exchanger : {0}", result.Name);
                    Console.WriteLine("CollectedDate : {0}", result.CollectedDate);
                    Console.WriteLine("Pivot : {0}", result.Pivot);

                }
                catch { }

                Console.WriteLine("Reiceved : {0}", e.Data);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer(1345);
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();
            Console.ReadKey(true);
            wssv.Stop();

        }
    }
}
