using Solid.Dip.Persistance;
using Solid.Dip.Personnel;

var serializer = new EmployeeSerializer();
var fileWorker = new CsvFileWorker();

var repository = new FileRepository<Employee>(fileWorker, serializer);

var employees = repository.Items;
var manager = new Manager("Steve Jackson", 5000);

foreach (var employee in employees)
    manager.ProcessTimeOffRequest(employee, 1);

Console.ReadKey();