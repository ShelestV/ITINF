using System;
using tmo_6lb.RandomGenerators.Abstract;

namespace tmo_6lb.RandomGenerators
{
    class RandomGenerator : IRandomGenerator
    {
        private const int MaxRandomValue = 100;
        private readonly Random random = new();

        public double NextFrom0To1()
        {
            var numerator = random.Next(1, MaxRandomValue - 1);
            double denominator = random.Next(numerator, MaxRandomValue);

            return numerator / denominator;
        }
    }
}
