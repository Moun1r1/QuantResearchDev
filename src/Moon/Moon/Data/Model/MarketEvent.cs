using Binance.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moon.Data.Model
{

    public class MarketEvent
    {
    }
    public class MarketEventArg : EventArgs
    {
        public MarketAssetClass EventClass { get; set; }
        public MarketAssetUpdateType UpdateType { get; set; }
        public MarketWatcher.Statistics DataSource { get; set; }
        public Dictionary<string, List<BinanceStreamTick>> Pairs { get; set; } 
        public MarketEventArg(MarketWatcher.Statistics _content ,MarketAssetUpdateType _updtype
        , MarketAssetClass _class)
        {
            this.UpdateType = _updtype;
            this.DataSource = _content;
            this.EventClass = _class;
        }
    }
    public enum MarketAssetUpdateType
    {
        All,
        MostLiquid,
        MedianMarketMove,
        KeyPairsCapital,
        TopSymbols,
        MostNegative,
        MostPositive,
        Sentiment,
        News
    }

    public enum MarketAssetClass
    {
        All,
        ETH,
        BTC,
        BNB,
        USDT,
        NotInScope

    }
}
