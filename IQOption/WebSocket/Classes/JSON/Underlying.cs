using RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class Underlying
    {
        public enum Active
        {
            EURUSD,
            EURGBP,
            GBPJPY,
            EURJPY,
            GBPUSD,
            USDJPY,
            AUDCAD,
            USDCHF,
            [EnumMember(Value = "EURUSD-OTC")]
            EURUSD_OTC,
            [EnumMember(Value = "EURGBP-OTC")]
            EURGBP_OTC,
            [EnumMember(Value = "USDCHF-OTC")]
            USDCHF_OTC,
            [EnumMember(Value = "EURJPY-OTC")]
            EURJPY_OTC,
            [EnumMember(Value = "NZDUSD-OTC")]
            NZDUSD_OTC,
            [EnumMember(Value = "GBPUSD-OTC")]
            GBPUSD_OTC,
            [EnumMember(Value = "GBPJPY-OTC")]
            GBPJPY_OTC,
            [EnumMember(Value = "USDJPY-OTC")]
            USDJPY_OTC,
            [EnumMember(Value = "AUDCAD-OTC")]
            AUDCAD_OTC,

            [EnumMember(Value = "USDZAR-OTC")]
            USDZAR_OTC,
            [EnumMember(Value = "USDSGD-OTC")]
            USDSGD_OTC,
            [EnumMember(Value = "USDHKD-OTC")]
            USDHKD_OTC,
            [EnumMember(Value = "USDINR-OTC")]
            USDINR_OTC,

            AUDUSD,
            AUDJPY,
            USDCAD,
            GBPCAD,
            GBPCHF,
            GBPAUD,
            EURCAD,
            EURAUD,
            CADJPY,
            EURCHF,
            GBPNZD,

            BTCUSD,
            XRPUSD,
            ETHUSD,
            LTCUSD,
            EOSUSD,
            NZDUSD,
            XAUUSD,
            USOUSD
        }
        public enum ActiveType
        {
            Forex,
            Crypto,
            Stock,
            Commodity
        }

        public int active_id;
        public int active_group_id;
        public ActiveType active_type;
        public Active underlying;
        //public ? schedule;
        public bool is_enabled;
        public Active name;
        //public ? localization_key;
        public int precision;
        [JsonConverter(typeof(UTMilliSecondsJsonConverter))]
        public DateTimeOffset start_time;
        //public ? regulation_mode;
        public bool is_suspended;
    }
}