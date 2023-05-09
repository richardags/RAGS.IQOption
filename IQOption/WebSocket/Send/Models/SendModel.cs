using IQOption.WebSocket.Classes.JSON;
using IQOption.WebSocket.Send.Classes;
using Newtonsoft.Json.Linq;

namespace IQOption.WebSocket.Send.Models
{
    internal class SendModel : DigitalOptionInstruments
    {
        internal enum Name
        {
            authenticate,
            setOptions,

            api_option_init_all,

            sendMessage,
            subscribeMessage,
            unsubscribeMessage
        }

        internal static string ToJSON(Name name, JObject msg, string request_id = null)
        {
            JObject json = new JObject();
            json.Add("msg", msg);
            json.Add("name", name.ToString());
            if (request_id != null)
            {
                json.Add("request_id", request_id);
            }

            return json.ToString();
        }
    }
}