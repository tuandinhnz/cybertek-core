using Newtonsoft.Json;

namespace Cybertek.Apis.Common.Mappers;

public interface IJsonSerializerSettingsProvider
{
    JsonSerializerSettings GetSettings();
}
