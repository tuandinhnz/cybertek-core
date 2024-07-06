using Newtonsoft.Json;

namespace Cybertek.Common.Converters;

public class StringTrimmingConverter : JsonConverter
{
    public override bool CanRead => true;
    public override bool CanWrite => false;

    public override void WriteJson(JsonWriter writer,
        object? value,
        JsonSerializer serializer)
    {
        throw new InvalidOperationException("This converter is only for reading.");
    }

    public override object? ReadJson(JsonReader reader,
        Type objectType,
        object? existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String && reader.Value != null)
        {
            return (reader.Value as string)?.Trim();
        }

        return reader.Value;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(string);
    }
}
