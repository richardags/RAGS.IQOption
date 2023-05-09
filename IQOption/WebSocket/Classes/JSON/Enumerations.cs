using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON
{
    public class Enumerations
    {
        public enum Direction
        {
            call = 1,
            put = 2,
            equal = 3
        }

        //HINT - DELETE BELLOW
        public enum BinaryOptionType
        {
            binary = 1, turbo = 2
        }
        public enum Phase
        {
            T
        }
                
        public enum InstrumentType
        {
            [EnumMember(Value = "binary-option")]
            binary_option,
            [EnumMember(Value = "turbo-option")]
            turbo_option,
            [EnumMember(Value = "digital-option")]
            digital_option,
            crypto,
            forex,
            cfd
        }
        public enum Period
        {
            M1 = 60,
            M5 = 300,
            M15 = 900,
            M30 = 1800, //not found this enum value in websocket send/response
            H1 = 3600, //not found this enum value in websocket send/response
        }
        public enum CandleSize
        {
            S1 = 1,
            S5 = 5,
            S10 = 10,
            S15 = 15,
            S30 = 30,
            S60 = 60,
            S120 = 120,
            S300 = 300,
            S600 = 600,
            S900 = 900,
            S1800 = 1800,
            S3600 = 3600,
            S7200 = 7200,
            S14400 = 14400,
            S28800 = 28800,
            S43200 = 43200,
            S86400 = 86400,
            S604800 = 604800,
            S2592000 = 2592000
        }
    }
}