using lb_3.DemandStreams;

namespace lb_3
{
    class Program
    {
        private const int GROUP = 1;
        private const int VARIANT = 30;
        private const int NUMBER_OF_CHANNELS = 5;
        static void Main(string[] args)
        {
            const double requestStreamParameter = 10.0 * GROUP / (VARIANT * NUMBER_OF_CHANNELS);
            const double responseStreamParameter = 3.0 * (GROUP + VARIANT) / (VARIANT * NUMBER_OF_CHANNELS);

            var requestStream = new PoissonDemandStream(requestStreamParameter);
            
            var responseStreams = new PoissonDemandStream[NUMBER_OF_CHANNELS];
            for (int i = 0; i < NUMBER_OF_CHANNELS; ++i)
            {
                responseStreams[i] = new PoissonDemandStream(responseStreamParameter);
            }

            
            for (int i = 0; i < 100; ++i)
            {
                var requestTime = requestStream.GetNextWaitingTime();
                
                
            }
        }
    }
}