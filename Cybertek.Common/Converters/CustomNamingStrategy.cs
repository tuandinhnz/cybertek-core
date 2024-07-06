using Newtonsoft.Json.Serialization;

namespace Cybertek.Common.Converters;

public abstract class CustomNamingStrategy : CamelCaseNamingStrategy
{
    protected CustomNamingStrategy(Dictionary<string, string> propertyMappings,
        HashSet<string> ignoredPropertyNames,
        bool processDictionaryKeys,
        bool overrideSpecifiedNames) 
        : this(propertyMappings, ignoredPropertyNames)
    {
        ProcessDictionaryKeys = processDictionaryKeys;
        OverrideSpecifiedNames = overrideSpecifiedNames;
    }

    protected CustomNamingStrategy(Dictionary<string, string> propertyMappings, HashSet<string> ignoredPropertyNames)
    {
        PropertyMappings = propertyMappings;
        IgnoredPropertyNames = ignoredPropertyNames;
    }

    public HashSet<string> IgnoredPropertyNames { get; }
    protected Dictionary<string, string> PropertyMappings { get; }

    public override string GetDictionaryKey(string key)
    {
        if (!ProcessDictionaryKeys || ShouldIgnore(key))
        {
            return base.GetDictionaryKey(key);
        }

        bool resolved = PropertyMappings.TryGetValue(key, out string resolvedName);
        return resolved ? resolvedName : base.GetDictionaryKey(key);
    }

    protected override string ResolvePropertyName(string name)
    {
        if (ShouldIgnore(name))
        {
            return name;
        }

        bool resolved = PropertyMappings.TryGetValue(name, out string resolvedName);
        return resolved ? resolvedName : name;
    }

    public bool ShouldIgnore(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return true;
        }

        var ignoredPropertyNames = new List<string>(IgnoredPropertyNames);
        string lowerKey = key.ToLowerInvariant();
        return ignoredPropertyNames.Any(n => n == lowerKey);
    }
}
