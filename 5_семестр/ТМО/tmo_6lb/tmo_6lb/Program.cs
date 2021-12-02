using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using tmo_6lb.Collections;
using tmo_6lb.Streams;

namespace tmo_6lb
{
	static class Program
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool AllocConsole();

		private static readonly List<Form1> forms = new List<Form1>();
		private static readonly List<string> formNames = new List<string>();

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

			Do();

			foreach (var form in forms)
				new Thread(() => Application.Run(form)).Start();
		}

		private static void Do()
		{
			var group = 1.0;
			var variant = 30.0;

			var numberOfChannels = 7;
			var avgServeTime = 120.0;

			var startTime = variant + 1.0;
			var endTime = variant + 200.0;

			var requestStreamParameter = (12.0 * group) / (variant * numberOfChannels);
			var requestStream = new PoissonDemandStream(requestStreamParameter, Streams.Enums.StreamType.Request);

			var responseStreamParameter = 1.0 / avgServeTime;
			var channels = new ResponseStreamCollection(numberOfChannels);
			channels.InitStreams(responseStreamParameter, startTime);

			for (var time = startTime; time < endTime;)
			{
				var requestTime = requestStream.GetNextWaitingTime();
				channels.SetRequest(time);
				time += requestTime;
			}

			for (int i = 0; i < channels.Count; ++i)
			{
				var form = new Form1();
				var points = new List<GraphicPoint>();
				foreach (var period in channels[i].ActivityPeriods)
				{
					var maxK = channels[i].ActivityPeriods.Select(p => p.Number).Max();
					points.Add(new GraphicPoint(period.Number, period.StartTime - startTime, maxK));
					points.Add(new GraphicPoint(period.Number, period.EndTime - startTime, maxK));
				}
				form.Points = points.ToArray();
				form.Text = $"{i + 1} Stream";

				forms.Add(form);
			}

			var queueForm = new Form1();
			var queuePoints = new List<GraphicPoint>();
			foreach (var period in channels.QueuePeriods)
			{
				var maxK = channels.QueuePeriods.Select(p => p.Number).Max();
				queuePoints.Add(new GraphicPoint(period.Number, period.StartTime - startTime, maxK));
				queuePoints.Add(new GraphicPoint(period.Number, period.EndTime - startTime, maxK));
			}
			queueForm.Points = queuePoints.ToArray();
			queueForm.Text = "Queue";

			forms.Add(queueForm);
		}
	}
}
