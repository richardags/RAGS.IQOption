using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Classes;
using Newtonsoft.Json.Linq;

namespace IQOption.WebSocket.Send.Models
{
    internal class Message
    {
        internal enum Version
        {
            v1 = 1,
            v2 = 2,
            v3 = 3,
            v4 = 4
        }
        internal enum Name
        {
            //Initialization
            get_initialization_data,
            //Unknown
            get_underlying_list,
            //BinaryOptions
            binary_options0open_option,
            //DigitalOptionInstruments
            digital_option_instruments0get_instruments,
            digital_option_instruments0get_underlying_list,
            //DigitalOptions
            digital_options0place_digital_option,
            subscribe_positions,
            //Risks
            get_commissions,
            //TradingInstruments
            get_instruments,
            //Core
            core0get_profile,
            get_balances,
            //Quotes
            get_candles,
            //Portfolio
            portfolio0get_positions
        }

        private static string TranslateName(Name name)
        {
            switch (name)
            {
                case Name.get_initialization_data:
                case Name.get_commissions:
                case Name.get_instruments:
                case Name.get_balances:
                case Name.get_candles:
                case Name.subscribe_positions:
                    return name.ToString().Replace("_", "-");
                case Name.binary_options0open_option:
                case Name.digital_option_instruments0get_instruments:
                case Name.digital_option_instruments0get_underlying_list:
                case Name.digital_options0place_digital_option:
                case Name.core0get_profile:
                case Name.portfolio0get_positions:
                    return name.ToString().Replace("0", ".").Replace("_", "-");
                default:
                    return name.ToString();
            }
        }
        internal static JObject ToJObject(Name name, Version version, JObject body)
        {
            JObject data = new JObject();
            data.Add("name", TranslateName(name));
            data.Add("version", string.Format("{0}.0", (int)version));
            data.Add("body", body);

            return data;
        }
    }
}