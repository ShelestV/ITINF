using System;

namespace lb_3.Mathematics
{
    internal static class Calculation
    {
        public static double Factorial(int number)
        {
            if (number <= 1) return 1.0;
            
            double result = 1.0;
            for (int i = 1; i <= number; ++i)
            {
                result *= i;
            }

            return result;
        }

        public static double p(double requestParameter, double responseParameter)
        {
            return requestParameter / responseParameter;
        }

        public static double P0(double requestParameter, double responseParameter, int numberOfChannels)
        {
            var p = Calculation.p(requestParameter, responseParameter);
            var denominatorPart = 0.0;
            for (var k = 0; k < numberOfChannels; ++k) denominatorPart += Math.Pow(p, k) / Factorial(k);
            return 1.0 / (denominatorPart + (Math.Pow(p, numberOfChannels + 1) / (Factorial(numberOfChannels) * (numberOfChannels - p))));
        }

        public static double Pk(double requestParameter, double responseParameter, int numberOfChannels, int k)
		{
            var p = Calculation.p(requestParameter, responseParameter);
            var numerator = Math.Pow(p, k) / Factorial(k);
            var denominator = 0.0;
            for (var i = 0; i < numberOfChannels; ++i)
                denominator += Math.Pow(p, i) / Factorial(i);
            return numerator / denominator;
		}

        public static double FailureProbability(double requestParameter, double responseParameter, int channelsAmount)
        {
            var p = Calculation.p(requestParameter, responseParameter);
            double numerator = Math.Pow(p, channelsAmount) / Factorial(channelsAmount);
            double denominator = 0.0;
            for (int i = 0; i <= channelsAmount; ++i)
                denominator += Math.Pow(p, i) / Factorial(i);

            return numerator / denominator;
        }

        public static double AverageNumberOfBusyChannels(double requestParameter, double responseParameter, int channelsAmount)
        {
            return Calculation.p(requestParameter, responseParameter) * (1.0 - FailureProbability(requestParameter, responseParameter, channelsAmount));
        }

        public static double AverageNumberOfFreeChannels(double requestParameter, double responseParameter, int channelsAmount)
        {
            return channelsAmount - AverageNumberOfBusyChannels(requestParameter, responseParameter, channelsAmount);
        }

        public static double RelativeBandwidth(double requestParameter, double responseParameter, int channelsAmount)
        {
            return 1.0 - FailureProbability(requestParameter, responseParameter, channelsAmount);
        }

        public static double AbsoluteBandwidth(double requestParameter, double responseParameter, int channelsAmount)
        {
            return requestParameter * RelativeBandwidth(requestParameter, responseParameter, channelsAmount);
        }

        public static double BusyCoefficient(double requestParameter, double responseParameter, int channelsAmount)
        {
            return AverageNumberOfBusyChannels(requestParameter, responseParameter, channelsAmount) / channelsAmount;
        }
    }
}