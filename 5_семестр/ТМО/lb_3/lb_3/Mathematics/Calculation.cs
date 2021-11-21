using System;
using System.Net.NetworkInformation;

namespace lb_3.Mathematics
{
    internal static class Calculation
    {
        private static double Factorial(int number)
        {
            if (number <= 1) return 1.0;
            
            double result = 1.0;
            for (int i = 1; i <= number; ++i)
            {
                result *= i;
            }

            return result;
        }

        public static double FailureProbability(double load, int channelsAmount)
        {
            double numerator = Math.Pow(load, channelsAmount) / Factorial(channelsAmount);
            double denominator = 0.0;
            for (int i = 0; i <= channelsAmount; ++i)
            {
                denominator += Math.Pow(load, i) / Factorial(i);
            }

            return numerator / denominator;
        }

        public static double AverageNumberOfBusyChannels(double load, int channelsAmount)
        {
            return load * (1.0 - FailureProbability(load, channelsAmount));
        }

        public static double AverageNumberOfFreeChannels(double load, int channelsAmount)
        {
            return channelsAmount - AverageNumberOfBusyChannels(load, channelsAmount);
        }

        public static double RelativeBandwidth(double load, int channelsAmount)
        {
            return 1.0 - FailureProbability(load, channelsAmount);
        }

        public static double AbsoluteBandwidth(double streamParameter, double load, int channelsAmount)
        {
            return streamParameter * RelativeBandwidth(load, channelsAmount);
        }

        public static double BusyCoefficient(double load, int channelsAmount)
        {
            return AverageNumberOfBusyChannels(load, channelsAmount) / channelsAmount;
        }
    }
}