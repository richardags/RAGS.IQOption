using System;
using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IQOption.WebSocket.Classes.JSON
{
    public class _BuyComplete
    {
        public class Result
        {
            public int user_id { get; set; }
            public int refund_value { get; set; }
            public double price { get; set; }
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset exp { get; set; }
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset created { get; set; }
            [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
            public DateTimeOffset created_millisecond { get; set; }
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset time_rate { get; set; }
            //[JsonConverter(typeof(StringEnumConverter))]
            public Enumerations.BinaryOptionType type { get; set; }
            public int act { get; set; }
            public Enumerations.Direction direction { get; set; }
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset exp_value { get; set; }
            public double value { get; set; }
            public long id { get; set; }
            public int profit_income { get; set; }
        }

        public bool isSuccessful { get; set; }
        //public ? message { get; set; }
        public Result result { get; set; }
    }
}