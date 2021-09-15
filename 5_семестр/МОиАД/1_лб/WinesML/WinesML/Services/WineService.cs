using System;
using System.Linq;
using System.Threading.Tasks;
using WinesML.Models;

namespace WinesML.Services
{
	class WineService : Abstract.IWineService, Abstract.IWineServiceAsync
	{
		private Wine wine;
		
		public WineService(Wine wine)
		{
			this.wine = wine;
		}

		public async Task<double> CalculateExpectedValueAsync()
		{
			return await Task.Run(CalculateExpectedValue);
		}

		public double CalculateExpectedValue()
		{
			return (1 / wine.NumberOfAttrubutes) * wine.AttributesArray.Sum();
		}

		public async Task<double> CalculateRmsBiasFromMeanAsync()
		{
			return await Task.Run(CalculateRmsBiasFromMean);
		}

		public double CalculateRmsBiasFromMean()
		{
			double numerator = wine.AttributesArray.Select(att => Math.Pow(att - CalculateExpectedValue(), 2)).Sum();
			double denominator = wine.NumberOfAttrubutes - 1;
			return Math.Sqrt(numerator / denominator);
		}

		public async Task<double> CalculateDispersionAsync()
		{
			return await Task.Run(CalculateDispersion);
		}

		public double CalculateDispersion()
		{
			return Math.Pow(CalculateRmsBiasFromMean(), 2);
		}
	}
}
