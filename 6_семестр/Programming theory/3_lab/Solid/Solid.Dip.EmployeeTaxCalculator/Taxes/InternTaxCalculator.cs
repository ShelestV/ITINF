namespace Solid.Dip.Taxes;

public sealed class InternTaxCalculator : TaxCalculator
{
    private const int IncomeTaxPercentage = 16;

    public override double Calculate(int monthlyIncome)
    {
        return 350 <= monthlyIncome ? CalculateTaxes(monthlyIncome, IncomeTaxPercentage) : 0;
    }
}
