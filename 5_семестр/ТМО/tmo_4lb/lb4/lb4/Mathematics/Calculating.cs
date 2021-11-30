using System;

namespace lb4.Mathematics
{
    public static class Calculating
    {
        private static double Factorial(int number)
        {
            if (number < 0) throw new ArgumentException();
            
            if (number is 0 or 1) return 1.0;
            
            var result = 1.0;
            for (var i = 2.0; i <= number; i++)
                result *= i;

            return result;
        }
        
        public static double RejectProbability(double requestStreamParameter, double avgServeTime, int numberOfChannels)
        {
            var p = requestStreamParameter * avgServeTime;
            var denominator = 0.0;
            for (var k = 0; k <= numberOfChannels; ++k)
            {
                var pow = Math.Pow(p, k);
                var factorial = Factorial(k);
                denominator += pow / factorial;
            }

            return (Math.Pow(p, numberOfChannels) / Factorial(numberOfChannels)) / denominator;
        }
    }
}