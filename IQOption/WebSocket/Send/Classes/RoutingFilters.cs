using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IQOption.WebSocket.Classes.JSON;

namespace IQOption.WebSocket.Send.Classes
{
    internal class RoutingFilters
    {
        public Enumerations.InstrumentType? instrument_type = null;
        public List<Enumerations.InstrumentType> instrument_types = null;
        public Enumerations.InstrumentType? type = null;
        public int? active_id = null;
        public int? size = null;
        public int? asset_id = null;
        public int? instrument_index = null;
        public int? user_group_id = null;
        public bool? is_regulated = null;
        public bool? filter_suspended = null;
        public int[] types_ids = null;
        public int[] tournaments_statuses_ids = null;
        public int? user_balance_id = null;
        public string amount = null;
        public string instrument_id = null;
        public long? to = null;
        public int? count = null;
        public int? user_id = null;
        public int? offset = null;
        public int? limit = null;
        public string frequency = null;
        public string[] ids = null;
        public int? option_type_id = null;
        public Enumerations.Direction? direction = null;
        public double? price = null;
        public long? expired = null;
        //public DateTimeOffset? start = null;
        //public DateTimeOffset? end = null;

        public string InstrumentTypeToString(Enumerations.InstrumentType? instrumentType)
        {
            switch (instrumentType)
            {
                case Enumerations.InstrumentType.binary_option:
                case Enumerations.InstrumentType.turbo_option:
                case Enumerations.InstrumentType.digital_option:
                    return instrumentType.ToString().Replace("_", "-");
                default:
                    return instrumentType.ToString();
            }
        }
        public List<string> InstrumentTypesToString(
            List<Enumerations.InstrumentType> instrumentTypes)
        {
            return instrumentTypes.Select(e => InstrumentTypeToString(e)).ToList();
        }
        public JObject ToJObject()
        {
            JObject routingFilters = new JObject();
            if (instrument_type != null)
            {
                routingFilters.Add("instrument_type", InstrumentTypeToString(instrument_type));
            }
            if (instrument_types != null)
            {
                routingFilters.Add("instrument_types",
                    JToken.FromObject(InstrumentTypesToString(instrument_types)));
            }
            if (type != null)
            {
                routingFilters.Add("type", InstrumentTypeToString(type));
            }
            if (active_id != null)
            {
                routingFilters.Add("active_id", active_id);
            }
            if (size != null)
            {
                routingFilters.Add("size", size);
            }
            if (asset_id != null)
            {
                routingFilters.Add("asset_id", asset_id);
            }
            if (instrument_index != null)
            {
                routingFilters.Add("instrument_index", instrument_index);
            }
            if (user_group_id != null)
            {
                routingFilters.Add("user_group_id", user_group_id);
            }
            if (is_regulated != null)
            {
                routingFilters.Add("is_regulated", is_regulated);
            }
            if (filter_suspended != null)
            {
                routingFilters.Add("filter_suspended", filter_suspended);
            }
            if (tournaments_statuses_ids != null)
            {
                routingFilters.Add("types_ids", JToken.FromObject(types_ids));
            }
            if (tournaments_statuses_ids != null)
            {
                routingFilters.Add("tournaments_statuses_ids", JToken.FromObject(tournaments_statuses_ids));
            }
            if (user_balance_id != null)
            {
                routingFilters.Add("user_balance_id", user_balance_id);
            }
            if (amount != null)
            {
                routingFilters.Add("amount", amount);
            }
            if (instrument_id != null)
            {
                routingFilters.Add("instrument_id", instrument_id);
            }
            if (to != null)
            {
                routingFilters.Add("to", to);
            }
            if (count != null)
            {
                routingFilters.Add("count", count);
            }
            if (user_id != null)
            {
                routingFilters.Add("user_id", user_id);
            }
            if (offset != null)
            {
                routingFilters.Add("offset", offset);
            }
            if (limit != null)
            {
                routingFilters.Add("limit", limit);
            }
            if (frequency != null)
            {
                routingFilters.Add("frequency", frequency);
            }
            if (ids != null)
            {
                routingFilters.Add("ids", JToken.FromObject(ids));
            }
            if (option_type_id != null)
            {
                routingFilters.Add("option_type_id", option_type_id);
            }
            if (direction != null)
            {
                routingFilters.Add("direction", direction.ToString());
            }
            if (price != null)
            {
                routingFilters.Add("price", price);
            }
            if (expired != null)
            {
                routingFilters.Add("expired", expired);
            }
            /*
            if (start != null)
            {
                routingFilters.Add("start", start.Value.ToUnixTimeSeconds());
            }
            if (end != null)
            {
                routingFilters.Add("end", end.Value.ToUnixTimeSeconds());
            }
            */
            return routingFilters;
        }
    }
}