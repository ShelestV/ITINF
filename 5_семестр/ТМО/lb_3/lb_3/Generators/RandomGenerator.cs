using System;
using lb_3.Generators.Abstract;

namespace lb_3.Generators
{
    internal class RandomGenerator : IRandomGenerator
    {
        private const int MaxRandomValue = 100;
        private readonly Random random = new();

        public double NextFrom0To1()
        {
            int numerator = random.Next(1, MaxRandomValue - 1);
            double denominator = random.Next(numerator, MaxRandomValue);

            return numerator / denominator;
        }
    }
}