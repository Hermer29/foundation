using System.Collections.Generic;
using Hermer29.Foundation.Internal.KeyValueDataStructure;

namespace Hermer29.Foundation.Internal
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, string> ToDictionary(this KeyValues keyValuePairs)
        {
            var dictionary = new Dictionary<string, string>();
            if (keyValuePairs == null || keyValuePairs.values == null)
            {
                return dictionary;
            }
            foreach (var pairs in keyValuePairs.values)
            {
                dictionary.Add(pairs.key, pairs.value);
            }

            return dictionary;
        }
    }
}