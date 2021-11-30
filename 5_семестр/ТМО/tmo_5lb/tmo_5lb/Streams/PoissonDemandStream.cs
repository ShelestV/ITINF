using System;
using tmo_5lb.Generators;
using tmo_5lb.Generators.Abstract;
using tmo_5lb.Streams.Enums;

namespace tmo_5lb.Streams
{
	class PoissonDemandStream
	{
        private readonly double streamParameter;
        private readonly IRandomGenerator randomGenerator;
        private readonly StreamType type;
        public PoissonDemandStream(double streamParameter, StreamType type)
        {
            this.streamParameter = streamParameter;
            randomGenerator = new RandomGenerator();
            this.type = type;
        }

        public double GetNextWaitingTime()
        {
            var randomValue = randomGenerator.NextFrom0To1();
            return -(Math.Log(randomValue) / streamParameter);
        }
    }
}
