using System;
using System.Collections.Generic;
using IQOption.Interfaces;
using IQOption.WebSocket;
using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Classes;

namespace IQOption
{
    public class IQOptionAPI : WebSocketAPI, IAuthentication
    {
        public event EventOnAuthenticationStatusEvent OnAuthenticationStatusEvent;

        private bool isDebugMode = false;

        public static IDictionary<string, int> actives = new Dictionary<string, int>()
        {
            { "EURUSD-OTC", 76},
            { "EURGBP-OTC", 77},
            { "USDCHF-OTC", 78},
            { "EURJPY-OTC", 79},
            { "NZDUSD-OTC", 80},
            { "GBPUSD-OTC", 81},
            { "GBPJPY-OTC", 84},
            { "USDJPY-OTC", 85},
            { "AUDCAD-OTC", 86},
            { "USDSGD-OTC", 0},
            { "USDHKD-OTC", 0},
            { "USDINR-OTC", 0},
            { "EURUSD", 1},
            { "EURGBP", 2},
            { "GBPJPY", 3},
            { "EURJPY", 4},
            { "GBPUSD", 5},
            { "USDJPY", 6},
            { "AUDCAD", 7},
            { "NZDUSD", 8},
            { "USDRUB", 10},
            { "USDCHF", 72},
            { "XAUUSD", 74},
            { "XAGUSD", 75},
            { "AUDUSD", 99},
            { "USDCAD", 100},
            { "AUDJPY", 101},
            { "GBPCAD", 102},
            { "GBPCHF", 103},
            { "GBPAUD", 104},
            { "EURCAD", 105},
            { "CHFJPY", 106},
            { "CADCHF", 107},
            { "EURAUD", 108},
            { "USDNOK", 168},
            { "EURNZD", 212},
            { "BTCUSD", 816},
            { "XRPUSD", 817},
            { "ETHUSD", 818},
            { "LTCUSD", 819},
            { "EOSUSD", 864},
            { "USDINR", 865},
            { "USDPLN", 866},
            { "USDBRL", 867},
            { "USDZAR", 868},
            { "USDSGD", 892},
            { "USDHKD", 893},
            { "AUDCHF", 943},
            { "AUDNZD", 944},
            { "CADJPY", 945},
            { "EURCHF", 946},
            { "GBPNZD", 947},
            { "NZDCAD", 948},
            { "NZDJPY", 949},
            { "NZDCHF", 1048}
        };
        public InitializationData initializationData;
        public DOIUnderlyingList doiUnderlyingList;
        public CoreGetProfile coreGetProfile;
        public CoreGetBalances coreGetBalances;

