using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON
{
    public class CoreGetBalances
    {
        public class Balance
        {
            public int id { get; set; }
            public int user_id { get; set; }
            public int type { get; set; }
            public double amount { get; set; }
            public string currency { get; set; }
        }
        public enum Type
        {
            REAL = 1,
            DEMO = 4
        }

        [JsonProperty("msg")]
        public List<Balance> balances;

        public Balance GetBalance(Type type)
        {
            return balances.Find(e => e.type == (int)type);
        }
    }
}