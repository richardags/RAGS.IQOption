using RAGS.IQOption.WebSocket.Classes.JSON;
using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTimeOffsetExtension;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    internal class BinaryOptions
    {
        private static DateTimeOffset MinutesToExpired(int expiration)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow; // ServerTime; //DateTimeOffset.Now;
            
            if (now.Second >= 0 && now.Second <= 30)
            {
                now = now.AddMinutes(expiration);
            }
            else
            {
                now = now.AddMinutes(expiration + 1);
            }

            return now.TruncateSecond();
        }

        internal static string OpenOption(int user_balance_id, int active_id,
            int option_type_id, Enumerations.Direction direction, DateTimeOffset expired,
            double price, string request_id = null)
        {
            RoutingFilters body = new RoutingFilters();
            body.user_balance_id = user_balance_id;
            body.active_id = active_id;
            body.option_type_id = option_type_id;
            body.direction = direction;
            body.expired = expired.ToUniversalTime().ToUnixTimeSeconds();
            body.price = price; //WARNING amount.ToString().Replace(",", ".");

            JObject msg = Message.ToJObject(
                Message.Name.binary_options0open_option,
                Message.Version.v1,
                body.ToJObject());

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
        internal static string OpenOption(int user_balance_id, int active_id,
            Enumerations.Direction direction, int expiration, double price, string request_id = null)
        {
            int option_type_id = expiration > 5 ? 1 : 3;

            DateTimeOffset expired = MinutesToExpired(expiration);

            return OpenOption(user_balance_id, active_id, option_type_id, direction, expired, price, request_id);
        }
    }
}