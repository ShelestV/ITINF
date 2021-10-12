using System;

namespace tmo_1lb
{
	class StreamPosibility
	{
		private double streamParameter;
		private double time;

		public StreamPosibility(double streamParameter, double time)
		{
			this.streamParameter = streamParameter;
			this.time = time;
		}

		private double GetFactorial(int value)
		{
			if (value < 0)
				throw new ArgumentException();

			double result = 1;
			for (int i = 2; i <= value; ++i)
				result *= i;

			return result;
		}

		public double Calculate(int degree)
		{
			double multiply = streamParameter * time;
			double numerator = Math.Exp(-multiply) * Math.Pow(multiply, degree);
			double denominator = GetFactorial(degree);
			return numerator / denominator;
		}
	}
}
