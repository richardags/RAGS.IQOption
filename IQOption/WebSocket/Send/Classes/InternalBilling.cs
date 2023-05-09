using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Send.Classes
{
    internal class InternalBilling
    {
        internal static string BalanceChangued(bool subscribe)
        {
            RoutingFilters routingFilters = new RoutingFilters();

            JObject msg = Parameters.ToJObject(
                Parameters.Name.internal_billing0balance_changed,
                Parameters.Version.v1,
                routingFilters
                );

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, msg);
        }
    }
}