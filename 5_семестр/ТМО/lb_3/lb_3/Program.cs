using System;
using lb_3.Collections;
using lb_3.DemandStreams;

namespace lb_3
{
    internal class Program
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
            responseStreams.InitStreams(responseStreamParameter);
            
            for (double time = 0; time < 100.0;)
            {
                Console.WriteLine("\nTime : " + time + "\n");
                var isSetRequest = responseStreams.SetRequest(time);

                Console.WriteLine(time + " : " + isSetRequest);
                
                time += requestStream.GetNextWaitingTime();
            }
        }
    }
}