using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Moon.Nodes.Client
{
    class Program
    {
        public static WebSocketSharp.WebSocket ws = new WebSocketSharp.WebSocket("ws://localhost:1346/CandleMarket");
        static void Main(string[] args)
        {

            ws.OnMessage += (sender, e) =>
            {
                dynamic receiver = JsonConvert.DeserializeObject<dynamic>(e.Data);
                Console.WriteLine("Received: " + receiver.Properties);

            };

            while (ws.IsAlive != true)
            {
                ws.Connect();
            }
            ws.OnClose += Ws_OnClose;
            Console.ReadKey(true);
        }

        private static void Ws_OnClose(object sender, CloseEventArgs e)
        {
            while (ws.IsAlive != true)
            {
                ws.Connect();
            }
        }
    }
}
