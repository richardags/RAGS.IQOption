using IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IQOption.WebSocket.Classes.JSON.Enumerations;

namespace IQOption.WebSocket.Classes.JSON
{
    public class CandleGenerated
    {
        public int active_id;
        public CandleSize size;
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

        public Direction direction()
        {
            if (close > open)
            {
                return Direction.call;
            }
            else if (close < open)
            {
                return Direction.put;
            }
            else
            {
                return Direction.equal;
            }
        }
    }
}