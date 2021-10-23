using System;
using lb_3.Generators;
using lb_3.Generators.Abstract;

namespace lb_3.DemandStreams
{
    internal class PoissonDemandStream
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