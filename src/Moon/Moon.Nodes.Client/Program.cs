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
        public static List<Binance.Net.Objects.BinanceStreamTrade> Buyer = new List<Binance.Net.Objects.BinanceStreamTrade>();
        public static List<Binance.Net.Objects.BinanceStreamTrade> Seller = new List<Binance.Net.Objects.BinanceStreamTrade>();

        static void Main(string[] args)
        {
            ws.OnMessage += Ws_OnMessage;
            ws.OnClose += Ws_OnClose;
            ws.OnError += Ws_OnError;

            while (ws.IsAlive != true)
            {
               
                ws.Connect();
                Console.WriteLine("Is Alive : {0}", ws.IsAlive);
            }
           
            Console.ReadKey(true);
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Messages receiver = JsonConvert.DeserializeObject<Messages>(e.Data);
                switch (receiver.MessageType)
                {
                    case TypeOfContent.Binance_Candles:
                        var Candle = JsonConvert.DeserializeObject<BinanceCandle>(receiver.Content);
                        Candlesreceived.Add(Candle);
                        Console.WriteLine("Pair - {0} : Received candle from node {1} - collection date : {2}", Candle.Name, receiver.ContentNodeName, Candle.CollectedDate);
                        break;
                    case TypeOfContent.Binance_TradesBuyer:
                        var TradeBuyer = JsonConvert.DeserializeObject<Binance.Net.Objects.BinanceStreamTrade>(receiver.Content);
                        Buyer.Add(TradeBuyer);
                        Console.WriteLine("Pair - {0} : Received Buyer Trade from node {1} - execution date : {2}", TradeBuyer.Symbol, receiver.ContentNodeName, TradeBuyer.EventTime);

                        break;
                    case TypeOfContent.Binance_TradesSeller:
                        var TradeSeller = JsonConvert.DeserializeObject<Binance.Net.Objects.BinanceStreamTrade>(receiver.Content);
                        Seller.Add(TradeSeller);
                        Console.WriteLine("Pair - {0} : Received Seller Trade from node {1} - execution date : {2}", TradeSeller.Symbol, receiver.ContentNodeName, TradeSeller.EventTime);

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

        }

        private static void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Exception on socket stream : {0}", e.Message);
            ws.Close();
            ws = new WebSocketSharp.WebSocket("ws://localhost:1346/CandleMarket");
            ws.Connect();
            ws.OnMessage += Ws_OnMessage;
            ws.OnClose += Ws_OnClose;
            ws.OnError += Ws_OnError;

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
