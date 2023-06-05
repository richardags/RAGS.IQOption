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
    internal class Quotes
    {
        /// <summary>
        /// Get Candles.
        /// </summary>
        /// <param name="asset_id">Active id</param>
        /// <param name="size">Size of candle in seconds.</param>
        /// <param name="to">End time in unixtimestamp.</param>
        /// <param name="count">Quantity of candles.</param>
        /// <param name="request_id">Optional identify parameter.</param>
        /// <returns></returns>
        internal static string GetCandles(int asset_id, Enumerations.CandleSize size, long to,
            int count, string request_id = null)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.active_id = asset_id;
            routingFilters.size = (int)size;
            routingFilters.to = to;
            routingFilters.count = count;
            //routingFilters.only_closed = only_closed;

            JObject msg = Message.ToJObject(
                Message.Name.get_candles,
                Message.Version.v2,
                routingFilters.ToJObject()
                );

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }

        /// <summary>
        /// Subscribe Candle Generated.
        /// </summary>
        /// <param name="subscribe">Subscribe or Unsuscribe state.</param>
        /// <param name="asset_id">Active id.</param>
        /// <param name="size">size of candle in seconds.</param>
        /// <returns></returns>
        internal static string CandleGenerated(bool subscribe, int asset_id,
            Enumerations.CandleSize size)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.active_id = asset_id;
            routingFilters.size = (int)size;

            JObject _params = Parameters.ToJObject(
                Parameters.Name.candle_generated,
                routingFilters
                );

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, _params);
        }
        /// <summary>
        /// Subscribe Candles Generated.
        /// </summary>
        /// <param name="subscribe">Subscribe or Unsuscribe state.</param>
        /// <param name="asset_id">Active id.</param>
        /// <returns></returns>
        internal static string CandlesGenerated(bool subscribe, int asset_id)
        {
            RoutingFilters routingFilters = new RoutingFilters();
            routingFilters.active_id = asset_id;

            JObject _params = Parameters.ToJObject(
                Parameters.Name.candles_generated,
                routingFilters
                );

            SendModel.Name name = subscribe ?
                SendModel.Name.subscribeMessage : SendModel.Name.unsubscribeMessage;

            return SendModel.ToJSON(name, _params);
        }
    }
}