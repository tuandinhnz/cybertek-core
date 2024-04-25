namespace Cybertek.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetInsensitive<TValue>(this IDictionary<string, TValue> dictionary, string key)
        {
            return dictionary.FirstOrDefault(pair => string.Equals(pair.Key, key, StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }
}
