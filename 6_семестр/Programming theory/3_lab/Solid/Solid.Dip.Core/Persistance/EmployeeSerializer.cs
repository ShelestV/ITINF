namespace Solid.Dip.Persistance;

public class EmployeeSerializer : ISerializable<Personnel.Employee>
{
    public string Serialize(Personnel.Employee employee)
    {
        return $"{employee.FullName},{employee.MonthlyIncome},{employee.NbHoursPerWeek}";
    }

    public Personnel.Employee Serialize(string str)
    {
        var employeeRecord = str.Split(',');
        var name = employeeRecord[0];
        var income = int.Parse(employeeRecord[1]);
        var nbHours = int.Parse(employeeRecord[2]);

        if (nbHours == 40)
            return new Personnel.FullTimeEmployee(name, income);
        if (nbHours == 20)
            return new Personnel.PartTimeEmployee(name, income);
        return new Personnel.Intern(name, income, nbHours);
    }
}
