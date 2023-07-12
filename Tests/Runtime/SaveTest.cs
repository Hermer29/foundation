using NUnit.Framework;
using Tests.Runtime;

namespace Hermer29.Foundation.Tests.Runtime
{
    public class SaveTest
    {
        [Test]
        public void WhenHasIncompatibleVersions_TryCreate_ThenDataFromSaveShouldBeFromGreaterVersionSource()
        {
            // arrange
            const string dataA = "blahblahblah";
            var fakeDataA = new FakeDataSource(3, dataA);
            const string dataB = "BLAHBLAHBLAH";
            var fakeDataB = new FakeDataSource(7, dataB);
            
            // act
            var sources = new ISourceAdapter[] { fakeDataA, fakeDataB };
            var save = Create.CreateSave(sources);
            
            // assert
            Assert.That(save.GetValue() == dataB);
        }

        [Test]
        public void WhenHasIncompatibleVersions_TryCreate_ThenSourcesVersionsShouldBeEqual()
        {
            // arrange
            const string dataA = "blahblahblah";
            const int versionFromA = 3;
            var fakeDataA = new FakeDataSource(versionFromA, dataA);
            const string dataB = "BLAHBLAHBLAH";
            const int versionFromB = 7;
            var fakeDataB = new FakeDataSource(versionFromB, dataB);

            // act
            var sources = new ISourceAdapter[] { fakeDataA, fakeDataB };
            var save = Create.CreateSave(sources);

            // assert
            Assert.That(fakeDataA.Record.version == fakeDataB.Record.version);
        }
    }
}