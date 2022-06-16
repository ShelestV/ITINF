namespace Solid.Dip.Personnel;

public sealed class PartTimeEmployee : Employee
{
    private const int WorkNbHoursPerWeek = 20;

    public PartTimeEmployee(string fullName, int monthlyIncome) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = WorkNbHoursPerWeek;
    }

    public override void RequestTimeOff(int nbDays, Employee manager)
    {
        Console.WriteLine($"Time off request for part time employee {this.FullName}; " +
            $"Nb days {nbDays}; " +
            $"Requested from {manager.FullName}");
    }
}
