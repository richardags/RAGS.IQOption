using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Classes;
using Newtonsoft.Json.Linq;

namespace IQOption.WebSocket.Send.Models
{
    internal class Parameters
    {
        internal enum Version
        {
            v1 = 1,
            v2 = 2,
            v3 = 3,
            v5 = 5
        }
        internal enum Name
        {
            //Risks
            commission_changed,
            //PriceSplitter
            price_splitter0client_price_generated,
            //DigitalOptionInstruments
            digital_option_instruments0instrument_generated,
            digital_option_instruments0underlying_list_changed,
            //TradingInstruments
            instruments_changed,
            //Quotes
            candle_generated,
            candles_generated,
            //Portfolio
            portfolio0position_changed,
            portfolio0order_changed,
            //Internal-Billing
            internal_billing0balance_changed
    }

        private static string TranslateName(Name name)
        {
            switch (name)
            {
                case Name.commission_changed:
                case Name.instruments_changed:
                case Name.candle_generated:
                case Name.candles_generated:
                    return name.ToString().Replace("_", "-");
                case Name.price_splitter0client_price_generated:
                case Name.digital_option_instruments0instrument_generated:
                case Name.digital_option_instruments0underlying_list_changed:
                case Name.portfolio0position_changed:
                case Name.portfolio0order_changed:
                case Name.internal_billing0balance_changed:
                    return name.ToString().Replace("_", "-").Replace("0", ".");
                default:
                    return name.ToString();
            }
        }

        internal static JObject ToJObject(Name name, RoutingFilters routingFilters)
        {
            JObject @params = new JObject();
            @params.Add("routingFilters", routingFilters.ToJObject());

            JObject data = new JObject();
            data.Add("name", TranslateName(name));
            data.Add("params", @params);

            return data;
        }
        internal static JObject ToJObject(Name name, Version version, RoutingFilters routingFilters)
        {
            JObject data = ToJObject(name, routingFilters);
            data.Add("version", string.Format("{0}.0", (int)version));

            return data;
        }
    }
}