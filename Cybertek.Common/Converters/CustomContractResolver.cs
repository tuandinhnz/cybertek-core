using Newtonsoft.Json.Serialization;

namespace Cybertek.Common.Converters;

public class CustomContractResolver : CamelCasePropertyNamesContractResolver
{
    public static readonly CustomContractResolver Instance = new CustomContractResolver();

    private CustomContractResolver()
    {
        NamingStrategy = new SerializationNamingStrategy(true, false); 
    }
}
