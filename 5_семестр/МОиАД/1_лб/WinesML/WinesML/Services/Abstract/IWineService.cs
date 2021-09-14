namespace WinesML.Services.Abstract
{
	interface IWineService
	{
		double CalculateExpectedValue();
		double CalculateRmsBiasFromMean();
		double CalculateDispersion();
	}
}
