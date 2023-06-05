using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RAGS.IQOption.WebRequest
{
    internal static class SSIDManager
    {
        private static readonly string CurrentDirectory =
            Path.Combine(Directory.GetCurrentDirectory(), "SSID");

        private static string GetPath(string email)
        {
            return Path.Combine(CurrentDirectory, email + ".txt");
        }

        internal static WebRequestAPI.GetSSIDResult Get(string email, string password, bool fromCache)
        {
            if (fromCache)
            {
                string ssid = Import(email);

                if (!string.IsNullOrEmpty(ssid))
                {
                    return new WebRequestAPI.GetSSIDResult(ssid, true);
                }
            }

            WebRequestAPI.GetSSIDResult ssidResult = WebRequestAPI.GetSSID(email, password);

            if(ssidResult.error == null)
            {
                Export(email, ssidResult.ssid);
            }

            return ssidResult;
        }

        private static string Import(string email)
        {
            if (File.Exists(GetPath(email)))
            {
                return File.ReadAllText(GetPath(email));
            }
            else
            {
                return null;
            }
        }
        private static void Export(string email, string newSSID)
        {
            Directory.CreateDirectory(CurrentDirectory);
            File.WriteAllText(GetPath(email), newSSID);
        }
        internal static void Delete(string email)
        {
            File.Delete(GetPath(email));
        }
    }
}