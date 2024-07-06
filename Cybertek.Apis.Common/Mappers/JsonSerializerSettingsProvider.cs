using Cybertek.Common.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cybertek.Apis.Common.Mappers;

public class JsonSerializerSettingsProvider : IJsonSerializerSettingsProvider
{
    public virtual JsonSerializerSettings GetSettings()
    {
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = CustomContractResolver.Instance,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            DateParseHandling = DateParseHandling.DateTimeOffset
        };

        ConfigureSerializationSettings(serializerSettings);

        return serializerSettings;
    }

    protected virtual void ConfigureSerializationSettings(JsonSerializerSettings serializerSettings)
    {
        serializerSettings.Converters.Add(new StringEnumConverter());
        serializerSettings.Converters.Add(new StringTrimmingConverter());
        serializerSettings.Converters.Add(new CustomJObjectConverter());
        serializerSettings.Converters.Add(new CustomExpandoObjectConverter());
    }
}
