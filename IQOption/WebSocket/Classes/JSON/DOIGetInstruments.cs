using RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class DOIGetInstruments
    {
        public class Instrument
        {
            public class Data
            {
                //public string strike;
                public string symbol;
                public Enumerations.Direction direction;
            }

            public int index;
            public Enumerations.InstrumentType instrument_type;
            public int asset_id;
            public int user_group_id;
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset expiration;
            public Enumerations.Period period;
            public double quote;
            public double volatility;
            [JsonConverter(typeof(UTSecondsJsonConverter))]
            public DateTimeOffset generated_at;
            public List<Data> data;
            //public int deadtime;
            //public int buyback_deadtime;
        }

        public List<Instrument> instruments;
    }
}