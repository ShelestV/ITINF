var employeeFileSerializer = new Solid.Dip.Persistance.EmployeeFileSerializer();
var repository = new Solid.Dip.Persistance.EmployeeFileRepository(employeeFileSerializer);

var employees = repository.GetAll();
var manager = new Solid.Dip.Personnel.FullTimeEmployee("Steve Jackson", 5000);

foreach (var employee in employees)
    employee.RequestTimeOff(1, manager);