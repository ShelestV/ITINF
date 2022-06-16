using Solid.Dip.Personnel;

namespace Solid.Dip.Taxes;

public sealed class FullTimeTaxCalculator : TaxCalculator
{
    private const int RetirementTaxPercentage = 10;
    private const int IncomeTaxPercentage = 16;
    private const int BaseHealthInsurance = 100;

    public override double Calculate(Employee employee)
    {
        var monthlyIncome = employee.MonthlyIncome;
        return BaseHealthInsurance 
            + CalculateTaxes(monthlyIncome, RetirementTaxPercentage) 
            + CalculateTaxes(monthlyIncome, IncomeTaxPercentage);
    }
}
