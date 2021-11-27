using lb4.Loggers;
using lb4.Streams;
using lb4.Streams.Enums;

namespace lb4.Collections
{
    public class ResponseStreamCollection
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
            for (var i = 0; i < size; ++i)
                streams[i] = new PoissonDemandStream(streamParameter, StreamType.Response);
        }
        
        public bool SetRequest(double time)
        {
            FreeChannels(time);

            for (var i = 0; i < size; ++i)
            {
                if (isBusy[i]) continue;
                
                releaseTimes[i] = time + streams[i].GetNextWaitingTime();
                isBusy[i] = true;
                ConsoleLogger.SetRequest(i, releaseTimes[i]);
                
                return true;
            }

            return false;
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