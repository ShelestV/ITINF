using System;
using tmo_3lb_with_graphic.Generators;
using tmo_3lb_with_graphic.Generators.Abstract;

namespace tmo_3lb_with_graphic.Streams
{
	class PoissonDemandStream
	{
        private readonly double streamParameter;
        private readonly IRandomGenerator randomGenerator;

        public PoissonDemandStream(double streamParameter)
        {
            this.streamParameter = streamParameter;
            randomGenerator = new RandomGenerator();
        }

        public double GetNextWaitingTime()
        {
            var randomValue = randomGenerator.NextFrom0To1();
            return -(Math.Log(randomValue) / streamParameter);
        }
    }
}
