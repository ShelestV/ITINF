using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinesML.DataProcessings
{
	public class Mathematic
	{
		public static double CalculateExpectedValue(IEnumerable<double> collection)
		{
			return collection.Sum() / collection.Count();
		}

		public static async Task<double> CalculateExpectedValueAsync(IEnumerable<double> collection)
		{
			return await Task.Run(() => CalculateExpectedValue(collection));
		}

		public static double CalculateRmsBiasFromMean(IEnumerable<double> collection)
		{
			double numerator = collection.Select(att => Math.Pow(att - CalculateExpectedValue(collection), 2)).Sum();
			double denominator = collection.Count() - 1;
			return Math.Sqrt(numerator / denominator);
		}

		public static async Task<double> CalculateRmsBiasFromMeanAsync(IEnumerable<double> collection)
		{
			return await Task.Run(() => CalculateRmsBiasFromMean(collection));
		}

		public static double CalculateDispersion(IEnumerable<double> collection)
		{
			return Math.Pow(CalculateRmsBiasFromMean(collection), 2);
		}

		public static async Task<double> CalculateDispersionAsync(IEnumerable<double> collection)
		{
			return await Task.Run(() => CalculateDispersion(collection));
		}
	}
}
