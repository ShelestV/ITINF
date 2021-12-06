using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using tmo_3lb_with_graphic.Collections;
using tmo_3lb_with_graphic.Streams;

namespace tmo_3lb_with_graphic
{
	static class Program
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		private const int GROUP = 2;
		private const int VARIANT = 30;
		private const int NUMBER_OF_CHANNELS = 5;

		private static Form1 form;

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			AllocConsole();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			form = new Form1();
			form.Points = Do().Select(p => p.ToGraphicPoint()).ToArray();

			Application.Run(form);
		}

		private static MathGraphicPoint[] Do()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			var points = new List<MathGraphicPoint>();

			const double requestStreamParameter = 10.0 * GROUP / (VARIANT * NUMBER_OF_CHANNELS);
			const double responseStreamParameter = 3.0 * (GROUP + VARIANT) / (VARIANT * NUMBER_OF_CHANNELS);

			var requestStream = new PoissonDemandStream(requestStreamParameter);

			var responseStreams = new ResponseStreamCollection(NUMBER_OF_CHANNELS);
			responseStreams.InitStreams(responseStreamParameter);

			for (double time = 0; time < 100.0;)
			{
				//Console.WriteLine("\nTime : " + time + "\n");
				var isSetRequest = responseStreams.SetRequest(time);
				Console.WriteLine();
				//Console.WriteLine(time + " : " + (isSetRequest ? "request setted" : "request rejected"));

				time += requestStream.GetNextWaitingTime();
			}

			Console.WriteLine("\n\n\n");

			//Console.WriteLine("Failure probability = " + Mathematics.Calculation.FailureProbability(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			//Console.WriteLine("Avarage number of busy channels = " + Mathematics.Calculation.AverageNumberOfBusyChannels(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			//Console.WriteLine("Avarage number of free channels = " + Mathematics.Calculation.AverageNumberOfFreeChannels(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			//Console.WriteLine("Relative bandwidth = " + Mathematics.Calculation.RelativeBandwidth(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			//Console.WriteLine("Absolute bandwidth = " + Mathematics.Calculation.AbsoluteBandwidth(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));

			Console.WriteLine("Вероятность того, что канал занят = " + Mathematics.Calculation.FailureProbability(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			Console.WriteLine("Среднее количество занятых каналов = " + Mathematics.Calculation.AverageNumberOfBusyChannels(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			Console.WriteLine("Среднее количество свободных каналов = " + Mathematics.Calculation.AverageNumberOfFreeChannels(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			Console.WriteLine("Относительная пропускная способность = " + Mathematics.Calculation.RelativeBandwidth(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));
			Console.WriteLine("Абсолютная пропускная способность = " + Mathematics.Calculation.AbsoluteBandwidth(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS));

			Console.WriteLine("\n\n\n");

			var p = Mathematics.Calculation.p(requestStreamParameter, responseStreamParameter);
			var P0 = Mathematics.Calculation.P0(requestStreamParameter, responseStreamParameter, NUMBER_OF_CHANNELS);
			for (int k = 0; k < NUMBER_OF_CHANNELS; ++k)
			{
				var Pk = (Math.Pow(p, k) / Mathematics.Calculation.Factorial(k)) * P0;
				Console.WriteLine($"{k + 1}: Pk = {Pk}");
			}

			MathGraphicPoint.MaxK = NUMBER_OF_CHANNELS - 1;
			for (var k = 0; k < NUMBER_OF_CHANNELS; ++k)
			{
				var Pk = Math.Pow(Mathematics.Calculation.p(requestStreamParameter, responseStreamParameter), k) / Mathematics.Calculation.Factorial(k);
				var denominator = 0.0;
				for (var i = 0; i < NUMBER_OF_CHANNELS; ++i)
					denominator += Math.Pow(Mathematics.Calculation.p(requestStreamParameter, responseStreamParameter), i) / Mathematics.Calculation.Factorial(i);

				points.Add(new MathGraphicPoint(k, Pk / denominator));
			}

			return points.ToArray();
		}
	}
}
