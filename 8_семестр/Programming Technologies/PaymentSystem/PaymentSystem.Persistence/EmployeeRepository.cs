using PaymentSystem.Application;
using PaymentSystem.Domain;
using System.Text.Json;

namespace PaymentSystem.Persistence;

public sealed class EmployeeRepository : JsonFileRepository, IEmployeeRepository
{
    private readonly IList<Employee> employees;

    public EmployeeRepository()
    {
        this.employees = GetData()
            .Select(CreateEmployeeByJson)
            .Where(x => x is not null)
            .Select(x => x!)
            .ToList();
    }

    public int Add(Employee employee)
    {
        var maxId = employees.Any() ? employees.Max(x => x.Id) : 0;
        employee.Id = maxId + 1;

		employees.Add(employee);

        return employee.Id;
    }

    public void Update(Employee employee)
    {
        employees.FirstOrDefault(x => x.Id == employee.Id)?.CopyDataFrom(employee);
    }

    public void Delete(int id)
    {
        var employee = employees.FirstOrDefault(x => x.Id == id);
        if (employee is not null)
            employees.Remove(employee);
    }

    public Employee? Get(int id)
    {
        return employees.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyCollection<Employee> GetAll()
    {
        return employees.ToArray();
    }

    public void SaveChanges()
    {
        var employeeJsons = string.Join(",\n", employees.Select(e => e.ToJson()));
        Save($"[\n{employeeJsons}\n]");
    }

    private static Employee? CreateEmployeeByJson(JsonElement json)
    {
        return json.GetProperty("Type").GetString()! switch
        {
            HourlyPayedEmployee.Type => new HourlyPayedEmployee(json),
            MonthlyPayedEmployee.Type => new MonthlyPayedEmployee(json),
            _ => null
        };

	}
}