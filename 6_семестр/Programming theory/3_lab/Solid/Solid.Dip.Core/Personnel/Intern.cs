namespace Solid.Dip.Personnel;

public class Intern : Employee
{
    public Intern(string fullName, int monthlyIncome, int nbHours) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = nbHours;
    }

    public override void RequestTimeOff(int nbDays, Employee manager)
    {
        Console.WriteLine($"Time off request for intern {this.FullName}; " +
            $"Nb days {nbDays}; " +
            $"Requested from {manager.FullName}");
    }
}
