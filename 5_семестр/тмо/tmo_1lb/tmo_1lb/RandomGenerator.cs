using System;

namespace tmo_1lb
{
	class RandomGenerator : IRandomGenerator
	{
		private int maxRandomValue = 100;
		private Random random = new Random();

		public double NextFrom0To1()
		{
			int numerator;
			int denominator;
			do
			{
				numerator = random.Next(1, maxRandomValue);
				denominator = random.Next(1, maxRandomValue);
			} while (!IsQuotientFrom0To1(numerator, denominator));

			return (double)numerator / denominator;
			#region Useless comment
			// Often when I divide two numbers of integer, result is casted to int
			// So to get correct result, I cast numerator and denominator to double
			#endregion
		}

		private bool IsQuotientFrom0To1(int numerator, 
										int denominator)
		{
			return denominator != 0 && numerator != 0 && numerator <= denominator;
		}
	}
}
