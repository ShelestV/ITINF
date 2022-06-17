namespace Solid.Dip.Taxes;

public abstract class TaxCalculator
{
    public abstract double Calculate(int monthlyIncome);

    protected static double CalculateTaxes(int monthlyIncome, int taxPercentage)
    {
        return (monthlyIncome * taxPercentage) / 100.0;
    }
}
