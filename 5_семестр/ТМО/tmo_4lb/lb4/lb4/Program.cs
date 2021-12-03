using System;
using lb4.Collections;
using lb4.Loggers;
using lb4.Mathematics;
using lb4.Streams;
using lb4.Streams.Enums;

namespace lb4
{
    class Program
    {
        static void Main(string[] args)
        {
            const double @group = 1.0;
            const double journalNumber = 9.0;
            const int numberOfChannels = 6;
            const double avgServeTimeInSec = 75.0;

            const double startTime = journalNumber + 1.0;
            const double endTime = journalNumber + 200.0;

            const double requestStreamParameter = (10.0 * group) / (journalNumber * numberOfChannels);
            var requestStream = new PoissonDemandStream(requestStreamParameter, StreamType.Request);
            
            const double responseStreamParameter = 1.0 / avgServeTimeInSec;
            var responseList = new ResponseStreamCollection(numberOfChannels);
            responseList.InitStreams(responseStreamParameter);

            for (var time = startTime; time < endTime;)
            {
                var timeChannel = responseList.SetRequest(time);
                var requestTime = requestStream.GetNextWaitingTime();
                if (timeChannel != null)
                {
                    ConsoleLogger.Info(requestTime, timeChannel.time - time, time, timeChannel.channelIndex);
                }
                else
                {
                    ConsoleLogger.Reject();
                }
                time += requestTime;
            }
            
            Console.WriteLine();
			Console.WriteLine("Requests count = " + responseList.RequestsCount);
			Console.WriteLine("Rejects count = " + responseList.RejectsCount);
			//Console.WriteLine("Количество запросов = " + responseList.RequestsCount);
   //         Console.WriteLine("Количество отказов = " + responseList.RejectsCount);
            
            Console.WriteLine();
			Console.WriteLine("Model reject probability = " + (responseList.RejectsCount / (double)responseList.RequestsCount));
			Console.WriteLine("Erlange formula = " + Calculating.RejectProbability(requestStreamParameter, avgServeTimeInSec, numberOfChannels));
			//Console.WriteLine("Модельная вероятность отказа = " + (responseList.RejectsCount / (double)responseList.RequestsCount));
   //         Console.WriteLine("Вероятность отказа по первой формуле Эрланга = " + Calculating.RejectProbability(requestStreamParameter, avgServeTimeInSec, numberOfChannels));
        }
    }
}