var consoleLogger = Solid.Dip.Loggers.ConsoleLogger.Instance;

var employeeFileSerializer = new Solid.Dip.Persistance.EmployeeFileSerializer();
var repository = new Solid.Dip.Persistance.EmployeeFileRepository(employeeFileSerializer);

var employees = repository.GetAll();

foreach (var employee in employees)
{
    var payslip = new Solid.Dip.Documents.Payslip(employee, Month.August);
    consoleLogger.Info(payslip.ToTxt().ToUpper());
}