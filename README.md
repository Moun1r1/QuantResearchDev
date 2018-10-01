# QuantResearchDev by Moon Hedge Fund 

![GitHub Logo](/media/logo.png)

Wanna talk with us ? : https://discord.gg/Qq4MVVV

Quant Research dev & Traders open source project

## Who is Moon ? 
A community of traders & coders that want to spread some good logic to improve the overral knowledge of automated trading (no identity nor invite will ever be provided to join us, we are closed)

## User stories

 - I want to be able to run backtests against 1 coin pair
 - I want to be abke to run backtests against multiple coins
 - I want to be able to run backtrsts using different candle sizes
 - I want to be able to implement multiple indicators in a single strategy using multiple time frames
 - I want to be able to see the order book in the strategy
 - As a user who wants to create some strat, I want to be able to easily draw some graphs with some custom info ( indicators / max / min / Sell / buy / anything )(edited)
 - As a user I want this bot to run on several coins
 -  As a user I want fine tuned buy and sell signal, at sub minute timings, ( probably though web sockets )
 - as a user, I want an integration with somethign like discord or telegram, so I can get some information from my running bot from my phone
 -  I would like to be able to monitor account holdings
 -  I would like to be able to manage the asks and bids volume based on my own rules
 -  i want: function of strategy responded for currency/fiat . ex: btc in last 2 minutes -2% then sell asset, currency to USDT
 -  i want: strategy for execution orders (can write do use market, limit and when - high volume, retries failed etc)
 -  As a strat explorer, I want to have several strat running on paper trade at the same time

Concept
- Rebuilt time candle dynamicaly to expand view 
- Invoke dynamicaly strategy by weight score
- Use direct websocket & Azure Data Table stored data


## Data Model
![GitHub Logo](/media/DataModel.jpg)

## Concept
- IDWN 
- IExchanger -> Platform
- IIndicator -> TA virtual method override
- IIMarket -> Stream all market pairs to built sentiment, retrace and corelate news
- INode -> Restream to node using websocket all moon roles
- IInterface -> Use modelized view for UI rendering
- Performance -> manage resources allocation and repeated task through timespan
- Risk -> Risk model using RMSI (Tree based risk management) (Identify - Avoid - Hold - Transfer)
- Risk Parking -> Parking hodler statisticals follower and manager
- DNA -> Dynamic code agent to change runtime logic
- RNN -> Recurent Neural Network to predict slope / direction

## Interface Overview
### Real Time wall bounce overview
![Waller](/media/Waller.JPG)

### Market Automatic Pairs detections
![Waller](/media/MarketAutoExtract.JPG)

### Real time market broadcaster
![Market](/media/broadcaster.JPG)

### Real time trady trading pattern & ta in server node
![TANode](/media/RTTA.JPG)

### Real time binance market all pairs movement
![MarketMovement](/media/RTAMarket.JPG)

### Real Time overall market sentiment and capital
![MarketSent](/media/MarketSentiment.png)

### Azure Data Table storage
![cloudstorage](/media/AzureTableStorage.png)

### Auto elect pair for a given strategy before live
![cloudstorage](/media/Autoelect.JPG)

### Some custom TA 
![DynamicId](/media/DAT.JPG)

## Release v0.1
Best effort for EOY 2018.

## Credits
- Bitmex (for the good market making codes) 
- Bitfinex (for the code websocket srv)  
- Trady (with love) 
- Ninja (for the good design)
