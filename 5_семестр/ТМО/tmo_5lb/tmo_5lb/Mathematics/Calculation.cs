using System;

namespace tmo_5lb.Mathematics
{
	static class Calculation
	{
		public static double Factorial(int number)
		{
			if (number <= 1) return 1.0;
			var result = 1.0;
			for (var i = 1; i <= number; ++i) result *= i;
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

		public static double QueueExistingProbability(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			return (Math.Pow(p, numberOfChannels + 1) * P0) /
				(Factorial(numberOfChannels) * (numberOfChannels - p));
		}

		public static double AllChannelsBusyProbability(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			return (Math.Pow(p, numberOfChannels) * P0) / (Factorial(numberOfChannels - 1) * (numberOfChannels - p));
		}

		public static double AvgNumberOfRequests(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			var sumLeftPart = 0.0;
			for (var k = 0; k < numberOfChannels - 1; ++k) sumLeftPart += Math.Pow(p, k) / Factorial(k);
			sumLeftPart *= p;
			var sumRightPart = (Math.Pow(p, numberOfChannels + 1) * (numberOfChannels + 1 - p)) / 
				(Factorial(numberOfChannels - 1) * Math.Pow(numberOfChannels - p, 2));
			return P0 * (sumLeftPart + sumRightPart);
		}

		public static double AvgQueueLength(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			return (Math.Pow(p, numberOfChannels + 1) * P0) / 
				(Factorial(numberOfChannels - 1) * Math.Pow(numberOfChannels - p, 2));
		}

		public static double AvgNumberOfFreeNodes(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			var sum = 0.0;
			for (var k = 1; k < numberOfChannels; ++k) sum += (k * Math.Pow(p, k)) / Factorial(numberOfChannels - k);
			return P0 * sum;
		}

		public static double AvgNumberOfBusyNodes(double requestParameter, double responseParameter, int numberOfChannels)
		{
			return numberOfChannels - AvgNumberOfFreeNodes(requestParameter, responseParameter, numberOfChannels);
		}

		public static double AvgWaitingTime(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			return (Math.Pow(p, numberOfChannels) * P0) /
				(responseParameter * Factorial(numberOfChannels - 1) * Math.Pow(numberOfChannels - p, 2));
		}

		public static double GeneralTimeOfRequestsInSystem(double requestParameter, double responseParameter, int numberOfChannels)
		{
			var p = Calculation.p(requestParameter, responseParameter);
			var P0 = Calculation.P0(requestParameter, responseParameter, numberOfChannels);
			return (Math.Pow(p, numberOfChannels + 1) * P0) /
				Factorial(numberOfChannels - 1) * Math.Pow(numberOfChannels - p, 2);
		}

		public static double AvgTimeOfRequestInSystem(double requestParameter, double responseParameter, int numberOfChannels)
		{
			return AvgWaitingTime(requestParameter, responseParameter, numberOfChannels) + (1.0 / responseParameter);
		}

		public static double SumAvgTimeOfRequestInSystemPerTime(double requestParameter, double responseParameter, int numberOfChannels)
		{
			return GeneralTimeOfRequestsInSystem(requestParameter, responseParameter, numberOfChannels) +
				p(requestParameter, responseParameter);
		}
	}
}
