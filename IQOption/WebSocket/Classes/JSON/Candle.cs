using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;

namespace IQOption.WebSocket.Classes.JSON
{
    public class Candle
    {
        public int id;
        [JsonConverter(typeof(UTSecondsJsonConverter))]
        public DateTimeOffset from;
        [JsonConverter(typeof(UTSecondsJsonConverter))]
        public DateTimeOffset to;
        public double open;
        public double close;
        public double min;
        public double max;
        public int volume;
        //public ? phase;

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