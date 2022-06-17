namespace Solid.Dip.Personnel;

public class PartTimeEmployee : Employee
{
    private const int WorkNbHoursPerWeek = 20;

    public override string Position => "Part Time Worker";

    public PartTimeEmployee(string fullName, int monthlyIncome) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = WorkNbHoursPerWeek;
    }
}
