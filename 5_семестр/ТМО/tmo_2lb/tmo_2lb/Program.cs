using System;
using System.Collections.Generic;

namespace tmo_2lb
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			int group = 2;
			int variant = 26;

			int startTime = variant + 1;
			int endTime = variant + 5;

			int time = endTime - startTime;

			double streamParameter1 = 9 * (double)group / variant;
			double streamParameter2 = 13 * (double)group / variant;

			IRandomGenerator generator = new RandomGenerator();

			var randomValues1 = new List<double>();
			var randomValues2 = new List<double>();

			for (int i = 0; i < 50; ++i)
			{
				randomValues1.Add(generator.NextFrom0To1());
				randomValues2.Add(generator.NextFrom0To1());
			}

			var stream1 = new PoissonRequirementsStream(streamParameter1, startTime, endTime);
			var stream2 = new PoissonRequirementsStream(streamParameter2, startTime, endTime);

			stream1.CalculateZ(randomValues1);
			stream2.CalculateZ(randomValues2);

			stream1.CalculateRequestReceipt();
			stream2.CalculateRequestReceipt();

			stream1.CalculateIntervals(25);
			stream2.CalculateIntervals(25);

			stream1.OutputData();
			Console.WriteLine();

			stream2.OutputData();
			Console.WriteLine();

			var streamSum = new PoissonRequirementsStream(
				ListMerger.MergeIntLists(stream1.Intervals, stream2.Intervals), 18, startTime, endTime);

			#region Table
			Console.Write("Number\t");
			for (int i = 1; i <= 25; ++i)
				Console.Write(i + "|");
			Console.WriteLine();

			int count = 1;
			Console.Write("x1(t)\t");
			foreach (var interval in stream1.Intervals)
			{
				Console.Write(((count > 9 && interval < 10) ? " " : "") + interval + "|");
				++count;
			}
			Console.WriteLine();

			count = 1;
			Console.Write("x2(t)\t");
			foreach (var interval in stream2.Intervals)
			{
				Console.Write(((count > 9 && interval < 10) ? " " : "") + interval + "|");
				++count;
			}
			Console.WriteLine();

			count = 1;
			Console.Write("x(sum)\t");
			foreach (var interval in streamSum.Intervals)
			{
				Console.Write(((count > 9 && interval < 10) ? " " : "") + interval + "|");
				++count;
			}
			Console.WriteLine();
			#endregion

			var barChart1 = new ConsoleBarChart(stream1.Intervals);
			var barChart2 = new ConsoleBarChart(stream2.Intervals);
			var barCharSum = new ConsoleBarChart(streamSum.Intervals);
			
			barChart1.Output();
			barChart2.Output();
			barCharSum.Output();
			Console.WriteLine();

			stream1.CalculateDistributionParameters();
			stream1.CalculateModelOfStreamParameter(variant);

			stream2.CalculateDistributionParameters();
			stream2.CalculateModelOfStreamParameter(variant);

			Console.WriteLine("Model sum stream parameter(calculated): " + streamSum.ModelStreamParameter);
			Console.WriteLine("Model sum stream parameter(first + second): " + (stream1.ModelStreamParameter + stream2.ModelStreamParameter));
			Console.WriteLine();

			Console.WriteLine("Expected value(first stream): " + stream1.GetExpectedValue());
			Console.WriteLine("Dispersion(first stream): " + stream1.GetDispersion());
			Console.WriteLine();
			Console.WriteLine("Expected value(second stream): " + stream2.GetExpectedValue());
			Console.WriteLine("Dispersion(second stream): " + stream2.GetDispersion());
			Console.WriteLine();
			Console.WriteLine("Expected value(sum stream): " + streamSum.GetExpectedValue());
			Console.WriteLine("Dispersion(sum stream): " + streamSum.GetDispersion());
			Console.Read();
		}
	}
}
