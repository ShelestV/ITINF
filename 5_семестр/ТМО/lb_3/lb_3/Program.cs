using System;
using lb_3.Collections;
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

            var responseStreams = new ResponseStreamCollection(NUMBER_OF_CHANNELS);
            responseStreams.Init(responseStreamParameter);


            for (var time = 0.0; time < 100.0;)
            {
                var requestTime = requestStream.GetNextWaitingTime();
                time += requestTime;
                var requestResult = responseStreams.TryBusyChannels(time);
            }
        }
    }
}