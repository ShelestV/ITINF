﻿using System;
using tmo_3lb_with_graphic.Generators.Abstract;

namespace tmo_3lb_with_graphic.Generators
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
