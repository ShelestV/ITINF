using System;
using lb4.Collections;
using lb4.Streams;
using lb4.Streams.Enums;

namespace lb4
{
    class Program
    {
        static void Main(string[] args)
        {
            const double @group = 1.0;
            const double journalNumber = 30.0;
            const int numberOfChannels = 5;
            const double avgServeTimeInSec = 80.0;
            
            const double requestStreamParameter = (10.0 * group) / (journalNumber * numberOfChannels);
            var requestStream = new PoissonDemandStream(requestStreamParameter, StreamType.Request);
            
            const double responseStreamParameter = 1.0 / avgServeTimeInSec;
            var responseList = new ResponseStreamCollection(numberOfChannels);
            responseList.InitStreams(responseStreamParameter);

            for (var time = 0.0; time < 100.0;)
            {
                responseList.SetRequest(time);
                time += requestStream.GetNextWaitingTime();
            }
        }
    }
}