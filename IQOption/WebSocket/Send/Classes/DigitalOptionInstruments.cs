using RAGS.IQOption.WebSocket.Classes.JSON;
using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    internal class DigitalOptionInstruments
    {
        internal static string GetInstruments(Enumerations.InstrumentType instrument_type,
            int asset_id, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.asset_id = asset_id;

            JObject msg = Message.ToJObject(
                Message.Name.digital_option_instruments0get_instruments,
                Message.Version.v1,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        internal static string InstrumentGenerated(bool subscribe,
            Enumerations.InstrumentType instrument_type, int asset_id, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.asset_id = asset_id;

            JObject _params = Parameters.ToJObject(
                Parameters.Name.digital_option_instruments0instrument_generated,
                Parameters.Version.v1,
                routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, _params, request_id);
        }

        internal static string GetUnderlyingList(string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.filter_suspended = true;

            JObject msg = Message.ToJObject(
                Message.Name.digital_option_instruments0get_underlying_list,
                Message.Version.v1,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        internal static string UnderlyingListChanged(bool subscribe, int user_group_id,
            string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.user_group_id = user_group_id;
            routingFilters.is_regulated = false;

            JObject _params = Parameters.ToJObject(
                Parameters.Name.digital_option_instruments0underlying_list_changed,
                Parameters.Version.v1,
                routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, _params, request_id);
        }
    }
}