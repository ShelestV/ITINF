using System.Threading.Tasks;

namespace WinesML.Services.Abstract
{
	public interface IWineServiceAsync
	{
		Task<double> CalculateExpectedValueAsync();
		Task<double> CalculateRmsBiasFromMeanAsync();
		Task<double> CalculateDispersionAsync();
	}
}
