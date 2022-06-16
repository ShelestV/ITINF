namespace Solid.Dip.Taxes;

// ToDo: Should be generic type!!!
// To childrens of this class we can provide incorrect employee
// For example To InternTaxCalculator I can transfer FullTimeEmployee (because Calculate() wait for base class)
public abstract class TaxCalculator
{
   public abstract double Calculate(Personnel.Employee employee);

    protected static double CalculateTaxes(int monthlyIncome, int taxPercentage)
    {
        return (monthlyIncome * taxPercentage) / 100.0;
    }
}
