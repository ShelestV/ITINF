namespace Solid.Dip.Personnel;

public abstract class Employee
{
    private readonly string firstName;
    private readonly string lastName;
    private int monthlyIncome;
    private int nbHoursPerWeek;

    public string FullName => $"{this.firstName} {this.lastName}";
    public string Email => $"{this.firstName}.{this.lastName}@globomantichr.com";

    public int MonthlyIncome
    {
        get => this.monthlyIncome;
        set
        {
            if (value < 0)
                throw new IllegalArgumentException("Income must be positive");
            this.monthlyIncome = value;
        }
    }

    public int NbHoursPerWeek
    {
        get => this.nbHoursPerWeek;
        set
        {
            if (value <= 0)
                throw new IllegalArgumentException("Number of hours must be greater than 0");
            this.nbHoursPerWeek = value;
        }
    }

    public Employee(string fullName, int monthlyIncome)
    {
        this.MonthlyIncome = monthlyIncome;

        var names = fullName.Split(' ');
        this.firstName = names[0];
        this.lastName = names[1];
    }

    // ToDo: I don't think that Employee should write about number of hours that he worked
    // I think we should delegate this method to other class and use it
    public abstract void RequestTimeOff(int nbDays, Employee manager);

    public override string ToString()
    {
        return $"{this.firstName} {this.lastName} - {this.monthlyIncome}";
    }
}
