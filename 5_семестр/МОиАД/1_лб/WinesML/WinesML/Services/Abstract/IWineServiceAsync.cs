using System.Threading.Tasks;

namespace WinesML.Services.Abstract
{
	interface IWineServiceAsync
	{
		Task<double> CalculateExpectedValueAsync();
		Task<double> CalculateRmsBiasFromMeanAsync();
		Task<double> CalculateDispersionAsync();
	}
}
