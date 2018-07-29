using CoinMarketCap;
using CoinMarketCap.Core;
using CoinMarketCap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.MarketWatcher
{
   public class Statistics
    {
        private ICoinMarketCapClient client = new CoinMarketCapClient();
        public GlobalDataEntity Market { get; set; } = new GlobalDataEntity();
        public List<TickerEntity> KeyPairsCapital { get; set; } = new List<TickerEntity>();
        public List<string> TopSymbol { get; set; } = new List<string>();
        public Statistics()
        {
            this.Market = client.GetGlobalDataAsync(CoinMarketCap.Enums.ConvertEnum.USD).Result;
            this.KeyPairsCapital = client.GetTickerListAsync(Global.shared.Config.Config_MarketWatcher_vars.First().KeysPairsToLoad , CoinMarketCap.Enums.ConvertEnum.USD).Result;
            this.TopSymbol = this.KeyPairsCapital.Select(y => y.Symbol).ToList();
        }

        public void Update()
        {
            this.Market = client.GetGlobalDataAsync(CoinMarketCap.Enums.ConvertEnum.USD).Result;
            this.KeyPairsCapital = client.GetTickerListAsync(Global.shared.Config.Config_MarketWatcher_vars.First().KeysPairsToLoad, CoinMarketCap.Enums.ConvertEnum.USD).Result;
            this.TopSymbol = this.KeyPairsCapital.Select(y => y.Symbol).ToList();

        }
        //ICoinMarketCapClient client = new CoinMarketCapClient();
        //var currency = client.GetGlobalDataAsync(CoinMarketCap.Enums.ConvertEnum.USD).Result;

    }
}
