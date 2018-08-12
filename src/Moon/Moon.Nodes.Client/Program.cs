using Moon.Data.Model;
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
        public static List<BinanceCandle> Candlesreceived = new List<BinanceCandle>();
        static void Main(string[] args)
        {

            ws.OnMessage += (sender, e) =>
            {

                try
                {
                    Messages receiver = JsonConvert.DeserializeObject<Messages>(e.Data);
                    switch(receiver.TargetObject)
                    {
                        case "BinanceCandle":
                            var Candle = JsonConvert.DeserializeObject<BinanceCandle>(e.Data);
                            Candlesreceived.Add(Candle);
                            Console.WriteLine("Received candle from node {0} - collection date : {1}", receiver.ContentNodeName, Candle.CollectedDate);
                            break;
                    }
                    //if(receiver is BinanceCandle)
                    //{
                    //    receiver.Properties.ToList().ForEach(y =>
                    //    {
                    //        Console.WriteLine("Propertie Name : {0} - Value {1}", y.Key, y.Value);
                    //    });

                    //}

                }
                catch
                {

                }

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
