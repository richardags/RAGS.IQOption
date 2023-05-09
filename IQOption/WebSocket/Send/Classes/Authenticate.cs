using IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Send.Classes
{
    internal class Authenticate
    {
        internal static string Send(string ssid)
        {
            JObject msg = new JObject();
            msg.Add("ssid", ssid);
            msg.Add("protocol", 3);
            msg.Add("session_id", "");

            return SendModel.ToJSON(SendModel.Name.authenticate, msg);
        }
    }
}