        /// <summary>
        /// Method only for inherance.
        /// </summary>
        protected IQOptionAPI() { }
        /// <summary>
        /// Method only for instance a object.
        /// </summary>
        /// <param name="email">E-mail of IQ Option account.</param>
        /// <param name="password">Password of IQ Option account.</param>
        public IQOptionAPI(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
        /// <summary>
        /// Establish connection with IQ Option server.
        /// </summary>
        public void Connect()
        {
            Open();
        }
        public void Disconnect()
        {
            Close();
        }
        public new bool IsStatusClosed
        {
            get { return base.IsStatusClosed; }
        }

        internal override void OnAuthenticationStatus(Status status, string error_message = null)
        {
            switch (status)
            {
                case Status.Connecting:
                    OnAuthenticationStatusEvent?.Invoke(Interfaces.Status.Connecting, error_message);
                    break;
                case Status.Connected:
                    initializationData = null;
                    doiUnderlyingList = null;
                    coreGetProfile = null;
                    coreGetBalances = null;

                    //this method is not sync
                    Send(SetOptions.SendResults());
                    //'sync' methods
                    Send(Initialization.GetInitializationData());
                    Send(DigitalOptionInstruments.GetUnderlyingList());
                    Send(Core.GeProfile());
                    Send(Core.GeBalances());
                    break;
                case Status.Closed:
                    OnAuthenticationStatusEvent?.Invoke(Interfaces.Status.Closed, error_message);
                    break;
                case Status.IncorretCredentials:
                case Status.Error:
                    OnAuthenticationStatusEvent?.Invoke(Interfaces.Status.Error, error_message);
                    break;
            }
        }

        internal override void OnInitializationDataEvent(InitializationData initializationData)
        {
            this.initializationData = initializationData;
            CheckAllDone();
        }
        internal override void OnDigitalOptionInstrumentsUnderlyingListEvent(
            DOIUnderlyingList doiUnderlyingList)
        {
            this.doiUnderlyingList = doiUnderlyingList;
            CheckAllDone();
        }

        internal override void OnRisksCommissionChangedEvent(CommissionChanged commissionChanged)
        {
            if (commissionChanged.instrument_type == Enumerations.InstrumentType.binary_option)
            {
                InitializationData.Active active =
                    initializationData.binary.actives.Find(e => e.id == commissionChanged.active_id);

                if(active == null)
                {
                    Console.WriteLine("OnRisksCommissionChangedEvent binary active=null - " + commissionChanged.active_id);
                    //warning
                    //OnRisksCommissionChangedEvent binary active=null - 74
                }
                else
                {
                    active.option.profit.commission = commissionChanged.commission.value;
                }                
            }
            else //turbo-option
            {
                InitializationData.Active active =
                    initializationData.turbo.actives.Find(e => e.id == commissionChanged.active_id);

                if (active == null)
                {
                    Console.WriteLine("OnRisksCommissionChangedEvent turbo active=null - " + commissionChanged.active_id);
                }
                else
                {
                    active.option.profit.commission = commissionChanged.commission.value;
                }
            }
        }

        internal override void OnCoreGetProfileEvent(CoreGetProfile coreGetProfile)
        {
            this.coreGetProfile = coreGetProfile;
            CheckAllDone();
        }
        internal override void OnCoreGetBalancesEvent(CoreGetBalances coreGetBalances)
        {
            this.coreGetBalances = coreGetBalances;
            CheckAllDone();
        }

        //others methods
        private void CheckAllDone()
        {
            if (initializationData != null &&
                doiUnderlyingList != null &&
                coreGetProfile != null &&
                coreGetBalances != null)
            {
                Send(Risks.CommissionChanged(true, Enumerations.InstrumentType.binary_option,
                    doiUnderlyingList.user_group_id));
                Send(Risks.CommissionChanged(true, Enumerations.InstrumentType.turbo_option,
                    doiUnderlyingList.user_group_id));

                OnAuthenticationStatusEvent?.Invoke(Interfaces.Status.Connected);
            }
        }

        public new virtual void SetDebugMode(bool enabled)
        {
            base.SetDebugMode(enabled);
            isDebugMode = enabled;
        }

        //public methods
        #region BinaryOptions
        public void BinaryOptionsOpenOption(int user_balance_id, int active_id,
            int option_type_id, Enumerations.Direction direction, DateTimeOffset expired,
            double price, string request_id = null)
        {
            Send(BinaryOptions.OpenOption(user_balance_id, active_id, option_type_id, direction, expired, price, request_id));
        }
        public void BinaryOptionsOpenOption(int user_balance_id, int active_id,
            Enumerations.Direction direction, int expiration, double price, string request_id = null)
        {
            Send(BinaryOptions.OpenOption(user_balance_id, active_id, direction, expiration, price, request_id));
        }
        #endregion

        #region DigitalOptions
        public void DigitalOptionsPlaceDigitalOption(int asset_id, DateTimeOffset expiration,
            Enumerations.Period instrument_period, Enumerations.Direction direction,
            int user_balance_id, double amount, int instrument_index, string request_id = null)
        {
            Send(DigitalOptions.PlaceDigitalOption(
                asset_id, expiration, instrument_period, direction, user_balance_id, amount,
                instrument_index, request_id
                ));
        }
        public void DigitalOptionsPlaceDigitalOption(int asset_id, string instrument_id,
            int user_balance_id, double amount, int instrument_index, string request_id = null)
        {
            Send(DigitalOptions.PlaceDigitalOption(
                asset_id, instrument_id, user_balance_id, amount,
                instrument_index, request_id
                ));
        }
        
        /// <summary>
        /// Subscribe position id for Digital Options.
        /// </summary>
        /// <param name="ids">position id</param>
        /// <param name="request_id">request_id from response</param>
        public void DigitalOptionsSubscribePositions(string[] ids, string request_id = null)
        {
            Send(DigitalOptions.SubscribePositions(ids, request_id));
        }
        #endregion

        #region DigitalOption
        public void DigitalOptionInstrumentsGet(int asset_id, string request_id)
        {
            Send(DigitalOptionInstruments.GetInstruments(
                Enumerations.InstrumentType.digital_option, asset_id, request_id));
        }
        #endregion

        #region PriceSplitter
        public void PriceSplitterClientPriceGenerated(bool subscribe, int instrument_index,
            int asset_id)
        {
            Send(PriceSplitter.ClientPriceGenerated(
                subscribe,
                Enumerations.InstrumentType.digital_option,
                instrument_index,
                asset_id
                ));
        }
        #endregion

        #region Portfolio
        public void PortfolioPositionChanged(bool subscribe,
            Enumerations.InstrumentType instrument_type, int user_id, int user_balance_id)
        {
            Send(Portfolio.PositionChanged(subscribe, instrument_type, user_id, user_balance_id));
        }
        public void PortfolioOrderChanged(bool subscribe, Enumerations.InstrumentType instrument_type,
            int user_id)
        {
            Send(Portfolio.OrderChanged(subscribe, instrument_type, user_id));
        }
        public void PortfolioGetPositions(List<Enumerations.InstrumentType> instrument_types, int offset,
            int limit, int? user_balance_id = null, string request_id = null)
        {
            Send(Portfolio.GetPositions(instrument_types, offset, limit, user_balance_id, request_id));
        }
        public void PortfolioGetPositions(Enumerations.InstrumentType instrument_type,
            int offset, int limit, int? user_balance_id = null, string request_id = null)
        {
            PortfolioGetPositions(new List<Enumerations.InstrumentType>() { instrument_type },
                offset, limit, user_balance_id, request_id);
        }
        public void PortfolioGetPositions(List<Enumerations.InstrumentType> instrument_types,
            int? user_balance_id = null, string request_id = null)
        {
            Send(Portfolio.GetPositions(instrument_types, user_balance_id, request_id));
        }
        public void PortfolioGetPositions(Enumerations.InstrumentType instrument_type,
            int? user_balance_id = null, string request_id = null)
        {
            PortfolioGetPositions(new List<Enumerations.InstrumentType>() { instrument_type },
                user_balance_id, request_id);
        }
        #endregion

        #region Quotes
        public void QuotesGetCandles(int asset_id, Enumerations.CandleSize size, DateTimeOffset to,
            int count, string request_id = null)
        {
            Send(Quotes.GetCandles(asset_id, size, to.ToUnixTimeSeconds(), count, request_id));
        }
        public void QuotesGetCandleOpenPrice(int asset_id, DateTimeOffset from,
            string request_id = null)
        {
            QuotesGetCandles(asset_id, Enumerations.CandleSize.S1, from, 1, request_id);
        }
        public void QuotesGetCandleClosePrice(int asset_id, DateTimeOffset to,
            string request_id = null)
        {
            QuotesGetCandles(asset_id, Enumerations.CandleSize.S1, to.AddSeconds(-1), 1, request_id);
        }

        public void QuotesCandleGenerated(bool subscribe, int asset_id,
            Enumerations.CandleSize size)
        {
            Send(Quotes.CandleGenerated(subscribe, asset_id, size));
        }
        #endregion

        #region Internal-Billing
        public void BalanceChangued(bool subscribe)
        {
            Send(InternalBilling.BalanceChangued(subscribe));
        }
        #endregion
    }
}