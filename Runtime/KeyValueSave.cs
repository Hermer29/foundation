using System.Collections.Generic;
using System.Linq;
using Hermer29.Foundation.Internal;
using Hermer29.Foundation.Internal.KeyValueDataStructure;
using UnityEngine;

namespace Hermer29.Foundation
{
    public class KeyValueSave
    {
        private readonly Save _save;
        private static IEnumerable<ISourceAdapter> _adapters;
        private Dictionary<string, string> _keyValues = new Dictionary<string, string>();

        private KeyValueSave(Save save)
        {
            _save = save;
        }

        public static KeyValueSave Create(IEnumerable<ISourceAdapter> adapters)
        {
            _adapters = adapters;
            var save = Save.Create(adapters.ToArray());
            if (string.IsNullOrEmpty(save.GetValue()))
            {
                return new KeyValueSave(save);
            }
            var deserialized = JsonUtility.FromJson<KeyValues>(save.GetValue());
            var keyValueSave = new KeyValueSave(save)
            {
                _keyValues = deserialized.ToDictionary()
            };
            return keyValueSave;
        }

        public static KeyValueSave Create(params ISourceAdapter[] adapters) => Create(adapters.AsEnumerable());

        public void SetInt(string key, int value)
        {
            _keyValues[key] = value.ToString();
            Replicate();
        }

        public void SetString(string key, string value)
        {
            _keyValues[key] = value;
            Replicate();
        }
        
        public bool HasKey(string key)
        {
            return _keyValues.ContainsKey(key);
        }

        public string GetString(string key) => _keyValues[key];

        public int GetInt(string key) => int.Parse(GetString(key));

        private void Replicate()
        {
            string utility = JsonUtility.ToJson(KeyValues.CreateFromDictionary(_keyValues));
            _save.SetValue(utility);
        }

        public void ReloadFromSources()
        {
            var save = Save.Create(_adapters.ToArray());
            var deserialized = JsonUtility.FromJson<KeyValues>(save.GetValue());
            _keyValues = deserialized.ToDictionary();
        }

        public void SaveChanges()
        {
            _save.Apply();
        }
    }
}