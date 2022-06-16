var consoleLogger = Solid.Dip.Loggers.ConsoleLogger.Instance;
var employeeFileSerializer = new Solid.Dip.Persistance.EmployeeFileSerializer();
var repository = new Solid.Dip.Persistance.EmployeeFileRepository(employeeFileSerializer);

var employees = repository.GetAll();

var totalTaxes = 0.0;
foreach (var employee in employees)
{
    var taxCalculator = Solid.Dip.Taxes.Factories.TaxCalculatorFactory.Create(employee);

    var tax = taxCalculator.Calculate(employee);
    consoleLogger.Info($"{employee.FullName} taxes: ${tax}");

    totalTaxes += tax;
}

consoleLogger.Info($"Total taxes = ${totalTaxes}");