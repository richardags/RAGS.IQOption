using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    public class Core
    {
        internal static string GeProfile(string request_id = null)
        {
            JObject msg = Message.ToJObject(
                Message.Name.core0get_profile,
                Message.Version.v1,
                new JObject());

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }

        internal static string GeBalances(string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.types_ids = new int[] { 1, 4 };
            routingFilters.tournaments_statuses_ids = new int[] { 3, 2 };

            JObject msg = Message.ToJObject(
                Message.Name.get_balances,
                Message.Version.v1,
                routingFilters.ToJObject());

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
    }
}