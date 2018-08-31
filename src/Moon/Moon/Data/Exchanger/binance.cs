using Binance.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using Moon.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Exchanger
{
    public class binance:Root
    {
        public BinanceClient Client { get; set; } = new Binance.Net.BinanceClient();
        public BinanceSocketClient Socket { get; set; } = new BinanceSocketClient();

        public Accounting.Account Account { get; set; } 

        public string TypeOfData { get; set; } = "BinanceConfig";
        public string Jscontainer { get; set; }

        public binance()
        {
            this.Account = new Accounting.Account(this);

        }
        public void SetApiKeys(string ApiKey, string ApiSecret)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(ApiKey, ApiSecret),
            });
            this.Client = new BinanceClient();
            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials(ApiKey, ApiSecret),
            });
            this.Socket = new BinanceSocketClient();
            this.Account.UpdateAccount();
        }

        public void Update()
        {
            this.Jscontainer = Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
