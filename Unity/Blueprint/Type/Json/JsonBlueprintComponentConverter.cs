using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SirMetathyst.Entitas.Blueprints.Unity
{
    public class JsonBlueprintComponentConverter : JsonConverter
    {
        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException ();
        }

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new JsonComponentBlueprint ();
            JObject obj = serializer.Deserialize<JObject> (reader);
            serializer.Populate (obj.CreateReader (), result);
            result.value = obj.GetValue ("value", StringComparison.OrdinalIgnoreCase).ToObject (result.type, serializer);
            return result;
        }

        public override bool CanConvert (Type objectType)
        {
            return objectType == typeof (JsonComponentBlueprint);
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}