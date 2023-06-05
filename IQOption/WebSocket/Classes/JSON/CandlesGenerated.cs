using Newtonsoft.Json;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class CandlesGenerated : Candle
    {
        public class Candles
        {
            [JsonProperty("1")]
            public Candle M1;
        }

        public int active_id;
        //public ? at;
        public double ask;
        public double bid;
        public double value;
        //public Enumerations.Phase phase;
        public Candles candles;
    }
}