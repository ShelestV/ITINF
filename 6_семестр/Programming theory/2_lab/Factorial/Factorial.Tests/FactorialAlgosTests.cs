using NUnit.Framework;
using System.Threading.Tasks;

namespace Factorial.Tests
{
    public class FactorialAlgosTests
    {
        private const int Number = 25;
        private const double ExpectedResult = 15511210043330985984000000.0;

        [Test]
        public void CalculateWithLoop_Success_Test()
        {
            var actual = FactorialAlgos.CalculateWithLoop(Number);
            Assert.AreEqual(ExpectedResult, actual);
        }

        [Test]
        public async Task CalculateWithTreeAsync_Success_Test()
        {
            var actual = await FactorialAlgos.CalculateWithTreeAsync(Number);
            Assert.AreEqual(ExpectedResult, actual);
        }

        [Test]
        public void CalculateWithFactorization_Success_Test()
        {
            var actual = FactorialAlgos.CalculateWithFactorization(Number);
            Assert.AreEqual(ExpectedResult, actual);
        }
    }
}