using System;
using System.Collections.Generic;

namespace tmo_1lb
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			int group = 1;
			int variant = 28;

			double streamParameter = group * 10.0 / variant;

			var stream = new PoissonRequirementsStream(streamParameter, variant + 1, variant + 5);

			IRandomGenerator generator = new RandomGenerator();
			var randomNumbers = new List<double>();
			for (var i = 0; i < 10; ++i)
				randomNumbers.Add(generator.NextFrom0To1());

			stream.CalculateStreamCharacteristics(randomNumbers, 25);

			Console.WriteLine("Random numbers: ");
			foreach (var number in randomNumbers)
				Console.WriteLine("\t" + number);
			Console.WriteLine();

			Console.WriteLine("Stream parameter: " + streamParameter);
			Console.WriteLine();

			stream.OutputStreamCharacteristics();
			Console.WriteLine();

			double time = stream.EndTime - stream.StartTime;

			double modelStreamParameter = stream.ModulStreamParameter;

			var taskPossibility = new StreamPosibility(streamParameter, time);
			var modelPossibility = new StreamPosibility(modelStreamParameter, time);

			Console.WriteLine("Stream parameter: " + streamParameter);
			Console.WriteLine("Stream possibilities: ");
			Console.WriteLine("P[0] = " + taskPossibility.Calculate(0));
			Console.WriteLine("P[1] = " + taskPossibility.Calculate(1));
			Console.WriteLine("P[4] = " + taskPossibility.Calculate(4));
			Console.Write("P[>=5] = ");
			Console.WriteLine(1.0 - taskPossibility.Calculate(4) 
				- taskPossibility.Calculate(3) - taskPossibility.Calculate(2)
				- taskPossibility.Calculate(1) - taskPossibility.Calculate(0));
			Console.Write("P[<3] = ");
			Console.WriteLine(taskPossibility.Calculate(0)
				+ taskPossibility.Calculate(1) + taskPossibility.Calculate(2));
			Console.Write("P[<=7] = ");
			Console.WriteLine(taskPossibility.Calculate(0) + taskPossibility.Calculate(1)
				+ taskPossibility.Calculate(2) + taskPossibility.Calculate(3)
				+ taskPossibility.Calculate(4) + taskPossibility.Calculate(5)
				+ taskPossibility.Calculate(6) + taskPossibility.Calculate(7));
			Console.WriteLine("P[0.1 < Zk < 0.5] = " + (1 - Math.Exp(-streamParameter * 0.5)
				- (1 - Math.Exp(-streamParameter * 0.1))));
			Console.WriteLine();

			Console.WriteLine("Model stream parameter: " + modelStreamParameter);
			Console.WriteLine("Model stream possibilities: ");
			Console.WriteLine("P[0] = " + modelPossibility.Calculate(0));
			Console.WriteLine("P[1] = " + modelPossibility.Calculate(1));
			Console.WriteLine("P[4] = " + modelPossibility.Calculate(4));
			Console.Write("P[>=5] = ");
			Console.WriteLine(1.0 - modelPossibility.Calculate(4)
				- modelPossibility.Calculate(3) - modelPossibility.Calculate(2)
				- modelPossibility.Calculate(1) - modelPossibility.Calculate(0));
			Console.Write("P[<3] = ");
			Console.WriteLine(modelPossibility.Calculate(0)
				+ modelPossibility.Calculate(1) + modelPossibility.Calculate(2));
			Console.Write("P[<=7] = ");
			Console.WriteLine(modelPossibility.Calculate(0) + modelPossibility.Calculate(1)
				+ modelPossibility.Calculate(2) + modelPossibility.Calculate(3)
				+ modelPossibility.Calculate(4) + modelPossibility.Calculate(5)
				+ modelPossibility.Calculate(6) + modelPossibility.Calculate(7));
			Console.WriteLine("P[0.1 < Zk < 0.5] = " + (1 - Math.Exp(-modelStreamParameter * 0.5)
				- (1 - Math.Exp(-modelStreamParameter * 0.1))));
			Console.WriteLine();

			Console.Read();
		}
	}
}
