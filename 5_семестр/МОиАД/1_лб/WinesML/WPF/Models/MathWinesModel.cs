using System;
using System.Collections.Generic;
using WinesML.DataProcessings;

namespace WPF.Models
{
	class MathWinesModel
	{
		private double expectedValue;
		private double rmsBiasFromMean;
		private double dispersion;

		public string Attribute { get; }
		public double ExpectedValue { get => Math.Round(expectedValue, 3); }
		public double RmsBiasFromMean { get => Math.Round(rmsBiasFromMean, 3); }
		public double Dispersion { get => Math.Round(dispersion, 3); }

		public MathWinesModel(string attribute, IEnumerable<double> attributeWinesCollection)
		{
			Attribute = attribute;
			expectedValue = Mathematic.CalculateExpectedValue(attributeWinesCollection);
			rmsBiasFromMean = Mathematic.CalculateRmsBiasFromMean(attributeWinesCollection);
			dispersion = Mathematic.CalculateDispersion(attributeWinesCollection);
		}
	}
}
