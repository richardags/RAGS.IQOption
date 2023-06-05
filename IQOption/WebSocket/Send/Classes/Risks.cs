using RAGS.IQOption.WebSocket.Classes.JSON;
using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    internal class Risks
    {
        internal static string GetCommissions(Enumerations.InstrumentType instrument_type,
            int user_group_id, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.user_group_id = user_group_id;

            JObject msg = Message.ToJObject(
                Message.Name.get_commissions,
                Message.Version.v1,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        //subscribe
        internal static string CommissionChanged(bool subscribe,
            Enumerations.InstrumentType instrument_type, int user_group_id)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.instrument_type = instrument_type;
            routingFilters.user_group_id = user_group_id;

            JObject @params = Parameters.ToJObject(
                Parameters.Name.commission_changed, Parameters.Version.v1, routingFilters);

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;
            
            return SendModel.ToJSON(name, @params);
        }
    }
}