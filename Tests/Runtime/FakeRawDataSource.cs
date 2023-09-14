namespace Hermer29.Foundation.Tests.Tests.Runtime
{
    public class FakeRawDataSource : ISourceAdapter
    {
        private string _value;
        
        public void SetValue(string value)
        {
            _value = value;
        }

        public string GetValue()
        {
            return _value;
        }

        public void Save()
        {
            
        }
    }
}