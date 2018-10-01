using Binance.Net;
using Binance.Net.Objects;
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
    public class BinanceExchanger:IRoot,IExchanger
    {
        public BinanceClient Client { get; set; } = new Binance.Net.BinanceClient();
        public BinanceSocketClient Socket { get; set; } = new BinanceSocketClient();

        public Accounting.Account Account { get; set; } 

        public string TypeOfData { get; set; } = "BinanceConfig";
        public string Jscontainer { get; set; }
        public Exchange Platform { get; set; } = Exchange.Binance;
        public string Name { get; set; } = "Binance";
        public string Provider { get; set; } = "BinanceNETApi";
        public DateTime UpateSince { get; set; } = DateTime.Now;
        public DateTime StartedSince { get; set; } = DateTime.Now;

        public BinanceExchanger()
        {
            this.Account = new Accounting.Account(this);

        }
        public void SetApiKeys(string ApiKey, string ApiSecret)
        {
            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(ApiKey, ApiSecret),
                RateLimitingBehaviour = CryptoExchange.Net.Objects.RateLimitingBehaviour.Wait
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

        public bool Init()
        {
            throw new NotImplementedException();
        }

        public bool Connect()
        {
            throw new NotImplementedException();
        }


        public bool Close()
        {
            throw new NotImplementedException();
        }

        public bool GetByTime(DateTime Start, DateTime End, string symbol)
        {
            throw new NotImplementedException();
        }

        bool IExchanger.Update()
        {
            throw new NotImplementedException();
        }
    }
}
