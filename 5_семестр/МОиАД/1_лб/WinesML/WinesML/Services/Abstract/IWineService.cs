namespace WinesML.Services.Abstract
{
	public interface IWineService
	{
		double CalculateExpectedValue();
		double CalculateRmsBiasFromMean();
		double CalculateDispersion();
	}
}
