using NUnit.Framework;
using WormsApplication.services.generator.name;

namespace WormsApplication.UnitTests
{
    public class UniquenessNamesTest
    {
        [Test]
        public void GenerateTest_NewNames_AllUniqueness()
        {
            var namesGenerator = new NamesGenerator();
            for (var i = 0; i < 1000; i++) namesGenerator.Generate();
            var countOfUsingList = namesGenerator.GetCountOfUsing();
            foreach (var count in countOfUsingList)
                Assert.AreEqual(count <= 1, true);
        }
    }
}