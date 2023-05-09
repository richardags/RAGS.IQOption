using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQOption.WebSocket.Classes.JSON.Converter
{
    internal class InitializationDataJsonConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(List<InitializationData.Active>);
        }
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            //check if empty
            //"binary":{"actives":[],"list":[]},"turbo":{"actives":[],"list":[]}

            List<InitializationData.Active> actives = new List<InitializationData.Active>();
            JToken jToken = JToken.Load(reader);

            if (jToken.Type == JTokenType.Object)
            {
                foreach (JProperty prop in jToken.Children<JProperty>())
                {
                    //Active active = prop.Value.ToObject<Active>();
                    //active.Name = prop.Name;
                    actives.Add(prop.Value.ToObject<InitializationData.Active>());
                }
            }

            return actives;
        }
        public override bool CanWrite
        {
            get { return false; }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}