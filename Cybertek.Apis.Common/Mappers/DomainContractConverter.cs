using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Cybertek.Apis.Common.Mappers;

public class DomainContractConverter : IDomainContractConverter
{
    protected JsonSerializer Serializer { get; }
    
    public DomainContractConverter(IJsonSerializerSettingsProvider settingsProvider)
    {
        Serializer = JsonSerializer.Create(settingsProvider.GetSettings());
    }

    public object Convert(Type targetType, object source)
    {
        IContractResolver contractResolver = null;
        if (source is IJsonPatchDocument patchSource)
        {
            contractResolver = patchSource.ContractResolver;
            patchSource.ContractResolver = null;
        }

        JToken sourceToken = source == null ? JValue.CreateNull() : JToken.FromObject(source, Serializer);
        var result = sourceToken.ToObject(targetType, Serializer);

        if (result is IJsonPatchDocument patchResult)
        {
            patchResult.ContractResolver = contractResolver;
        }

        return result;
    }
}
