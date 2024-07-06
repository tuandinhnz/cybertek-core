namespace Cybertek.Common.Converters;

public static class CustomPropertyMappings
{
    public static readonly HashSet<string> SerializationIgnoredPropertyNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    public static readonly HashSet<string> DeserializationIgnoredPropertyNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

    public static readonly Dictionary<string, string> SerializationMappings = new Dictionary<string, string>
    {
        {"Metadata", "_meta"},
        {"QueryString", "query_string"}
    };

    public static readonly Dictionary<string, string> DeserializationMappings = new Dictionary<string, string>
    {
        {"_meta", "Metadata"},
        {"query_string", "QueryString"},
        {"queryString", "QueryString"}
    };
}
