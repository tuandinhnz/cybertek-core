using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Cybertek.Common.Converters
{
    public class CustomJObjectConverter : JsonConverter
    {
        private static readonly Type JObjectType = typeof(JObject);

        public CustomJObjectConverter(NamingStrategy deserializationNamingStrategy = null, NamingStrategy serializationNamingStrategy = null)
        {
            SerializationNamingStrategy = serializationNamingStrategy ?? new SerializationNamingStrategy();
            DeserializationNamingStrategy = deserializationNamingStrategy ?? new DeserializationNamingStrategy();
        }

        protected NamingStrategy SerializationNamingStrategy { get; }
        protected NamingStrategy DeserializationNamingStrategy { get; }

        public override bool CanRead => true;
        public override bool CanWrite => true;

        public void IgnoreDeserializationPropertyNames(params string[] propertyNames)
        {
            if (DeserializationNamingStrategy is CustomNamingStrategy namingStrategy)
            {
                foreach (string propertyName in propertyNames)
                {
                    //A hashset will not duplicate items, and our hashset is using a case insensitive comparer
                    namingStrategy.IgnoredPropertyNames.Add(propertyName.ToLowerInvariant());
                }
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return JObjectType == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = new ExpandoObject();
            serializer.Populate(reader, value);
            JObject jObject = JObject.FromObject(value, serializer);

            //THIS works but doesnt take into account converters
            //            JObject jObject = JObject.Load(reader);

            ApplyNamingStrategyForToken(jObject, DeserializationNamingStrategy, false);

            return jObject;
        }

        protected void ApplyNamingStrategyForToken(JToken value, NamingStrategy namingStrategy, bool isWrite)
        {
            switch (value)
            {
                case JObject obj:
                    SetupMetadata(isWrite, obj);

                    obj
                        .Properties()
                        .ToList()
                        .ForEach(property => ApplyNamingStrategyForProperty(property, namingStrategy, isWrite));

                    RestoreMetadata(isWrite, obj);

                    break;
                case JArray array:
                    array.ToList().ForEach(token => ApplyNamingStrategyForToken(token, namingStrategy, isWrite));
                    break;
            }
        }

        private static void RestoreMetadata(bool isWrite, JObject obj)
        {
            if (isWrite)
            {
                return;
            }

            JToken metadata = obj.GetValue("Metadata", StringComparison.OrdinalIgnoreCase);
            if (obj.GetValue("_meta", StringComparison.OrdinalIgnoreCase) == null && metadata != null)
            {
                obj.Add("_meta", metadata);
            }
        }

        private static void SetupMetadata(bool isWrite, JObject obj)
        {
            if (!isWrite)
            {
                return;
            }

            JToken metadata = obj.GetValue("Metadata", StringComparison.OrdinalIgnoreCase);
            if (metadata != null)
            {
                obj.Remove("_meta");
            }
        }

        protected void ApplyNamingStrategyForProperty(JProperty property, NamingStrategy serializationNamingStrategy, bool isWrite)
        {
            string propertyName = serializationNamingStrategy.GetPropertyName(property.Name, false);
            var newProperty = new JProperty(propertyName, property.Value);
            property.Replace(newProperty);

            if (serializationNamingStrategy is CustomNamingStrategy namingStrategy && namingStrategy.ShouldIgnore(propertyName))
            {
                return;
            }

            ApplyNamingStrategyForToken(newProperty.Value, serializationNamingStrategy, isWrite);
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JObject jObject = (value as JObject)?.DeepClone() as JObject ?? JObject.FromObject(value, serializer);

            ApplyNamingStrategyForToken(jObject, SerializationNamingStrategy, true);

            jObject.WriteTo(writer, serializer.Converters.ToArray());
        }
    }
}
