using System.Collections.Generic;
using IQOption.WebSocket.Classes.JSON.Converter;
using Newtonsoft.Json;

namespace IQOption.WebSocket.Classes.JSON
{
    public class InitializationData
    {
        public class Active
        {
            public class Option
            {
                public class Profit
                {
                    public int commission;
                }
                public int exp_time;
                public Profit profit;
            }

            public int id;
            public string name;
            public int minimal_bet;
            public int maximal_bet;
            public Option option;
            public bool enabled;

            public int GetProfit()
            {
                return 100 - option.profit.commission;
            }
        }

        public class Binary
        {
            [JsonConverter(typeof(InitializationDataJsonConverter))]
            public List<Active> actives;
        }
        public class Turbo
        {
            [JsonConverter(typeof(InitializationDataJsonConverter))]
            public List<Active> actives;
        }

        public Binary binary;
        public Turbo turbo;
    }
}