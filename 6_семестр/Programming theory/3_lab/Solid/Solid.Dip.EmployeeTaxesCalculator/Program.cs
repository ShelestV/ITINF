using Solid.Dip.Loggers;
using Solid.Dip.Persistance;
using Solid.Dip.Personnel;

var consoleLogger = ConsoleLogger.Instance;

var serializer = new EmployeeSerializer();
var fileWorker = new CsvFileWorker();

var repository = new FileRepository<Employee>(fileWorker, serializer);

var employees = repository.Items;

var totalTaxes = 0.0;
foreach (var employee in employees)
{
    var taxCalculator = Solid.Dip.Taxes.Mappers.TaxCalculatorEmployeeMapper<Employee>.GetTaxCalculator(employee);

    var tax = taxCalculator.Calculate(employee);
    consoleLogger.Info($"{employee.FullName} taxes: ${tax}");

    totalTaxes += tax;
}

consoleLogger.Info($"Total taxes = ${totalTaxes}");

Console.ReadKey();