using System;
using FireBaseDynamicLinksService.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FireBaseDynamicLinksService.JsonConverters
{
    public class CreateShortDynamicLinkRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
            var t = JToken.FromObject(value);
            var modified = t.RemoveFields("ETag");

            modified.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override bool CanRead => false;
    }
}