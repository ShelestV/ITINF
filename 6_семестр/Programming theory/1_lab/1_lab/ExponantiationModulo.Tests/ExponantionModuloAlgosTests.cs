using NUnit.Framework;
using System.Threading.Tasks;

namespace ExponantiationModulo.Tests
{
    public class ExponantionModuloAlgosTests
    {
        private const int Root = 249;
        private const int Degree = 321;
        private const int Modulo = 499;

        private const double ExpectedResult = 447;

        [Test]
        public void CalculateWithMemoryEffiecient_Success_Test()
        {
            var actual = ExponentiationModulo.ExponantionModuloAlgos.CalculateWithMemoryEffiecient(Root, Degree, Modulo);
            Assert.AreEqual(ExpectedResult, actual);
        }

        [Test]
        public async Task CaclucateWithRepeatingSquareAndMultiplyingAsync_Success_Test()
        {
            var actual = await ExponentiationModulo.ExponantionModuloAlgos.CaclucateWithRepeatingSquareAndMultiplyingAsync(Root, Degree, Modulo);
            Assert.AreEqual(ExpectedResult, actual);
        }
    }
}