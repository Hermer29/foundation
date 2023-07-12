using System.Collections.Generic;

namespace Hermer29.Foundation.Internal.KeyValueDataStructure
{
    [System.Serializable]
    public class KeyValues
    {
        public StringKeyValuePair[] values;

        public static KeyValues CreateFromDictionary(Dictionary<string, string> dictionary)
        {
            var keyValuePairs = new KeyValues();
            keyValuePairs.values = new StringKeyValuePair[dictionary.Count];
            var counter = 0;
            if (dictionary.Count == 0)
                return new KeyValues();
            foreach (var keyValuePair in dictionary)
            {
                var current = new StringKeyValuePair();
                current.value = keyValuePair.Value;
                current.key = keyValuePair.Key;
                keyValuePairs.values[counter] = current;
                counter++;
            }

            return keyValuePairs;
        }
    }
}