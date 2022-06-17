using Solid.Dip.Loggers;
using Solid.Dip.Persistance;
using Solid.Dip.Personnel;

var consoleLogger = ConsoleLogger.Instance;

var serializer = new EmployeeSerializer();
var fileWorker = new CsvFileWorker();

var repository = new FileRepository<Employee>(fileWorker, serializer);

try
{
    var newEmployee = new FullTimeEmployee("Volodymyr Shelest", 2000);
    repository.Save(newEmployee);
    consoleLogger.Info("Employee has been saved");
}
catch (Exception ex)
{
    consoleLogger.Error("Error saving employee", ex);
}

Console.ReadKey();