using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQOption.WebSocket.Send.Classes
{
    internal class PriceSplitter
    {
        internal static string ClientPriceGenerated(bool subscribe,
            Enumerations.InstrumentType instrumentType, int instrument_index, int asset_id)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrumentType;
            routingFilters.asset_id = asset_id;
            routingFilters.instrument_index = instrument_index;

            JObject _params = Parameters.ToJObject(
                Parameters.Name.price_splitter0client_price_generated,
                Parameters.Version.v1,
                routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, _params);
        }
    }
}