using Solid.Dip.Personnel;

namespace Solid.Dip.Persistance;

// ToDo: maybe use generics instead Employee
// In future we can reuse this class for other goals
public sealed class EmployeeFileRepository : IEmployeeRepository
{
    private readonly Loggers.ILogger logger = Loggers.ConsoleLogger.Instance;

    private const string FileName = "employees.csv";
    private readonly ISerializable<Employee> serializer;

    public EmployeeFileRepository(ISerializable<Employee> serializer)
    {
        this.serializer = serializer;
    }

    public IList<Employee> GetAll()
    {
        var employees = new List<Employee>();

        try
        {
            using var stream = new StreamReader(FileName);
            var line = stream.ReadLine();
            while ((line = stream.ReadLine()) is not null)
            {
                employees.Add(CreateEmployeeFromCsv(line));
            }
        }
        catch (FileNotFoundException ex)
        {
            this.logger.Error($"Not exist file with name {FileName}", ex);
        }

        return employees;
    }

    public void Save(Employee employee)
    {
        try
        {
            var serializedString = this.serializer.Serialize(employee);
            using var stream = new StreamWriter(FileName);
            stream.WriteLine(serializedString);
        }
        catch (FileNotFoundException ex)
        {
            this.logger.Error($"Not exist file with name {FileName}", ex);
        }
    }

    private static Employee CreateEmployeeFromCsv(string line)
    {
        var employeeRecord = line.Split(',');
        var name = employeeRecord[0];
        var income = int.Parse(employeeRecord[1]);
        var nbHours = int.Parse(employeeRecord[2]);

        if (nbHours == 40)
            return new FullTimeEmployee(name, income);
        if (nbHours == 20)
            return new PartTimeEmployee(name, income);
        return new Intern(name, income, nbHours);

    }
}
