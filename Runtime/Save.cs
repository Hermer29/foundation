using System.Linq;
using Hermer29.Foundation.Internal;

namespace Hermer29.Foundation
{
    public class Save
    {
        private readonly SourceSerializer[] _adapters;

        private Save(ISourceAdapter[] adapters)
        {
            _adapters = new SourceSerializer[adapters.Length];
            for (var i = 0; i < adapters.Length; i++)
            {
                _adapters[i] = new SourceSerializer(adapters[i]);
            }
        }

        public static Save Create(params ISourceAdapter[] adapters)
        {
            var save = new Save(adapters);
            TryResolveVersionIncompatibilities(save);
            return save;
        }

        private static void TryResolveVersionIncompatibilities(Save save)
        {
            int? version = null;
            bool incompatibilityDetected = false;
            SourceSerializer leadingSource = null;
            SavedRecord leadingVersion = null;
            foreach (SourceSerializer sourceAdapterProcessor in save._adapters)
            {
                var record = sourceAdapterProcessor.GetRecord();
                if (version == null)
                {
                    version = record.version;
                    leadingSource = sourceAdapterProcessor;
                    leadingVersion = record;
                }
                if (record.version > version)
                {
                    incompatibilityDetected = true;
                    leadingSource = sourceAdapterProcessor;
                    leadingVersion = record;
                }
            }
            ResolveVersionIncompatibilities(when: incompatibilityDetected, save, leadingSource, leadingVersion);
        }

        private static void ResolveVersionIncompatibilities(bool when, Save save, SourceSerializer freshiestSource,
            SavedRecord freshiestRecord)
        {
            if (!when)
                return;
            foreach (SourceSerializer sourceAdapterProcessor in save._adapters.Except(new [] {freshiestSource}))
            {
                sourceAdapterProcessor.SetRecord(freshiestRecord);
            }
        }
        
        public void SetValue(string value)
        {
            SavedRecord record = GetRecord();
            record.version++;
            record.value = value;
            foreach (SourceSerializer sourceAdapter in _adapters)
            {
                sourceAdapter.SetRecord(record);
            }
        }

        private SavedRecord GetRecord()
        {
            SourceSerializer first = _adapters[0];
            SavedRecord record = first.GetRecord();
            return record;
        }

        public string GetValue()
        {
            SavedRecord record = GetRecord();
            return record.value;
        }
    }
}