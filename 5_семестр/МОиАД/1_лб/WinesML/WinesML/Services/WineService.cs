using System;
using System.Linq;
using WinesML.Models;

namespace WinesML.Services
{
	class WineService : Abstract.IWineService
	{
		private Wine wine;
		
		public WineService(Wine wine)
		{
			this.wine = wine;
		}

		public double CalculateExpectedValue()
		{
			return (1 / wine.NumberOfAttrubutes) * wine.AttributesArray.Sum();
		}

		public double CalculateRmsBiasFromMean()
		{
			double numerator = wine.AttributesArray.Select(att => Math.Pow(att - CalculateExpectedValue(), 2)).Sum();
			double denominator = wine.NumberOfAttrubutes - 1;
			return Math.Sqrt(numerator / denominator);
		}

		public double CalculateDispersion()
		{
			return Math.Pow(CalculateRmsBiasFromMean(), 2);
		}
	}
}
