using RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class CandleGenerated
    {
        public int active_id;
        public Enumerations.CandleSize size;
        [JsonConverter(typeof(UTNanoSecondsJsonConverter))]
        public DateTimeOffset at;
        [JsonConverter(typeof(UTSecondsJsonConverter))]
        public DateTimeOffset from;
        [JsonConverter(typeof(UTSecondsJsonConverter))]
        public DateTimeOffset to;
        //public ? min_at;
        //public ? max_at;
        //public int id;
        public double open;
        public double close;
        public double min;
        public double max;
        public double ask;
        public double bid;
        public int volume;
        //public Enumerations.Phase phase;

        public Enumerations.Direction direction()
        {
            if (close > open)
            {
                return Enumerations.Direction.call;
            }
            else if (close < open)
            {
                return Enumerations.Direction.put;
            }
            else
            {
                return Enumerations.Direction.equal;
            }
        }
    }
}