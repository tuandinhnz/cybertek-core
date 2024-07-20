using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using static Cybertek.Common.Converters.CustomPropertyMappings;

namespace Cybertek.Common.Converters;

public class CustomExpandoObjectConverter : ExpandoObjectConverter
{
    private static readonly Type ObjectType = typeof(object);

    public CustomExpandoObjectConverter(NamingStrategy namingStrategy = null)
    {
        NamingStrategy = namingStrategy ?? new DeserializationNamingStrategy();
    }

    private NamingStrategy NamingStrategy { get; set; }

    public override bool CanConvert(Type objectType)
    {
        return ObjectType == objectType || base.CanConvert(objectType) || IsDictionaryType(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        object model = base.ReadJson(reader, objectType, existingValue, serializer);

        var expandoModel = model as ExpandoObject;
        if (expandoModel == null)
        {
            return model;
        }

        ExpandoObject processedModel = ProcessSubProperties(expandoModel);

        if (IsDictionaryType(objectType))
        {
            //NOTE:  dictionary property cannot be assigned from expando object, lists can
            return new Dictionary<string, dynamic>(processedModel);
        }

        return processedModel;
    }

    private static bool IsDictionaryType(Type type)
    {
        return type == typeof(Dictionary<string, dynamic>) ||
               type == typeof(Dictionary<string, ExpandoObject>) ||
               type == typeof(Dictionary<string, object>) ||
               type == typeof(IDictionary<string, dynamic>) ||
               type == typeof(IDictionary<string, ExpandoObject>) ||
               type == typeof(IDictionary<string, object>);
    }

    private ExpandoObject ProcessSubProperties(ExpandoObject input)
    {
        if (input == null)
        {
            return null;
        }

        IDictionary<string, object> expandoDictionary = input;
        var property = new ExpandoObject();

        if (!expandoDictionary.Any())
        {
            return input;
        }

        foreach (string key in expandoDictionary.Keys)
        {
            object subProperty = expandoDictionary[key];
            if (subProperty == null)
            {
                continue;
            }

            if (subProperty is ExpandoObject)
            {
                if (!DeserializationIgnoredPropertyNames.Contains(key.ToLowerInvariant()))
                {
                    subProperty = ProcessSubProperties((dynamic) subProperty);
                }
            }

            AddProperty(property, NamingStrategy.GetPropertyName(key, false), subProperty);
        }

        return property;
    }

    private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
    {
        IDictionary<string, object> expandoDictionary = expando;
        expandoDictionary[propertyName] = propertyValue;
    }
}
