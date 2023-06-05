using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAGS.IQOption.WebSocket.Classes.JSON;
using RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class QuoteGenerated
    {
        public int active_id { get; set; }
        public string symbol { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
        public double value { get; set; }
        public int volume { get; set; }
        [JsonConverter(typeof(UTSecondsJsonConverter))]
        public DateTimeOffset time { get; set; }
        public bool round { get; set; }
        public bool closed { get; set; }
        public Enumerations.Phase phase { get; set; }
    }
}