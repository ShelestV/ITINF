namespace Solid.Dip.Personnel;

public class FullTimeEmployee : Employee
{
    private const int WorkNbHoursPerWeek = 40;

    public override string Position => "Full Time Worker";

    public FullTimeEmployee(string fullName, int monthlyIncome) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = WorkNbHoursPerWeek;
    }
}
