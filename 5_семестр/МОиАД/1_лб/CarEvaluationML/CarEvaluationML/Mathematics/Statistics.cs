using System;
using System.Collections.Generic;
using System.Linq;

namespace CarEvaluationML.Mathematics
{
	class Statistics
	{
		public static double CalculateExpectedValue(IEnumerable<int> collection)
		{
			return collection.Sum() / collection.Count();
		}

		public static double CalculateRmsBiasFromMean(IEnumerable<int> collection)
		{
			double numerator = collection.Select(att => Math.Pow(att - CalculateExpectedValue(collection), 2)).Sum();
			double denominator = collection.Count() - 1;
			return Math.Sqrt(numerator / denominator);
		}

		public static double CalculateDispersion(IEnumerable<int> collection)
		{
			return Math.Pow(CalculateRmsBiasFromMean(collection), 2);
		}
	}
}
