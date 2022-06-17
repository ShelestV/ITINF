namespace Solid.Dip.Taxes.Mappers;

internal class MapperList<TEmployee> where TEmployee : Personnel.Employee
{
    private readonly IList<EmployeeTypeTaxCalculator> list;

    public MapperList()
    {
        this.list = new List<EmployeeTypeTaxCalculator>(); ;
    }

    public TaxCalculator this[Type type] => list.First(x => x.EmployeeType.Equals(type)).TaxCalculator;

    public void Add(Type employeeType, TaxCalculator taxCalculator)
    {
        if (!list.Any(x => x.EmployeeType.Equals(employeeType)))
            list.Add(new(employeeType, taxCalculator));
    }
}
