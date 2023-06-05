using HttpClientHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;

namespace RAGS.IQOption.WebRequest
{
    internal class TradeRequest
    {
        private static readonly string URL = "https://auth.iqoption.com";
        private static readonly string PATCH = "/api/v1.0/login";

        internal class Parameters
        {
            public enum Method
            {
                login
            }

            public readonly Method method;
            public readonly string email;
            public readonly string password;

            public Dictionary<string, object> extras = new Dictionary<string, object>();

            public Parameters(string email, string password, Method method)
            {
                this.email = email;
                this.password = password;
                this.method = method;
            }
        }

        internal static RequestResult Request(Parameters parameters)
        {
            HttpClientHelperResult result = HttpClientHelperNS.Response(URL + PATCH, HttpMethod.Post, contentType: ContentType.URLEncoded, keyPairContent: parameters.extras);

            if (result.error == null)
            {
                return new RequestResult(result.data, result.httpStatusCode);
            }
            else
            {
                Console.WriteLine("TradeRequest.cs supprimed: " + result.httpStatusCode);

                return new RequestResult(new RequestError(result.httpStatusCode, result.data));
            }

            /*
            try
            {
                string data_string_write = GetURLEncoded(parameters);
                byte[] data_byte_write = Encoding.UTF8.GetBytes(data_string_write);

                HttpWebRequest request = System.Net.WebRequest.Create(URL + PATCH) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data_byte_write.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data_byte_write, 0, data_byte_write.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        if (response != null)
                        {
                            return new RequestResult(reader.ReadToEnd(), response.StatusCode);
                        }
                        else
                        {
                            return new RequestResult(reader.ReadToEnd(), HttpStatusCode.ExpectationFailed);
                        }
                    }
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    return new RequestResult(
                        new RequestError(((HttpWebResponse)we.Response).StatusCode, we.Status.ToString()));
                }
                else //(HttpWebResponse)we.Response is null
                {
                    Console.WriteLine("TradeRequest.cs supprimed: " + we.Status);
                    return new RequestResult(
                        new RequestError(HttpStatusCode.ExpectationFailed, we.Status.ToString()));
                }
            }
            */

            //request.CookieContainer = new CookieContainer();
            //request.CookieContainer.Add(new Cookie("ssid", key, "/", "iqoption.com"));
            //error code 400 (incorrect PostData) and 403 (useranme wrong)
        }

        /*
        private static string GetURLEncoded(Parameters parameters)
        {
            return string.Format("{0}",
                parameters.extras.Count == 0 ? "" :
                 string.Join("&", parameters.extras.Select(
                        e => string.Format("{0}={1}", e.Key, e.Value))));
        }
        */
    }
}