using System;
using lb4.Generators;
using lb4.Generators.Abstract;
using lb4.Streams.Enums;

namespace lb4.Streams
{
    public class PoissonDemandStream
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