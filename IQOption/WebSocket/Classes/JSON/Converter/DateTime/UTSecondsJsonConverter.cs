using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IQOption.WebSocket.Classes.JSON.Converter.DateTime
{
    internal class UTSecondsJsonConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is long)
            {
                return DateTimeOffset.FromUnixTimeSeconds((long)reader.Value);
            }
            else if (reader.Value is string)
            {
                return DateTimeOffset.FromUnixTimeSeconds(long.Parse((string)reader.Value));
            }
            else /*if (reader.Value == null)*/
            {
                return null;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((DateTimeOffset)value).ToUnixTimeSeconds());
        }
    }
}