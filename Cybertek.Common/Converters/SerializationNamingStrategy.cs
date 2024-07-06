using Humanizer;
using static Cybertek.Common.Converters.CustomPropertyMappings;

namespace Cybertek.Common.Converters
{
    public class SerializationNamingStrategy : CustomNamingStrategy
    {
        public SerializationNamingStrategy() 
            : base(SerializationMappings, SerializationIgnoredPropertyNames)
        {
        }

        public SerializationNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames) 
            : base(SerializationMappings, SerializationIgnoredPropertyNames, processDictionaryKeys, overrideSpecifiedNames)
        {
        }

        public override string GetDictionaryKey(string key)
        {
            string result = base.GetDictionaryKey(key);
            return ShouldIgnore(key) ? result : result.Camelize();
        }

        protected override string ResolvePropertyName(string name)
        {
            string result = base.ResolvePropertyName(name);

            if (string.Equals(name, result))
            {
                return ShouldIgnore(name) ? result : result.Camelize();
            }

            return result;
        }
    }
}
