namespace Solid.Dip.Personnel;

public class Intern : Employee
{
    public override string Position => "Intern";

    public Intern(string fullName, int monthlyIncome, int nbHours) : base(fullName, monthlyIncome)
    {
        this.NbHoursPerWeek = nbHours;
    }
}
