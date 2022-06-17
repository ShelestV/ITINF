namespace Solid.Dip.Taxes;

public sealed class FullTimeTaxCalculator : TaxCalculator
{
    private const int RetirementTaxPercentage = 10;
    private const int IncomeTaxPercentage = 16;
    private const int BaseHealthInsurance = 100;

    public override double Calculate(int monthlyIncome)
    {
        return BaseHealthInsurance 
            + CalculateTaxes(monthlyIncome, RetirementTaxPercentage) 
            + CalculateTaxes(monthlyIncome, IncomeTaxPercentage);
    }
}
