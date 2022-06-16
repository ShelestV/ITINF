namespace Solid.Dip.Persistance;

public class EmployeeFileSerializer : ISerializable<Personnel.Employee>
{
    public string Serialize(Personnel.Employee employee)
    {
        var sb = new System.Text.StringBuilder();
        sb.Append($"NAME: {employee.FullName}");
        sb.Append($"POSITION: {employee.GetType().Name}");
        sb.Append($"EMAIL: {employee.Email}");
        sb.Append($"MONTHLY WAGE: {employee.MonthlyIncome}");
        return sb.ToString();
    }
}
