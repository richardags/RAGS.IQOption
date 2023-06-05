using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    internal class Initialization
    {
        internal static string GetInitializationData(string request_id = null)
        {
            JObject msg = Message.ToJObject(
                Message.Name.get_initialization_data,
                Message.Version.v3,
                new JObject());

            return SendModel.ToJSON(SendModel.Name.sendMessage, msg, request_id);
        }
    }
}