namespace Solid.Dip.Personnel;

public class Manager : FullTimeEmployee
{
    public override string Position => "Manager";

    public Manager(string fullName, int monthlyIncome) : base(fullName, monthlyIncome)
    {
    }

    public void ProcessTimeOffRequest(Employee employee, int nbDays)
    {
        Console.WriteLine($"Time off request for {employee.Position} {employee.FullName}; " +
            $"Nb days {nbDays}; " +
            $"Requested from {this.FullName}");
    }
}
