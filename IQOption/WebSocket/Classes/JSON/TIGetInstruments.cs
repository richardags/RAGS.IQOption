using RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class TIGetInstruments
    {
        public class Instrument
        {
            public enum Active
            {
                AUDCAD,
                AUDCHF,
                AUDJPY,
                AUDNZD,
                AUDUSD,
                CADCHF,
                CADJPY,
                CHFJPY,
                EURAUD,
                EURCAD,
                EURCHF,
                EURGBP,
                EURJPY,
                EURNZD,
                EURUSD,
                GBPCAD,
                GBPJPY,
                GBPUSD,
                NZDCAD,
                NZDCHF,
                NZDJPY,
                NZDUSD,
                USDCAD,
                USDCHF,
                USDJPY
            }
            public enum ActiveType
            {
                Forex,
                Crypto,
                Stock,
            }

            public Active ticker;
            public bool is_visible;
            public Active id;
            public int active_id;
            public int active_group_id;
            public ActiveType active_type;
            public Active underlying;
            //public List<Schedule> schedule;
            public Active name;
            //public ? localization_key;
            public int precision;
            [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
            public DateTimeOffset start_time;
            public bool is_suspended;
        }

        public int user_group_id;
        //public bool is_regulated;
        public Enumerations.InstrumentType type;
        public List<Instrument> instruments;
    }
}