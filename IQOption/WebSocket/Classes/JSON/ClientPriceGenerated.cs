using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Classes.JSON
{
    public class ClientPriceGenerated
    {
        public class Price
        {
            public class Direction
            {
                public string symbol;
                public double ask;
                public double bid;
            }

            public string strike;
            public Direction call;
            public Direction put;
        }

        public int instrument_index;
        public Enumerations.InstrumentType instrument_type;
        public int asset_id;
        public int user_group_id;
        public List<Price> prices;

        private static double ConvertPriceAskToProfit(double price_ask)
        {
            return Math.Round((100 - price_ask) * 100 / price_ask, 2);
        }
        public double GetPayout(Enumerations.Direction direction)
        {
            Price price = prices.Find(e => e.strike == "SPT");

            if(direction == Enumerations.Direction.call)
            {
                return ConvertPriceAskToProfit(price.call.ask);
            }else if(direction == Enumerations.Direction.put)
            {
                return ConvertPriceAskToProfit(price.put.ask);
            }
            else
            {
                throw new Exception("PriceGenerated.class GetPayout() - direction error: " + direction);
            }
        }
    }
}