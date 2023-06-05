using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RAGS.IQOption.WebSocket.Classes.JSON.Converter.DateTime
{
    internal class UTNanoSecondsJsonConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is long)
            {
                return FromUnixTimeNanoSeconds((long)reader.Value);
            }
            else if (reader.Value is string)
            {
                return FromUnixTimeNanoSeconds(long.Parse((string)reader.Value));
            }
            else /*if (reader.Value == null)*/
            {
                return null;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((DateTimeOffset)value).ToUnixTimeMilliseconds());
        }

        private static DateTimeOffset FromUnixTimeNanoSeconds(long nanoSeconds)
        {
            return DateTimeOffset.MinValue.AddYears(1969).AddTicks(nanoSeconds / 100);
        }
    }
}