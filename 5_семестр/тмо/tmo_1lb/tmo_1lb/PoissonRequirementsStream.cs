using System;
using System.Collections.Generic;
using System.Linq;

namespace tmo_1lb
{
	class PoissonRequirementsStream
	{
		private double streamParameter;
		private List<double> Z = new List<double>();
		private List<double> requestReceipt = new List<double>();
		private List<double> intervals = new List<double>();
		private Dictionary<int, int> distributionParameters 
			= new Dictionary<int, int>();

		private double startTime;
		private double endTime;
		private double intervalTime;

		private double expectedValue;
		private double modulStreamParameter;

		public double StartTime { get => startTime; }
		public double EndTime { get => endTime; }
		public double ModulStreamParameter { get => modulStreamParameter; }

		public PoissonRequirementsStream(double streamParameter,
										 int startTime,
										 int endTime)
		{
			this.streamParameter = streamParameter;
			this.startTime = startTime;
			this.endTime = endTime;
		}

		// Variant as parameter is very strange
		// But the problem is also not quite mathematical
		public void CalculateStreamCharacteristics(IEnumerable<double> randomNumbers, 
												   int numberOfIntervals)
		{
			CalculateZ(randomNumbers);
			CalculateRequestReceipt();
			CalculateIntervals(numberOfIntervals);
			CalculateDistributionParameters();
			CalculateModulOfStreamParameter();
		}

		public void CalculateZ(IEnumerable<double> randomNumbers)
		{
			foreach (double number in randomNumbers)
				Z.Add(-(Math.Log(number) / streamParameter));
		}

		public void CalculateRequestReceipt()
		{
			double time = startTime;
			int index = 0;
			while (time + Z[index] < endTime)
			{
				time += Z[index];
				requestReceipt.Add(time);
				++index;
			} 
		}

		public void CalculateIntervals(int numberOfIntervals)
		{
			intervalTime = (endTime - startTime) / numberOfIntervals;
			int requestReceiptIndex = 0;
			for (int i = 0; i < numberOfIntervals; ++i)
			{
				int requestOnInterval = 0;

				double start = GetIntervalTimeFromStartTime(i, intervalTime);
				double end = GetIntervalTimeFromStartTime(i + 1, intervalTime);

				if (IsIndexInRequestReceiptRange(requestReceiptIndex) &&
					end >= requestReceipt[requestReceiptIndex])
				{
					while (IsIndexInRequestReceiptRange(requestReceiptIndex) &&
						IsOnInterval(start, end, requestReceipt[requestReceiptIndex]))
					{
						++requestReceiptIndex;
						++requestOnInterval;
					}
				}
				intervals.Add(requestOnInterval);
			}
		}

		private double GetIntervalTimeFromStartTime(int numberOfInterval,
													double intervalTime)
		{
			return startTime + numberOfInterval * intervalTime;
		}

		private bool IsIndexInRequestReceiptRange(int index)
		{
			return 0 <= index && index < requestReceipt.Count;
		}

		private bool IsOnInterval(double startIntervalTime, 
								  double endIntervalTime, 
								  double time)
		{
			return startIntervalTime <= time && time <= endIntervalTime;
		}

		public void CalculateDistributionParameters()
		{
			int requestNumberOnInterval = 0;
			
			for (int numberOfCheckIntervals = 0; numberOfCheckIntervals < intervals.Count;)
			{
				int numberOfIntervals = 0;
				foreach (var interval in intervals)
				{
					if (interval == requestNumberOnInterval)
					{
						++numberOfIntervals;
						++numberOfCheckIntervals;
					}
				}
				distributionParameters.Add(requestNumberOnInterval, numberOfIntervals);
				++requestNumberOnInterval;
			}
		}

		public void CalculateModulOfStreamParameter()
		{
			double preExpectedValue = distributionParameters.Select(par => par.Key * par.Value).Sum();
			expectedValue = preExpectedValue / (double)intervals.Count;
			modulStreamParameter = expectedValue / intervalTime;
		}

		public void OutputStreamCharacteristics()
		{
			Console.WriteLine("Start time: " + startTime);
			Console.WriteLine("End time: " + endTime);
			Console.WriteLine("Time difference: " + (endTime - startTime));
			Console.WriteLine();

			Console.WriteLine("Z:");
			foreach (double z in Z)
				Console.WriteLine("\t" + z);
			Console.WriteLine();

			Console.WriteLine("Requirements times: ");
			foreach (double time in requestReceipt)
				Console.WriteLine("\t" + time);
			Console.WriteLine();

			Console.WriteLine("Interval time: " + intervalTime);
			Console.WriteLine("Requirements on intervals");
			int i = 0;
			foreach (double interval in intervals)
				Console.WriteLine("\t" + ++i + ": " + interval);
			Console.WriteLine();

			Console.WriteLine("Amount of equal requirements on intervals: ");
			i = 0;
			foreach (var amount in distributionParameters)
				Console.WriteLine("\t" + amount.Key + ": " + amount.Value);

			var amountOfEqualRequirementsOnInterval = 
				distributionParameters.Select(key => key.Value).ToList();
			Console.WriteLine("Sum of amount: " + amountOfEqualRequirementsOnInterval.ToArray().Sum());
			Console.WriteLine();

			Console.WriteLine("Expected value: " + expectedValue);
			Console.WriteLine();
		}
	}
}
