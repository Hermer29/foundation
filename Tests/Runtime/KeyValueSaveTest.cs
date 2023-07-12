using NUnit.Framework;
using Tests.Runtime;

namespace Hermer29.Foundation.Tests.Runtime
{
    public class KeyValueSaveTest
    {
        [Test]
        public void WhenTwoKeyValueContainers_TrySetByKey_ThenShouldReceiveSameValue()
        {
            // arrange
            const string key = "adafas";
            const string value = "afafsaas";
            const int versionFromB = 7;
            var fakeDataB = new FakeDataSource(versionFromB, "");
            var keyValueSave = KeyValueSave.Create(fakeDataB);
            
            // act
            keyValueSave.SetString(key, value);
            var receive = keyValueSave.GetString(key);

            // assert
            Assert.That(receive == value);
        }
    }
}