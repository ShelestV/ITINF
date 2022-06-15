using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factorial
{
    public static class FactorialAlgos
    {
        public static double CalculateWithLoop(int number)
        {
            if (!IsCorrectNumberToCalculate(number))
                return 0.0;

            var result = 1.0;
            for (var i = 2; i <= number; i++)
                result *= i;
            return Round(result);
        }

        public static async Task<double> CalculateWithTreeAsync(int number)
        {
            if (!IsCorrectNumberToCalculate(number))
                return 0.0;

            if (number == 0)
                return 1.0;

            if (number == 1 || number == 2)
                return number;

            return Round(await MultiplyTree(2, number));
        }

        public static double CalculateWithFactorization(int number)
        {
            if (!IsCorrectNumberToCalculate(number))
                return 0.0;

            if (number == 0)
                return 1.0;

            if (number == 1 || number == 2)
                return number;

            var markers = new bool[number + 1];
            var factorsAndExponentsDictionary = new Dictionary<int, int>();
            for (var current = 2; current < number; current++)
            {
                if (!markers[current]) // if number is simple
                {
                    var degree = GetDegree(current, number);
                    factorsAndExponentsDictionary.Add(current, degree);
                    SeetNumbers(markers, current, number);
                }
            }


            return Round(factorsAndExponentsDictionary
                            .Select(x => Math.Pow(x.Key, x.Value))
                            .Aggregate((x, y) => x * y));
        }

        private static bool IsCorrectNumberToCalculate(int number)
        {
            return 0 <= number;
        }

        private static async Task<double> MultiplyTree(int leftLimit, int rightLimit)
        {
            if (leftLimit > rightLimit)
                return 1.0;

            if (leftLimit == rightLimit)
                return leftLimit;

            if (rightLimit - leftLimit == 1)
                return leftLimit * rightLimit;

            int median = (leftLimit + rightLimit) / 2;

            var leftPartCalculatingTask = Task.Run(() => MultiplyTree(leftLimit, median));
            var rightPartCalculatingTask = Task.Run(() => MultiplyTree(median + 1, rightLimit));
            await Task.WhenAll(leftPartCalculatingTask, rightPartCalculatingTask);

            return leftPartCalculatingTask.Result * rightPartCalculatingTask.Result;
        }

        private static int GetDegree(int current, int max)
        {
            var k = max / current;
            int degree = 0;
            while (k > 0)
            {
                degree += k;
                k /= current;
            }
            return degree;
        }

        private static void SeetNumbers(bool[] markers, int current, int max)
        {
            for (var i = 2; current * i <= max; i++)
                markers[current * i] = true;
        }

        private static double Round(double number, int numberOfSymbolsAfterDot = 0)
        {
            var d = Math.Pow(10, numberOfSymbolsAfterDot);
            return Math.Round(number * d) / d;
        }
    }
}
