using IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON
{
    public class PositionStatePosition
    {
        public double current_price;
        public string id;
        public double margin; //investiment
        [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
        public DateTimeOffset quote_timestamp;
        //public double open_price; //is diff from current_price
        //public string instrument_type;
        //public ? expected_profit;
        //public ? sell_profit;
        //public ? pnl;
        //public ? pnl_net;
    }
}