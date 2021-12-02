using System;
using System.Collections.Generic;
using tmo_6lb.RandomGenerators;
using tmo_6lb.RandomGenerators.Abstract;
using tmo_6lb.Streams.Enums;
using tmo_6lb.Streams.Helpers;

namespace tmo_6lb.Streams
{
	class PoissonDemandStream
	{
        private readonly double streamParameter;
        private readonly IRandomGenerator randomGenerator;
        private readonly StreamType type;

        public ICollection<Period> ActivityPeriods { get; } = new List<Period>();
        public double LastTime { get; set; }

        public PoissonDemandStream(double streamParameter, StreamType type, double startTime = 0.0)
        {
            this.streamParameter = streamParameter;
            randomGenerator = new RandomGenerator();
            this.type = type;
            LastTime = startTime;
        }

        public double GetNextWaitingTime()
        {
            var randomValue = randomGenerator.NextFrom0To1();
            var waitingTime = -(Math.Log(randomValue) / streamParameter);

            return waitingTime;
        }
    }
}
