using RAGS.IQOption.WebSocket.Send.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGS.IQOption.WebSocket.Send.Classes
{
    internal class SetOptions
    {        
        internal static string SendResults(string request_id = null)
        {
            JObject msg = new JObject();
            msg.Add("sendResults", true);

            return SendModel.ToJSON(SendModel.Name.setOptions, msg, request_id);
        }
    }
}