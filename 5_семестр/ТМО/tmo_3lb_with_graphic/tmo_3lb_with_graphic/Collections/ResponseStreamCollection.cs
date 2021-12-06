using tmo_3lb_with_graphic.Streams;

namespace tmo_3lb_with_graphic.Collections
{
	class ResponseStreamCollection
	{
        private readonly int size;
        private readonly PoissonDemandStream[] streams;
        private readonly bool[] isBusy;
        private readonly double[] releaseTimes;

        public ResponseStreamCollection(int size = 0)
        {
            this.size = size;

            streams = new PoissonDemandStream[size];
            isBusy = new bool[size];
            releaseTimes = new double[size];
        }

        public void InitStreams(double streamParameter)
        {
            for (int i = 0; i < size; ++i)
            {
                streams[i] = new PoissonDemandStream(streamParameter);
            }
        }

        public bool SetRequest(double time)
        {
            FreeChannels(time);

            for (int i = 0; i < size; ++i)
            {
                if (!isBusy[i])
                {
                    releaseTimes[i] = time + streams[i].GetNextWaitingTime();
                    isBusy[i] = true;
                    Loggers.ConsoleLogger.SetRequest(i, releaseTimes[i]);
                    return true;
                }
            }

            return false;
        }

        private void FreeChannels(double time)
        {
            for (int i = 0; i < size; ++i)
            {
                if (isBusy[i] && releaseTimes[i] <= time)
                {
                    Loggers.ConsoleLogger.FreeChannel(i, releaseTimes[i]);
                    //Console.WriteLine((i + 1) + "-channel is free(" + releaseTimes[i] + ")");
                    isBusy[i] = false;
                    releaseTimes[i] = 0.0;
                }
            }
        }
    }
}
