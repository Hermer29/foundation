using Hermer29.Foundation.Tests.Tests.Runtime;
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
        
        [Test]
        public void WhenDataIsEmpty_TryCreate_ThenSourcesMustHaveSameData()
        {
            // arrange
            var fakeDataB = new FakeRawDataSource();
            var fakeDataA = new FakeRawDataSource();
            var forSetKeyInArrange = KeyValueSave.Create(fakeDataA);
            forSetKeyInArrange.SetInt("ABRA", 1);

            // act
            var sources = new ISourceAdapter[] { fakeDataA, fakeDataB };
            var keyValueSave = KeyValueSave.Create(sources);
            
            // assert
            Assert.That(keyValueSave.GetInt("ABRA") == 1);
        }
        
        [Test]
        public void WhenDataIsSetTwice_TryCreate_ThenVersionShouldBeThree()
        {
            //STUPID
            // arrange
            var fakeDataB = new FakeDataSource(1, "");
            var saveForArrange = KeyValueSave.Create(fakeDataB);
            saveForArrange.SetInt("ABRA", 2);
            saveForArrange.SetInt("CADABRA", 2);
            
            // act
            var sources = new ISourceAdapter[] { fakeDataB };
            var keyValueSave = KeyValueSave.Create(sources);
            
            // assert
            Assert.That(fakeDataB.Record.version == 3);
        }
    }
}