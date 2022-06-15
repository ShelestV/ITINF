using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExponentiationModulo
{
    public static class ExponantionModuloAlgos
    {
        public static double CalculateWithMemoryEffiecient(double root, int degree, int modulo)
        {
            var result = 1.0;
            for (var iterationIndex = 1; iterationIndex <= degree; iterationIndex++)
                result = (result * root) % modulo;
            return result;
        }

        public static async Task<double> CaclucateWithRepeatingSquareAndMultiplyingAsync(int root, int degree, int modulo)
        {
            var degreesOfTwo = ToProductOfPowersOfTwo(degree);
            var simplifyTasks = new Task<int>[degreesOfTwo.Count];
            //for (var degreeIndex = 0; degreeIndex < degreesOfTwo.Count; degreeIndex++)
            //{
            //    simplifyTasks[degreeIndex] = Task.Run(() => 
            //        SimplifyPowerOfNumberByModulo(root, degreesOfTwo[degreeIndex], modulo));
            //}

            //await Task.WhenAll(simplifyTasks);

            //var simplified = new int[degreesOfTwo.Count];
            //for (var i = 0; i < degreesOfTwo.Count; i++)
            //    simplified[i] = simplifyTasks[i].Result;

            var simplified = new int[degreesOfTwo.Count];
            for (var degreeIndex = 0; degreeIndex < degreesOfTwo.Count; degreeIndex++)
            {
                simplified[degreeIndex] = SimplifyPowerOfNumberByModulo(root, degreesOfTwo[degreeIndex], modulo);
        }

            return simplified.Aggregate((x, y) => x * y) % modulo;
        }

        private static IList<int> ToProductOfPowersOfTwo(int number, IList<int> productsOfPowers = null)
        {
            productsOfPowers ??= new List<int>(); 

            var degree = 0;
            while (Math.Pow(2, degree) < number)
            {
                ++degree;
            }

            if (Math.Pow(2, degree) == number)
            {
                productsOfPowers.Add(degree);
                return productsOfPowers;
            }

            var resultDegree = degree - 1;

            productsOfPowers.Add(resultDegree);
            return ToProductOfPowersOfTwo(number - (int)Math.Pow(2, resultDegree), productsOfPowers);
        }

        private static int SimplifyPowerOfNumberByModulo(int root, int numberOfPowersOfTwo, int modulo)
        {
            for (var i = 0; i < numberOfPowersOfTwo; i++)
                root = (int)Math.Pow(root, 2) % modulo;
            return root;
        }
    }
}
