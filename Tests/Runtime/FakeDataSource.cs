using Hermer29.Foundation;
using Hermer29.Foundation.Internal;
using UnityEngine;

namespace Tests.Runtime
{
    public class FakeDataSource : ISourceAdapter
    {
        public SavedRecord Record;
        
        public FakeDataSource(int version, string data)
        {
            Record = new SavedRecord
            {
                version = version,
                value = data
            };
        }
        
        public void SetValue(string value)
        {
            Record = JsonUtility.FromJson<SavedRecord>(value);
        }

        public string GetValue()
        {
            return JsonUtility.ToJson(Record);
        }

        public void Save()
        {
            
        }
    }
}