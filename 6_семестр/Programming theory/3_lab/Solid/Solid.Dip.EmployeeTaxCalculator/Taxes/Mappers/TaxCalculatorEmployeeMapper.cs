namespace Solid.Dip.Taxes.Mappers;

public static class TaxCalculatorEmployeeMapper<TEmployee>
    where TEmployee : Personnel.Employee
{
    private readonly static MapperList<TEmployee> mapper;

    static TaxCalculatorEmployeeMapper()
    {
        mapper = new MapperList<TEmployee>();

        mapper.Add(typeof(Personnel.Intern), new InternTaxCalculator());
        mapper.Add(typeof(Personnel.FullTimeEmployee), new FullTimeTaxCalculator());
        mapper.Add(typeof(Personnel.PartTimeEmployee), new PartTimeTaxCalculator());
    }

    public static TaxCalculator GetTaxCalculator(Personnel.Employee employee)
    {
        return mapper[employee.GetType()];
    }
}
