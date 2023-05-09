using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IQOption.WebSocket.Send.Classes
{
    internal class TradingInstruments
    {
        internal static string GetInstruments(Enumerations.InstrumentType instrument_type,
            string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.type = instrument_type;

            JObject msg = Message.ToJObject(
                Message.Name.get_instruments,
                Message.Version.v4,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }

        internal static string InstrumentsChanged(bool subscribe,
            Enumerations.InstrumentType instrument_type,
            int user_group_id, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.type = instrument_type;
            routingFilters.user_group_id = user_group_id;
            routingFilters.is_regulated = false;

            JObject @params = Parameters.ToJObject(
                Parameters.Name.instruments_changed, Parameters.Version.v5, routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, @params, request_id);
        }
    }
}