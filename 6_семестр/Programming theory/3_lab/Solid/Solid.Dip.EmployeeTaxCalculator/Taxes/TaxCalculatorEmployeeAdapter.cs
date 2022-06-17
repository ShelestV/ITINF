using Solid.Dip.Personnel;

namespace Solid.Dip.Taxes;

public static class TaxCalculatorEmployeeAdapter
{
    private static readonly IDictionary<Type, TaxCalculator> taxCalculatorsDict;

    static TaxCalculatorEmployeeAdapter()
    {
        taxCalculatorsDict = new Dictionary<Type, TaxCalculator>
        {
            { typeof(Intern), new InternTaxCalculator() },
            { typeof(FullTimeEmployee), new FullTimeTaxCalculator() },
            { typeof(PartTimeEmployee), new PartTimeTaxCalculator() }
        };
    }

    public static TaxCalculator GetTaxCalculator(Employee employee)
    {
        return taxCalculatorsDict[employee.GetType()];
    }
}
