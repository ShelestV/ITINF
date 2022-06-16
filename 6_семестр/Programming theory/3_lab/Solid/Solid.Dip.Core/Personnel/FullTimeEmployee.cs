namespace Solid.Dip.Personnel;

public sealed class FullTimeEmployee : Employee
{
    private const int WorkNbHoursPerWeek = 40;

    public FullTimeEmployee(string fullName, int monthlyIncome) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = WorkNbHoursPerWeek;
    }

    public override void RequestTimeOff(int nbDays, Employee manager)
    {
        Console.WriteLine($"Time off request for full time employee {this.FullName}; " +
            $"Nb days {nbDays}; " +
            $"Requested from {manager.FullName}");
    }
}
