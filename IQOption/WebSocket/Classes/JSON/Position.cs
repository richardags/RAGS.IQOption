using IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace IQOption.WebSocket.Classes.JSON
{
    public class Position
    {
        public class RawData
        {
            public int user_group_id;
            //public double? close_effect_amount;
            public long id;
            //public ? margin_call;
            public CloseReason? close_reason;
            public string reject_status;
            //public ? swap;
            //public ? tpsl_extra;
            //public ? sell_avg_price_enrolled;
            //public ? count;
            //public ? sell_amount;
            public Status status;
            public Enumerations.Direction instrument_dir;
            //public ? close_at;
            //public ? currency;
            public int user_balance_id;
            public Underlying.Active instrument_underlying;
            //public ? type;
            //public ? index;
            public double buy_avg_price;
            [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
            public DateTimeOffset instrument_expiration;
            public Enumerations.Period instrument_period;
            public int instrument_index;
            //public ? margin_call_enrolled;
            public long instrument_strike_value;
            //public ? margin;
            //public ? leverage;
            //public ? close_effect_amount_enrolled;
            //public ? commission;
            public string instrument_id;
            //public ? stop_lose_order_id;
            //public ? instrument_id_escape;
            public int user_id;
            //public ? last_index;
            //public ? update_at;
            //public ? swap_enrolled;
            public double instrument_strike;
            //public ? open_client_platform_id;
            //public ? create_at;
            public double buy_amount;
            //public ? buy_amount_enrolled;
            //public ? commission_enrolled;
            public double open_underlying_price;
            public List<long> order_ids;
            public Enumerations.InstrumentType instrument_type;
            //...

            public double GetPayout
            {
                get
                {
                    return Math.Round((100 - buy_avg_price) * 100 / buy_avg_price);
                }
            }
        }

        public class BinaryOptionsOptionChanged
        {
            public enum OptionType
            {
                binary,
                turbo
            }

            public long option_id;
            public Enumerations.Direction direction;
            public int option_type_id;
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset expiration_time;
            public int profit_percent;
            public OptionType option_type;

            public double GetPayout { 
                get
                {
                    return profit_percent - 100;
                }
            }
        }

        public class RawEvent
        {
            [JsonProperty("digital_options_position_changed1")]
            public RawData digitalOptionsPositionChanged1;

            [JsonProperty("digital_options_order_changed1")]
            public RawData digitalOptionsOrderChanged1;   

            [JsonProperty("binary_options_option_changed1")]
            public BinaryOptionsOptionChanged binaryOptionsOptionChanged;
        }

        public enum Source
        {
            [EnumMember(Value = "binary-options")]
            BinaryOptions,
            [EnumMember(Value = "digital-options")]
            DigitalOptions,
        }
        public enum Status
        {
            //position-changed
            open, closed,
            //order-changed
            rejected,
            filled
        }
        public enum CloseReason //HINT - in raw_event is nullable
        {
            expired, @default,
            win, loose, equal, sold, canceled, //HINT - deprecated, in old raw_event parameter
        }

        public string id;
        public int user_id;
        public int user_balance_id;
        //public int platform_id;
        //public long external_id;
        public int active_id;
        public string instrument_id;
        public Source source;
        public Enumerations.InstrumentType instrument_type;
        public Status status;
        public CloseReason close_reason;
        [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
        public DateTimeOffset open_time;
        [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
        public DateTimeOffset close_time;
        public double open_quote;
        public double invest;
        //public double invest_enrolled;
        //public ? sell_profit;
        //public ? sell_profit_enrolled;
        //public ? expected_profit;
        //public ? expected_profit_enrolled;
        public double pnl;
        public double pnl_realized;
        public double pnl_net;
        public double current_price;
        [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
        public DateTimeOffset quote_timestamp;
        //public ? swap;

        [JsonProperty("raw_event")]
        public RawEvent rawEvent;

        public bool ExistsOrderID(long order_id)
        {
            if(rawEvent.binaryOptionsOptionChanged != null)
            {
                return order_id == rawEvent.binaryOptionsOptionChanged.option_id;
            }
            else if (rawEvent.digitalOptionsPositionChanged1 != null)
            {
                return rawEvent.digitalOptionsPositionChanged1.order_ids.Exists(e => e == order_id);
            }
            else if (rawEvent.digitalOptionsOrderChanged1 != null)
            {
                return rawEvent.digitalOptionsOrderChanged1.id == order_id;
            }
            else
            {
                throw new Exception("Position.cs ExistsOrderID() - error: rawEvent null");
            }
        }
        
        public double GetPayout
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    return rawEvent.binaryOptionsOptionChanged.GetPayout;
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.GetPayout;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.GetPayout;
                }
                else
                {
                    throw new Exception("Position.cs GetPayout - error: rawEvent null");
                }
            }
        }
        public double GetWinProfit
        {
            get
            {
                return GetPayout * invest / 100;
            }
        }
        public string GetActiveName
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    return IQOptionAPI.actives.First(e => e.Value == active_id).Key.Replace("_", "-");
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.instrument_underlying.ToString();
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.instrument_underlying.ToString();
                }
                else
                {
                    throw new Exception("Position.cs GetActiveName - error: rawEvent null");
                }
            }
        }
        public Enumerations.Direction GetDirection
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    return rawEvent.binaryOptionsOptionChanged.direction;
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.instrument_dir;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.instrument_dir;
                }
                else
                {
                    throw new Exception("Position.cs GetDirection - error: rawEvent null");
                }
            }
        }
        public DateTimeOffset GetExpiration
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    return rawEvent.binaryOptionsOptionChanged.expiration_time;
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.instrument_expiration;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.instrument_expiration;
                }
                else
                {
                    throw new Exception("Position.cs GetExpiration - error: rawEvent null");
                }
            }
        }

        /// <summary>
        /// Only for digital-options.
        /// </summary>
        public Enumerations.Period GetPeriod
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    throw new Exception("Position.cs GetPeriod - error: method don't allowed for binary-options.");
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.instrument_period;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.instrument_period;
                }
                else
                {
                    throw new Exception("Position.cs GetPeriod - error: rawEvent null");
                }
            }
        }
        /// <summary>
        /// Only for digital-options.
        /// </summary>
        public int GetInstrumentIndex
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    throw new Exception("Position.cs GetInstrumentIndex - error: method don't allowed for binary-options.");
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.instrument_index;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.instrument_index;
                }
                else
                {
                    throw new Exception("Position.cs GetInstrumentIndex - error: rawEvent null");
                }
            }
        }
        /// <summary>
        /// Only for digital-options.
        /// </summary>
        public string GetRejectStatus
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    throw new Exception("Position.cs GetRejectStatus - error: method don't allowed for binary-options.");
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    return rawEvent.digitalOptionsPositionChanged1.reject_status;
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    return rawEvent.digitalOptionsOrderChanged1.reject_status;
                }
                else
                {
                    throw new Exception("Position.cs GetRejectStatus - error: rawEvent null");
                }
            }
        }

        /// <summary>
        /// Only for binary-options.
        /// </summary>
        public BinaryOptionsOptionChanged.OptionType GetOptionType
        {
            get
            {
                if (rawEvent.binaryOptionsOptionChanged != null)
                {
                    return rawEvent.binaryOptionsOptionChanged.option_type;
                }
                else if (rawEvent.digitalOptionsPositionChanged1 != null)
                {
                    throw new Exception("Position.cs GetOptionType - error: method don't allowed for digital-options.");
                }
                else if (rawEvent.digitalOptionsOrderChanged1 != null)
                {
                    throw new Exception("Position.cs GetOptionType - error: method don't allowed for digital-options.");
                }
                else
                {
                    throw new Exception("Position.cs GetOptionType - error: rawEvent null");
                }
            }
        }
    }
}