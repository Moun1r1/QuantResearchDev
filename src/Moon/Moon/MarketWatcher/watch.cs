using Binance.Net.Objects;
using CoinMarketCap;
using CoinMarketCap.Core;
using CoinMarketCap.Entities;
using Moon.Data.Extender;
using Moon.Data.Model;
using Moon.Data.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.MarketWatcher
{

    /// <summary>
    /// Instance to create after (direct link)
    /// </summary>
   public class Statistics : IRoot
    {
        public string TypeOfData { get; set; } = "MarketWatcher";
        private ICoinMarketCapClient client = new CoinMarketCapClient();
        public GlobalDataEntity Market { get; set; } = new GlobalDataEntity();
        public ObservableCollection<BinanceStreamKlineData> BWatcherKeysPairs { get; set; } = new ObservableCollection<BinanceStreamKlineData>();
        public event EventHandler<MarketEventArg> NewUpdate;

        public decimal binancebtcpairmove { get; set; } = 0;
        public decimal binanceethpairmove { get; set; } = 0;
        public decimal binanceusdtpairmove { get; set; } = 0;
        public decimal binancebnbpairmove { get; set; } = 0;

        public List<string> binance_mostliquid_btc { get; set; } = new List<string>();
        public List<string> binance_mostliquid_eth { get; set; } = new List<string>();
        public List<string> binance_mostliquid_usdt { get; set; } = new List<string>();
        public List<string> binance_mostliquid_bnb { get; set; } = new List<string>();

        public List<string> TopPairMove { get; set; } = new List<string>();

        public List<TickerEntity> KeyPairsCapital { get; set; } = new List<TickerEntity>();
        public List<string> TopSymbol { get; set; } = new List<string>();
        public string Jscontainer { get; set; }

        public Statistics()
        {
        }

        public void ConnectBinance(BinanceProvier source)
        {
            source.BAllPairsData.CollectionChanged += BAllPairsData_CollectionChanged;
            source.RegisterAllMarket();
        }

        public void BAllPairsData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var receiveddata = (BinanceStreamTick[])e.NewItems[0];
                try
                {
                    //Data Extract
                    var filteredByGroupBTC = receiveddata.Where(y => y.Symbol.EndsWith("BTC")).ToList();
                    var filteredByGroupETH = receiveddata.Where(y => y.Symbol.EndsWith("ETH")).ToList();
                    var filteredByGroupUSDT = receiveddata.Where(y => y.Symbol.EndsWith("USDT")).ToList();
                    var filteredByGroupBNB = receiveddata.Where(y => y.Symbol.EndsWith("BNB")).ToList();

                    this.binance_mostliquid_btc = filteredByGroupBTC.OrderBy(y => y.TotalTrades).ToList().GetRange(0, 5).Select(y => y.Symbol).ToList();
                    this.binance_mostliquid_eth = filteredByGroupETH.OrderBy(y => y.TotalTrades).ToList().GetRange(0, 5).Select(y => y.Symbol).ToList();
                    this.binance_mostliquid_usdt = filteredByGroupUSDT.OrderBy(y => y.TotalTrades).ToList().GetRange(0, 5).Select(y => y.Symbol).ToList();
                    this.binance_mostliquid_bnb = filteredByGroupBNB.OrderBy(y => y.TotalTrades).ToList().GetRange(0, 5).Select(y => y.Symbol).ToList();


                    //Median of market move
                    var liftedBTCMarketMove = filteredByGroupBTC.Select(y => y.PriceChangePercentage).ToList().Mean();
                    var liftedETHMarketMove = filteredByGroupETH.Select(y => y.PriceChangePercentage).ToList().Mean();
                    var liftedUSDTMarketMove = filteredByGroupUSDT.Select(y => y.PriceChangePercentage).ToList().Mean();
                    var liftedBNBMarketMove = filteredByGroupBNB.Select(y => y.PriceChangePercentage).ToList().Mean();

                    //Market liquid

                    this.binancebtcpairmove = liftedBTCMarketMove;
                    this.binanceethpairmove = liftedETHMarketMove;
                    this.binanceusdtpairmove = liftedUSDTMarketMove;
                    this.binancebnbpairmove = liftedBNBMarketMove;


                    Dictionary<string, List<BinanceStreamTick>> PairMultiValue = new Dictionary<string, List<BinanceStreamTick>>();
                    foreach (var onepair in receiveddata)
                    {
                        var IsBTC = onepair.Symbol.EndsWith("BTC");
                        var IsETH = onepair.Symbol.EndsWith("ETH");
                        var IsUSDT = onepair.Symbol.EndsWith("USDT");
                        var IsXLM = onepair.Symbol.EndsWith("BNB");
                        string BasePairName = "";
                        if (IsBTC) { BasePairName = onepair.Symbol.Replace("BTC", ""); }
                        if (IsETH) { BasePairName = onepair.Symbol.Replace("ETH", ""); }
                        if (IsUSDT) { BasePairName = onepair.Symbol.Replace("USDT", ""); }
                        if (IsXLM) { BasePairName = onepair.Symbol.Replace("BNB", ""); }

                        if (PairMultiValue.ContainsKey(BasePairName)) { }
                        else { PairMultiValue.Add(BasePairName, receiveddata.Where(y => y.Symbol.StartsWith(BasePairName)).ToList()); }
                    }

                    NewUpdate?.Invoke(this, new MarketEventArg(this, MarketAssetUpdateType.All, MarketAssetClass.All));

                }
                catch
                {

                }

            }
        }

        public void Update()
        {
            this.Market = client.GetGlobalDataAsync(CoinMarketCap.Enums.ConvertEnum.USD).Result;
            //this.KeyPairsCapital = client.GetTickerListAsync(Global.Shared.Config.Config_MarketWatcher_vars.First().KeysPairsToLoad, CoinMarketCap.Enums.ConvertEnum.USD).Result;
            this.TopSymbol = this.KeyPairsCapital.Select(y => y.Symbol).ToList();
        }
        //ICoinMarketCapClient client = new CoinMarketCapClient();
        //var currency = client.GetGlobalDataAsync(CoinMarketCap.Enums.ConvertEnum.USD).Result;

    }
}
