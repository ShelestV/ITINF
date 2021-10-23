using lb_3.DemandStreams;

namespace lb_3.Collections
{
    internal class ResponseStreamCollection
    {
        private int size;
        private PoissonDemandStream[] streams;
        private bool[] isBusy;
        private double[] releaseTime;
        
        public ResponseStreamCollection(int size = 0)
        {
            this.size = size;
            streams = new PoissonDemandStream[size];
            isBusy = new bool[size];
            releaseTime = new double[size];
        }

        public void BusyChannels(double time)
        {
            FreeChannels(time);

            for (int i = 0; i < size; ++i)
            {
                if (!isBusy[i])
                {
                    releaseTime[i] = time + streams[i].GetNextWaitingTime();
                }
            }
        }

        private void FreeChannels(double time)
        {
            for (int i = 0; i < size; ++i)
            {
                if (releaseTime[i] <= time)
                {
                    isBusy[i] = false;
                    releaseTime[i] = 0.0;
                }
            }
        }
    }
}