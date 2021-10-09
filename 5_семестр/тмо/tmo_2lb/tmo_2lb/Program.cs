using System;
using System.Collections.Generic;

namespace tmo_2lb
{
	class Program
	{
		static void Main(string[] args)
		{
			int group = 3;
			int variant = 18;

			int startTime = variant + 1;
			int endTime = variant + 5;

			int time = endTime - startTime;

			double streamParameter1 = 9 * group / variant;
			double streamParameter2 = 13 * group / variant;

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

			stream1.CalculateIntervals(14);
			stream2.CalculateIntervals(14);

			var streamSum = new PoissonRequirementsStream(
				ListMerger.MergeIntLists(stream1.Intervals, stream2.Intervals), 18, startTime, endTime);

			#region Table
			Console.Write("Number\t");
			for (int i = 1; i <= 14; ++i)
				Console.Write(i + "\t");
			Console.WriteLine();

			Console.Write("x1(t)\t");
			foreach (var interval in stream1.Intervals)
				Console.Write(interval + "\t");
			Console.WriteLine();

			Console.Write("x2(t)\t");
			foreach (var interval in stream2.Intervals)
				Console.Write(interval + "\t");
			Console.WriteLine();

			Console.Write("x(sum)\t");
			foreach (var interval in streamSum.Intervals)
				Console.Write(interval + "\t");
			Console.WriteLine();
			#endregion

			var barChart1 = new ConsoleBarChart(stream1.Intervals);
			var barChart2 = new ConsoleBarChart(stream2.Intervals);
			var barCharSum = new ConsoleBarChart(streamSum.Intervals);
			
			barChart1.Output();
			barChart2.Output();
			barCharSum.Output();
			Console.WriteLine();

			var modulSumPosibility = new StreamPosibility(streamSum.ModulStreamParameter, time);
			var sumPosibility = new StreamPosibility(streamParameter1 + streamParameter2, time);


			Console.WriteLine("Modal sum stream parameter: " + streamSum.ModulStreamParameter);
			Console.WriteLine("Modal sum stream posibilities: ");
			Console.WriteLine("P[0] = " + modulSumPosibility.Calculate(0));
			Console.WriteLine("P[1] = " + modulSumPosibility.Calculate(1));
			Console.WriteLine("P[4] = " + modulSumPosibility.Calculate(4));
			Console.Write("P[>=5] = ");
			Console.WriteLine(1.0 - modulSumPosibility.Calculate(4)
				- modulSumPosibility.Calculate(3) - modulSumPosibility.Calculate(2)
				- modulSumPosibility.Calculate(1) - modulSumPosibility.Calculate(0));
			Console.Write("P[<3] = ");
			Console.WriteLine(modulSumPosibility.Calculate(0)
				+ modulSumPosibility.Calculate(1) + modulSumPosibility.Calculate(2));
			Console.Write("P[<=7] = ");
			Console.WriteLine(modulSumPosibility.Calculate(0) + modulSumPosibility.Calculate(1)
				+ modulSumPosibility.Calculate(2) + modulSumPosibility.Calculate(3)
				+ modulSumPosibility.Calculate(4) + modulSumPosibility.Calculate(5)
				+ modulSumPosibility.Calculate(6) + modulSumPosibility.Calculate(7));
			Console.WriteLine("P[0.1 < Zk < 0.5] = " + (1 - Math.Exp(-streamSum.ModulStreamParameter * 0.5)
				- (1 - Math.Exp(-streamSum.ModulStreamParameter * 0.1))));
			Console.WriteLine();

			Console.WriteLine("Sum stream parameter: " + (streamParameter1 + streamParameter2));
			Console.WriteLine("Sum stream posibilities: ");
			Console.WriteLine("P[0] = " + sumPosibility.Calculate(0));
			Console.WriteLine("P[1] = " + sumPosibility.Calculate(1));
			Console.WriteLine("P[4] = " + sumPosibility.Calculate(4));
			Console.Write("P[>=5] = ");
			Console.WriteLine(1.0 - sumPosibility.Calculate(4)
				- sumPosibility.Calculate(3) - sumPosibility.Calculate(2)
				- sumPosibility.Calculate(1) - sumPosibility.Calculate(0));
			Console.Write("P[<3] = ");
			Console.WriteLine(sumPosibility.Calculate(0)
				+ sumPosibility.Calculate(1) + sumPosibility.Calculate(2));
			Console.Write("P[<=7] = ");
			Console.WriteLine(sumPosibility.Calculate(0) + sumPosibility.Calculate(1)
				+ sumPosibility.Calculate(2) + sumPosibility.Calculate(3)
				+ sumPosibility.Calculate(4) + sumPosibility.Calculate(5)
				+ sumPosibility.Calculate(6) + sumPosibility.Calculate(7));
			Console.WriteLine("P[0.1 < Zk < 0.5] = " + (1 - Math.Exp(-(streamParameter1 + streamParameter2) * 0.5)
				- (1 - Math.Exp(-(streamParameter1 + streamParameter2) * 0.1))));
			Console.WriteLine();

			Console.WriteLine("Expected value: " + streamSum.GetExpectedValue());
			Console.WriteLine("Dispersion: " + streamSum.GetDispersion());
			Console.Read();
		}
	}
}
