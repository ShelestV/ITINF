using System;

namespace tmo_6lb.Mathematics
{
	class Calculation
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
			var result = (Math.Pow(p, numberOfChannels + 1) * P0) /
				(Factorial(numberOfChannels) * (numberOfChannels - p));
			return result > 1.0 ? 1.0 : result;
		}
	}
}
