using System;

namespace tmo_2lb
{
	class RandomGenerator : IRandomGenerator
	{
		private int maxRandomValue = 100;
		private Random random = new Random();

		public double NextFrom0To1()
		{
			int numerator = random.Next(1, maxRandomValue - 1);
			int denominator = random.Next(numerator, maxRandomValue);

			return (double)numerator / denominator;
		}
	}
}
