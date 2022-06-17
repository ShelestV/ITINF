using Solid.Dip.Loggers;
using Solid.Dip.Persistance;
using Solid.Dip.Personnel;
using Solid.Dip.Documents;

var consoleLogger = ConsoleLogger.Instance;

var serializer = new EmployeeSerializer();
var fileWorker = new CsvFileWorker();

var repository = new FileRepository<Employee>(fileWorker, serializer);

var employees = repository.Items;

foreach (var employee in employees)
{
    var payslip = new Payslip(employee, Month.August);
    consoleLogger.Info(payslip.ToTxt().ToUpper());
}

Console.ReadKey();