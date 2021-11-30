using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using tmo_5lb.Collections;
using tmo_5lb.Streams;
using System.Collections.Generic;
using tmo_5lb.Mathematics;

namespace tmo_5lb
{
    static class Program
    {
        private static readonly Form1 form = new Form1();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Task.Run(Do);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form);


        }

        private static async Task Do()
		{
            // Has not any asynchronus computings
            var group = 1.0;
            var variant = 30.0;
            var numberOfChannels = 4;

            var requestStreamParameter = (15.0 * group) / (variant * numberOfChannels);
            var requestStream = new PoissonDemandStream(requestStreamParameter, Streams.Enums.StreamType.Request);

            var responseStreamParameter = (3.0 * (group + numberOfChannels)) / (variant * numberOfChannels);
            var channels = new ResponseStreamCollection(numberOfChannels);
            channels.InitStreams(responseStreamParameter);

            var graphicPoints = new List<GraphicPoint>();

            var p = Calculation.p(requestStreamParameter, responseStreamParameter);
            var P0 = Calculation.P0(requestStreamParameter, responseStreamParameter, numberOfChannels);

            var Pk = 0.0;
            var k = 0;
            for (; k < numberOfChannels; ++k) Pk = (Math.Pow(p, k) / Calculation.Factorial(numberOfChannels)) * P0;

            bool isLessThenEpsilon = false;
            do
            {
                var previousPk = Pk;
                
                Pk = (Math.Pow(p, k) * P0) / (Math.Pow(numberOfChannels, k - numberOfChannels) * Calculation.Factorial(numberOfChannels));
                if (Math.Abs(previousPk - Pk) < 0.00001)
                    isLessThenEpsilon = true;
                
                ++k;
            } while (!isLessThenEpsilon);
        }
    }
}