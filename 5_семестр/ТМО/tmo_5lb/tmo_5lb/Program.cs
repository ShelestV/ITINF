using System;
using System.Windows.Forms;
using System.Collections.Generic;
using tmo_5lb.Mathematics;
using System.Linq;
using tmo_5lb.Loggers;
using System.Runtime.InteropServices;

namespace tmo_5lb
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private static Form1 form;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();
            ConsoleLogger.Init();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            form = new Form1();
            form.Points =  Do().Select(p => p.ToGraphicPoint()).ToArray();

            Application.Run(form); 
        }

        private static MathGraphicPoint[] Do()
		{
            var group = 1.0;
            var variant = 30.0;
            var numberOfChannels = 4;

            var requestStreamParameter = (15.0 * group) / (variant * numberOfChannels);
            //var requestStream = new PoissonDemandStream(requestStreamParameter, Streams.Enums.StreamType.Request);

            var responseStreamParameter = (3.0 * (group + numberOfChannels)) / (variant * numberOfChannels);
            //var channels = new ResponseStreamCollection(numberOfChannels);
            //channels.InitStreams(responseStreamParameter);

            /*for (var time = 0.0; time < 200;)
            {
                var requestTime = requestStream.GetNextWaitingTime();
                channels.SetRequest(time);
                time += requestTime;
            }*/

            var graphicPoints = new List<MathGraphicPoint>();

            var p = Calculation.p(requestStreamParameter, responseStreamParameter);
            var P0 = Calculation.P0(requestStreamParameter, responseStreamParameter, numberOfChannels);

            var Pk = 0.0;
            var k = 0;
            for (; k < numberOfChannels; ++k)
            {
                Pk = (Math.Pow(p, k) / Calculation.Factorial(k)) * P0;
                graphicPoints.Add(new MathGraphicPoint(k, Pk));
            }

            bool isLessThenEpsilon = false;
            do
            {
                var previousPk = Pk;
                
                Pk = (Math.Pow(p, k) * P0) / (Math.Pow(numberOfChannels, k - numberOfChannels) * Calculation.Factorial(numberOfChannels));
                graphicPoints.Add(new MathGraphicPoint(k, Pk));
                if (Math.Abs(previousPk - Pk) < 0.00001)
                {
                    isLessThenEpsilon = true;
                    MathGraphicPoint.MaxK = k;
                }
                
                ++k;
            } while (!isLessThenEpsilon);

            #region Console output
            ConsoleLogger.QueueExistingProbability(Calculation.QueueExistingProbability(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AllChannelsBusyProbability(Calculation.AllChannelsBusyProbability(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgNumberOfRequests(Calculation.AvgNumberOfRequests(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgQueueLength(Calculation.AvgQueueLength(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgNumberOfFreeNodes(Calculation.AvgNumberOfFreeNodes(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgNumberOfBusyNodes(Calculation.AvgNumberOfBusyNodes(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgWaitingTimeToServe(Calculation.AvgWaitingTime(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.GeneralWaitingTimeForRequestInQueue(Calculation.GeneralTimeOfRequestsInSystem(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.AvgTimeOfRequestInSystem(Calculation.AvgTimeOfRequestInSystem(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.SumTimeOfAllRequestsInSystem(Calculation.SumAvgTimeOfRequestInSystemPerTime(requestStreamParameter, responseStreamParameter, numberOfChannels));
            ConsoleLogger.EmptyLine();
            foreach (var point in graphicPoints)
                ConsoleLogger.Pk(point.K, point.P);
			#endregion

			return graphicPoints.ToArray();
        }
    }
}