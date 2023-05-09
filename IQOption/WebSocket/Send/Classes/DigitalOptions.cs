using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Send.Classes
{
    internal class DigitalOptions
    {
        private static string ToInstrumentID(int asset_id, DateTimeOffset expiration,
            Enumerations.Period instrument_period, Enumerations.Direction direction)
        {
            return string.Format("do{0}A{1}{2}M{3}SPT", asset_id,
                expiration.ToUniversalTime().ToString("yyyyMMdd'D'HHmmss'T'"),
                (int)instrument_period / 60,
                direction == Enumerations.Direction.call ? "C" : "P");
        }
        private static string PlaceDigitalOption(int user_balance_id, double amount,
            int instrument_index, int assetId, string instrument_id, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.user_balance_id = user_balance_id;
            routingFilters.amount = amount.ToString().Replace(",", ".");
            routingFilters.instrument_index = instrument_index;
            routingFilters.asset_id = assetId;
            routingFilters.instrument_id = instrument_id;

            JObject msg = Message.ToJObject(
                Message.Name.digital_options0place_digital_option,
                Message.Version.v2,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        internal static string PlaceDigitalOption(int asset_id, DateTimeOffset expiration,
            Enumerations.Period instrument_period, Enumerations.Direction direction,
            int user_balance_id, double amount, int instrument_index, string request_id = null)
        {
            string instrument_id = ToInstrumentID(asset_id, expiration, instrument_period,
                direction);

            return PlaceDigitalOption(user_balance_id, amount, instrument_index, asset_id,
                instrument_id, request_id);
        }
        internal static string PlaceDigitalOption(int asset_id, string instrument_id,
            int user_balance_id, double amount, int instrument_index, string request_id = null)
        {
            return PlaceDigitalOption(user_balance_id, amount, instrument_index, asset_id,
                instrument_id, request_id);
        }

        internal static string SubscribePositions(string[] ids, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.frequency = "frequent";
            routingFilters.ids = ids;

            JObject msg = Message.ToJObject(
                Message.Name.subscribe_positions,
                Message.Version.v1,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
    }
}