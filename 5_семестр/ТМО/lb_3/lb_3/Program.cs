using System;
using lb_3.Collections;
using lb_3.DemandStreams;
using lb_3.Mathematics;

namespace lb_3
{
    static class Program
    {
        private const int GROUP = 1;
        private const int VARIANT = 9;
        private const int NUMBER_OF_CHANNELS = 4;
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            const double requestStreamParameter = 10.0 * GROUP / (VARIANT * NUMBER_OF_CHANNELS);
            const double responseStreamParameter = 3.0 * (GROUP + VARIANT) / (VARIANT * NUMBER_OF_CHANNELS);

            var requestStream = new PoissonDemandStream(requestStreamParameter);

            var responseStreams = new ResponseStreamCollection(NUMBER_OF_CHANNELS);
            responseStreams.Init(responseStreamParameter);


            for (var time = 0.0; time < 100.0;)
            {
                var requestTime = requestStream.GetNextWaitingTime();
                time += requestTime;
                responseStreams.BusyChannels(time);
            }

            double load = requestStreamParameter / responseStreamParameter;
            Console.WriteLine();
            Console.WriteLine("Failure probability: " + Calculation.FailureProbability(load, NUMBER_OF_CHANNELS));
            Console.WriteLine("Average number of busy channels: " + Calculation.AverageNumberOfBusyChannels(load, NUMBER_OF_CHANNELS));
            Console.WriteLine("Average number of free channels: " + Calculation.AverageNumberOfFreeChannels(load, NUMBER_OF_CHANNELS));
            Console.WriteLine("Relative bandwidth: " + Calculation.RelativeBandwidth(load, NUMBER_OF_CHANNELS));
            Console.WriteLine("Absolute bandwidth: " + Calculation.AbsoluteBandwidth(requestStreamParameter, load, NUMBER_OF_CHANNELS));
            Console.WriteLine("Busy coefficient: " + Calculation.BusyCoefficient(load, NUMBER_OF_CHANNELS));
        }
    }
}