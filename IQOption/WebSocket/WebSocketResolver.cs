using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RAGS.IQOption.Interfaces;
using RAGS.IQOption.WebSocket.Classes.JSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RAGS.IQOption.WebSocket
{
    public class WebSocketResolver : IDigitalOptionInstruments, IPriceSplitter, IDigitalOption,
        IQuotes, IPortfolio, IBinaryOptions, IInternalBilling
    {
        #region events
        public event EventOnDigitalOptionInstrumentsEvent OnDigitalOptionInstrumentsEvent;
        public event EventOnPriceSplitterClientPriceGeneratedEvent OnPriceSplitterClientPriceGeneratedEvent;
        public event EventOnDigitalOptionPlaceDigitalOptionEvent OnDigitalOptionPlaceDigitalOptionEvent;
        public event EventOnDigitalOptionSubscribePositionsEvent OnDigitalOptionSubscribePositionsEvent;
        public event EventOnQuotesGetCandlesEvent OnQuotesGetCandlesEvent;
        public event EventOnQuotesCandleGeneratedEvent OnQuotesCandleGenerated;
        public event EventOnPortfolioPositionChangedEvent OnPortfolioPositionChangedEvent;
        public event EventOnPortfolioOrderChangedEvent OnPortfolioOrderChangedEvent;
        public event EventOnPortfolioGetPositionsEvent OnPortfolioGetPositionsEvent;
        public event EventOnBinaryOptionsOpenOptionEvent OnBinaryOptionsOpenOptionEvent;
        public event EventOnInternalBillingBalanceChangedEvent OnInternalBillingBalanceChangedEvent;
        #endregion

        private bool isDebugMode = false;

        protected virtual void SetDebugMode(bool enabled)
        {
            isDebugMode = enabled;
        }

        internal void ResolveMessage(string message)
        {
            JObject json = JObject.Parse(message);

            switch (json["name"].ToString())
            {
                case "authenticated":
                    AuthenticatedResolver(json);
                    break;
                case "result":
                    //Console.WriteLine(message);
                    SetOptionsResolver(json);
                    break;
                case "initialization-data":
                    InitializationDataResolver(json);
                    break;
                case "front":
                    //{"name":"front","msg":"ws09b.prod.ws.wz-ams","session_id":"8789320692449781567"}
                    break;
                case "timeSync":
                    TimeSyncResolver(json);
                    break;
                case "heartbeat":
                    break;
                case "commissions":
                    RisksCommissionsResolver(json);
                    break;
                case "commission-changed":
                    RisksCommissionChangedResolver(json);
                    break;
                case "instruments":
                    InstrumentsResolver(json);
                    break;
                case "instrument-generated":
                    DigitalOptionInstrumentsGeneratedResolver(json);
                    break;
                case "instruments-changed":
                    TradingInstrumentsChangedResolver(json);
                    break;
                case "client-price-generated":
                    PriceSplitterClientPriceGeneratedResolver(json);
                    break;
                case "underlying-list":
                    DigitalOptionInstrumentsUnderlyingListResolver(json);
                    break;
                case "underlying-list-changed":
                    DigitalOptionInstrumentsUnderlyingListChangedResolver(json);
                    break;
                case "profile":
                    CoreGetProfileResolver(json);
                    break;
                case "balances":
                    CoreGetBalancesResolver(json);
                    break;
                case "digital-option-placed":
                    DigitalOptionPlaceDigitalOptionResolver(json);
                    break;
                case "candles":
                    QuotesGetCandlesResolver(json);
                    break;
                case "candle-generated":
                    QuotesCandleGeneratedResolver(json);
                    break;
                case "position-changed":
                    PortfolioPositionChangedResolver(json);
                    break;
                case "order-changed":
                    PortfolioOrderChangedResolver(json);
                    break;
                case "positions":
                    PortfolioGetPositionsResolver(json);
                    break;
                case "positions-state":
                    DigitalOptionsPositionsStateResolver(json);
                    break;
                case "option":
                    BinaryOptionsOpenOptionResolver(json);
                    break;
                case "balance-changed":
                    InternalBillingBalanceChangedResolver(json);
                    break;
                //fix bellow
                case "_quote-generated":
                    QuoteGeneratedResolver(json["msg"]);
                    break;
                case "_buyComplete":
                    BuyCompleteResolver(json["msg"]);
                    break;
                case "_listInfoData":
                    break;
                default:
                    Console.WriteLine("ResolveMessage no recognized: " + message);
                    break;
            }
        }

        private void InstrumentsResolver(JToken json)
        {
            if (json["msg"].Count() > 1)
            {
                TradingInstrumentsGetResolver(json);
            }
            else
            {
                DigitalOptionInstrumentsResolver(json);
            }
        }

        #region Authenticate
        private void AuthenticatedResolver(JToken json)
        {
            OnAuthenticatedEvent((bool)json["msg"]);
        }
        internal virtual void OnAuthenticatedEvent(bool isAuthenticated) { }
        #endregion

        #region SetOptions
        private void SetOptionsResolver(JToken json)
        {
            OnSetOptionsEvent((bool)json["msg"]["success"], (string)json["request_id"]);
        }
        internal virtual void OnSetOptionsEvent(bool success, string request_id = null) { }
        #endregion

        #region InitializationData
        private void InitializationDataResolver(JToken json)
        {
            int status = (int)json["status"];

            if (status == 0)
            {
                InitializationData initializationData = json["msg"].ToObject<InitializationData>();
                OnInitializationDataEvent(initializationData);
            }
            else
            {
                throw new Exception("Resolver.class InitializationDataResolver() status uknown " + json.ToString());
            }
        }
        internal virtual void OnInitializationDataEvent(InitializationData initializationData) { }
        #endregion

        #region Risks
        private void RisksCommissionsResolver(JToken json)
        {
            //422 Unprocessable Entity
            //{"request_id":"","name":"commissions","msg":{"user_group_id":["user_group_id is required"]},"status":4220}

            int status = (int)json["status"];

            if (status == 2000)
            {
                Commissions commissions = json["msg"].ToObject<Commissions>();
                OnRisksCommissionsEvent(commissions);
            }
            else
            {
                throw new Exception("CommissionsResolver status uknown " + json.ToString());
            }
        }
        internal virtual void OnRisksCommissionsEvent(Commissions commissions) { }

        private void RisksCommissionChangedResolver(JToken json)
        {
            CommissionChanged commissionChanged = json["msg"].ToObject<CommissionChanged>();

            if(commissionChanged == null)
            {
                Console.WriteLine("commissionChanged = null - " + json.ToString());
            }
            else
            {
                OnRisksCommissionChangedEvent(commissionChanged);
            }
        }
        internal virtual void OnRisksCommissionChangedEvent(CommissionChanged commissionChanged) { }
        #endregion

        #region DigitalOptionInstruments
        //Instruments
        private void DigitalOptionInstrumentsResolver(JToken json)
        {
            int status = (int)json["status"];

            if (status == 2000)
            {
                DOIGetInstruments doiGetInstruments = json["msg"].ToObject<DOIGetInstruments>();
                OnDigitalOptionInstrumentsEvent?.Invoke(
                    (string)json["request_id"], doiGetInstruments);
            }
            else
            {
                Console.WriteLine(json.ToString());

                Error error = new Error();
                error.status = status;
                error.message = (string)json["msg"]["reason"];

                OnDigitalOptionInstrumentsEvent?.Invoke((string)json["request_id"], null, error);
            }
        }

        private void DigitalOptionInstrumentsGeneratedResolver(JToken json)
        {
            DOIGetInstruments doiGetInstruments = json["msg"].ToObject<DOIGetInstruments>();
            OnDigitalOptionInstrumentsGeneratedEvent(doiGetInstruments);
        }
        internal virtual void OnDigitalOptionInstrumentsGeneratedEvent(DOIGetInstruments doiGetInstruments) { }

        //UnderlyingList
        private void DigitalOptionInstrumentsUnderlyingListResolver(JToken json)
        {
            try
            {
                DOIUnderlyingList doiUnderlyingList = json["msg"].ToObject<DOIUnderlyingList>();
                OnDigitalOptionInstrumentsUnderlyingListEvent(doiUnderlyingList);
            }
            catch(Exception exception)
            {
                Console.WriteLine(json);
                Console.WriteLine(exception);
            }
        }
        internal virtual void OnDigitalOptionInstrumentsUnderlyingListEvent(DOIUnderlyingList doiUnderlyingList) { }

        private void DigitalOptionInstrumentsUnderlyingListChangedResolver(JToken json)
        {
            DOIUnderlyingListChanged doiUnderlyingListChanged = json["msg"].ToObject<DOIUnderlyingListChanged>();
            OnDigitalOptionInstrumentsUnderlyingListChangedEvent(doiUnderlyingListChanged);
        }
        internal virtual void OnDigitalOptionInstrumentsUnderlyingListChangedEvent(DOIUnderlyingListChanged doiUnderlyingListChanged) { }
        #endregion

        #region PriceSplitter
        private void PriceSplitterClientPriceGeneratedResolver(JToken json)
        {
            ClientPriceGenerated priceGenerated = json["msg"].ToObject<ClientPriceGenerated>();
            OnPriceSplitterClientPriceGeneratedEvent?.Invoke(priceGenerated);
        }
        #endregion

        #region TradingInstruments
        private void TradingInstrumentsGetResolver(JToken json)
        {
            int status = (int)json["status"];

            if (status == 2000)
            {
                TIGetInstruments tiGetInstruments = json["msg"].ToObject<TIGetInstruments>();
                OnTradingInstrumentsGetEvent((string)json["request_id"], tiGetInstruments);
            }
            else
            {
                throw new Exception("TradingInstrumentsGetResolver status uknown " + json.ToString());
            }
        }
        internal virtual void OnTradingInstrumentsGetEvent(string request_id, TIGetInstruments tiGetInstruments) { }
        
        private void TradingInstrumentsChangedResolver(JToken json)
        {
            TIGetInstruments tiGetInstruments = json["msg"].ToObject<TIGetInstruments>();
            OnTradingInstrumentsChangedEvent(tiGetInstruments);
        }
        internal virtual void OnTradingInstrumentsChangedEvent(TIGetInstruments tiGetInstruments) { }
        #endregion

        #region Core
        private void CoreGetProfileResolver(JToken json)
        {
            if((int)json["status"] == 0 && (bool)json["msg"]["isSuccessful"])
            {
                CoreGetProfile coreGetProfile = json["msg"]["result"].ToObject<CoreGetProfile>();
                OnCoreGetProfileEvent(coreGetProfile);
            }
            else
            {
                throw new Exception("Resolver.class ProfileResolver() - status unknown: " + json.ToString());
            }
        }
        internal virtual void OnCoreGetProfileEvent(CoreGetProfile coreGetProfile) { }

        private void CoreGetBalancesResolver(JToken json)
        {
            if((int)json["status"] == 0)
            {
                CoreGetBalances coreGetBalances = json.ToObject<CoreGetBalances>();
                OnCoreGetBalancesEvent(coreGetBalances);
            }
            else
            {
                throw new Exception("Resolver.class CoreGetBalancesResolver() - status unknown: " + json.ToString());
            }
        }
        internal virtual void OnCoreGetBalancesEvent(CoreGetBalances coreGetBalances) { }
        #endregion

        #region BinaryOptions
        private void BinaryOptionsOpenOptionResolver(JToken json)
        {
            if ((int)json["status"] == 0)
            {
                OnBinaryOptionsOpenOptionEvent?.Invoke(
                    (string)json["request_id"],
                    json["msg"].ToObject<Option>());
            }
            else
            {
                BinaryOptionsOpenOptionException error = new(
                    (int)json["status"],
                    (string)json["msg"]["message"]);

                OnBinaryOptionsOpenOptionEvent?.Invoke(
                    (string)json["request_id"],
                    null,
                    error);
            }
        }
        #endregion

        #region DigitalOption
        private void DigitalOptionPlaceDigitalOptionResolver(JToken json)
        {
            if ((int)json["status"] == 2000)
            {
                OnDigitalOptionPlaceDigitalOptionEvent?.Invoke(
                    (string)json["request_id"],
                    (long)json["msg"]["id"]);
            }
            else //status 5000 or another
            {
                Error error = new Error();
                error.status = (int)json["status"];
                error.message = (string)json["msg"]["message"];

                OnDigitalOptionPlaceDigitalOptionEvent?.Invoke(
                    (string)json["request_id"],
                    null,
                    error);
            }
        }
        private void DigitalOptionsPositionsStateResolver(JToken json)
        {
            OnDigitalOptionSubscribePositionsEvent?.Invoke(
                json["msg"].ToObject<PositionsState>(),
                (string)json["request_id"]);
        }
        #endregion

        #region Quotes
        private void QuotesGetCandlesResolver(JToken json)
        {
            if ((int)json["status"] == 2000)
            {
                List<Candle> candles = json["msg"]["candles"].ToObject<List<Candle>>();

                OnQuotesGetCandlesEvent?.Invoke((string)json["request_id"], candles);
            }
            else
            {
                OnQuotesGetCandlesEvent?.Invoke((string)json["request_id"], null);
            }
        }
        //subscribe
        private void QuotesCandleGeneratedResolver(JToken json)
        {
            OnQuotesCandleGenerated?.Invoke(json["msg"].ToObject<CandleGenerated>());
        }
        #endregion

        #region Portfolio
        private void PortfolioGetPositionsResolver(JToken json)
        {
            if (isDebugMode)
            {
                Console.WriteLine(json.ToString());
            }

            if ((int)json["status"] == 2000)
            {
                OnPortfolioGetPositionsEvent?.Invoke((string)json["request_id"],
                    json["msg"].ToObject<PGetPositions>());
            }
            else
            {
                Console.WriteLine(json.ToString());
                throw new Exception("Resolver.class PortfolioGetPositionsResolver() - status unknown: " + json.ToString());
            }
        }

        private void PortfolioPositionChangedResolver(JToken json)
        {
            OnPortfolioPositionChangedEvent?.Invoke(json["msg"].ToObject<Position>());
        }
        private void PortfolioOrderChangedResolver(JToken json)
        {
            OnPortfolioOrderChangedEvent?.Invoke(json["msg"].ToObject<Position>());
        }
        #endregion

        #region Internal-Billing
        private void InternalBillingBalanceChangedResolver(JToken json)
        {
            OnInternalBillingBalanceChangedEvent?.Invoke(
                json["msg"].ToObject<BalanceChanged>());
        }
        #endregion

        #region Unknown
        private void TimeSyncResolver(JToken json)
        {
            DateTimeOffset timeSync = DateTimeOffset.FromUnixTimeMilliseconds((long)json["msg"]);
            OnTimeSyncEvent(timeSync);
        }
        internal virtual void OnTimeSyncEvent(DateTimeOffset timeSync) { }
        #endregion

        private void QuoteGeneratedResolver(JToken json)
        {
            QuoteGenerated quoteGenerated = json.ToObject<QuoteGenerated>();
            OnQuoteGeneratedEvent(quoteGenerated);
        }
        internal virtual void OnQuoteGeneratedEvent(QuoteGenerated quoteGenerated) { }

        private void BuyCompleteResolver(JToken json)
        {
            _BuyComplete buyComplete;

            bool isSuccessful = (bool)json["isSuccessful"];

            if (isSuccessful)
            {
                buyComplete = json["result"].ToObject<_BuyComplete>();
            }
            else
            {
                buyComplete = new _BuyComplete();
            }

            buyComplete.isSuccessful = isSuccessful;

            BuyCompleteEvent(buyComplete);
        }
        internal virtual void BuyCompleteEvent(_BuyComplete buyComplete) { }
    }
}