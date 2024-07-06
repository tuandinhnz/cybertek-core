using Humanizer;
using static Cybertek.Common.Converters.CustomPropertyMappings;

namespace Cybertek.Common.Converters;

public class DeserializationNamingStrategy : CustomNamingStrategy
{
    public DeserializationNamingStrategy() 
        : base(DeserializationMappings, DeserializationIgnoredPropertyNames)
    {
    }

    public override string GetDictionaryKey(string key)
    {
        string result = base.GetDictionaryKey(key);
        return ShouldIgnore(key) ? result : result.Pascalize();
    }

    protected override string ResolvePropertyName(string name)
    {
        string result = base.ResolvePropertyName(name);
        return ShouldIgnore(name) ? result : result.Pascalize();
    }
}
