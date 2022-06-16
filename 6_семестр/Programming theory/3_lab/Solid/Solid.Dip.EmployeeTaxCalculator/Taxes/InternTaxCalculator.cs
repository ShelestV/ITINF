using Solid.Dip.Personnel;

namespace Solid.Dip.Taxes;

public sealed class InternTaxCalculator : TaxCalculator
{
    private const int IncomeTaxPercentage = 16;

    public override double Calculate(Employee employee)
    {
        var monthlyIncome = employee.MonthlyIncome;
        return 350 <= monthlyIncome ? CalculateTaxes(monthlyIncome, IncomeTaxPercentage) : 0;
    }
}
