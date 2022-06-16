var employeeFileSerializer = new Solid.Dip.Persistance.EmployeeFileSerializer();
var consoleLogger = Solid.Dip.Loggers.ConsoleLogger.Instance;

var repository = new Solid.Dip.Persistance.EmployeeFileRepository(employeeFileSerializer);
var employees = repository.GetAll();

foreach (var employee in employees)
{
    try
    {
        repository.Save(employee);
        consoleLogger.Info($"Saved employee {employee}");
    }
    catch (Exception ex)
    {
        consoleLogger.Error("Error saving employee", ex);
    }
}