using lb_3.DemandStreams;
using lb_3.Loggers;

namespace lb_3.Collections
{
    internal class ResponseStreamCollection
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

        public void Init(double streamParameter)
        {
            for (int i = 0; i < size; ++i)
            {
                streams[i] = new PoissonDemandStream(streamParameter);
            }
        }
        
        public void BusyChannels(double time)
        {
            FreeChannels(time);

            for (var i = 0; i < size; ++i)
            {
                if (isBusy[i]) continue;
                
                releaseTimes[i] = time + streams[i].GetNextWaitingTime();
                isBusy[i] = true;
                ConsoleLogger.SetRequest(i, releaseTimes[i]);
                return;
            }
            
            ConsoleLogger.FailRequest();
        }

        private void FreeChannels(double time)
        {
            for (var i = 0; i < size; ++i)
            {
                if (!isBusy[i] || !(releaseTimes[i] <= time)) continue;
                
                ConsoleLogger.FreeChannel(i, releaseTimes[i]);
                isBusy[i] = false;
                releaseTimes[i] = 0.0;
            }
        }
    }
}