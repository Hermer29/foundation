
using UnityEngine;

namespace Hermer29.Foundation.Internal
{
    internal class SourceSerializer
    {
        private readonly ISourceAdapter _adapter;

        public SourceSerializer(ISourceAdapter adapter)
        {
            _adapter = adapter;
        }

        public SavedRecord GetRecord()
        {
            string raw = _adapter.GetValue();
            
            if (string.IsNullOrEmpty(raw) || string.IsNullOrWhiteSpace(raw))
            {
                return new SavedRecord();
            }
            return JsonUtility.FromJson<SavedRecord>(raw);
        }

        public void SetValue(string value, int version)
        {
            SavedRecord record = new SavedRecord()
            {
                version = version,
                value = value
            };
            SetRecord(record);
        }

        public void SetRecord(SavedRecord record) => _adapter.SetValue(JsonUtility.ToJson(record));

        public void SaveValue()
        {
            _adapter.Save();
        }
    }
}