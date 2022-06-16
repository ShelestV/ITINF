using Solid.Dip.Personnel;

namespace Solid.Dip.Taxes.Factories;

public sealed class TaxCalculatorFactory
{
    public static TaxCalculator Create(Employee employee)
    {
        // ToDo: It's facking shit. This factory valotates Barbara Liscov prinsiple
        // Why is it here? And why some programmers define this shit as pattern???
        // ...
        if (employee is Intern)
            return new InternTaxCalculator();

        if (employee is PartTimeEmployee)
            return new PartTimeTaxCalculator();

        if (employee is FullTimeEmployee)
            return new FullTimeTaxCalculator();

        throw new IllegalArgumentException("Incorrect type of Employee");
    }
}
