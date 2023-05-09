using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace IQOption.WebSocket.Send.Classes
{
    internal class Portfolio
    {
        internal static string GetPositions(List<Enumerations.InstrumentType> instrument_types,
            int offset, int limit, int? user_balance_id = null, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_types = instrument_types;
            routingFilters.user_balance_id = user_balance_id;
            routingFilters.offset = offset;
            routingFilters.limit = limit;

            JObject msg = Message.ToJObject(
                Message.Name.portfolio0get_positions,
                Message.Version.v3,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        internal static string GetPositions(List<Enumerations.InstrumentType> instrument_types,
            int? user_balance_id = null, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_types = instrument_types;
            routingFilters.user_balance_id = user_balance_id;

            JObject msg = Message.ToJObject(
                Message.Name.portfolio0get_positions,
                Message.Version.v4,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        //subscribe
        internal static string PositionChanged(bool subscribe, Enumerations.InstrumentType instrument_type,
            int user_id, int user_balance_id)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.user_id = user_id;
            routingFilters.user_balance_id = user_balance_id;

            JObject @params = Parameters.ToJObject(
                Parameters.Name.portfolio0position_changed, Parameters.Version.v3, routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, @params);
        }
        internal static string OrderChanged(bool subscribe, Enumerations.InstrumentType instrument_type,
            int user_id)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.user_id = user_id;

            JObject @params = Parameters.ToJObject(
                Parameters.Name.portfolio0order_changed, Parameters.Version.v2, routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, @params);
        }
    }
}