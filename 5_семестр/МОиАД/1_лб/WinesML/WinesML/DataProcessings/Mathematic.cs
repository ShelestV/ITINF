using System;
using System.Collections.Generic;
using System.Linq;

namespace WinesML.DataProcessings
{
	public class Mathematic
	{
		public static double CalculateExpectedValue(IEnumerable<double> collection)
		{
			return collection.Sum() / collection.Count();
		}

		public static double CalculateRmsBiasFromMean(IEnumerable<double> collection)
		{
			double numerator = collection.Select(att => Math.Pow(att - CalculateExpectedValue(collection), 2)).Sum();
			double denominator = collection.Count() - 1;
			return Math.Sqrt(numerator / denominator);
		}

		public static double CalculateDispersion(IEnumerable<double> collection)
		{
			return Math.Pow(CalculateRmsBiasFromMean(collection), 2);
		}
	}
}
