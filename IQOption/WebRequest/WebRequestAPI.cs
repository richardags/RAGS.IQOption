using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RAGS.IQOption.WebRequest
{
    internal class WebRequestAPI
    {
        internal class GetSSIDResult
        {
            public static HttpStatusCode IncorretCredentials = HttpStatusCode.Forbidden;

            public RequestError error;
            public string ssid;
            public bool isCache;

            public GetSSIDResult(string ssid, bool isCache)
            {
                this.ssid = ssid;
                this.isCache = isCache;
            }
            public GetSSIDResult(RequestError error)
            {
                this.error = error;
            }
        }

        internal static GetSSIDResult GetSSID(string email, string password)
        {
            TradeRequest.Parameters parameters =
                     new TradeRequest.Parameters(email, password, TradeRequest.Parameters.Method.login);
            parameters.extras.Add("email", email);
            parameters.extras.Add("password", password);

            RequestResult result = TradeRequest.Request(parameters);

            if (result.error == null)
            {
                if (result.data != null)
                {
                    if (result.code == HttpStatusCode.OK)
                    {
                        JObject json = JObject.Parse(result.data);
                        string ssid = (string)json["data"]["ssid"];

                        return new GetSSIDResult(ssid, false);
                    }
                    else if (result.code == HttpStatusCode.Forbidden)
                    {
                        return new GetSSIDResult(
                            new RequestError(GetSSIDResult.IncorretCredentials, result.data));
                    }
                    else
                    {
                        return new GetSSIDResult(new RequestError(result.code, result.data));
                    }
                }
                else
                {
                    return new GetSSIDResult(new RequestError(result.code));
                }
            }
            else
            {
                Console.WriteLine(string.Format("WebRequestAPI.class GetSSID() - error: {0} {1}", result.error.code, result.error.message));

                return new GetSSIDResult(result.error);
            }
        }
    }
}