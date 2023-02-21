using PaymentSystem.Calculators;
using System.Text.Json;

namespace PaymentSystem.Domain;

public sealed class HourlyPayedEmployee : Employee
{
    public const string Type = "Hourly";

    public int WorkedHours { get; set; }

    public HourlyPayedEmployee() : base()
    {
    }

    public HourlyPayedEmployee(JsonElement jsonElement) : base(jsonElement)
    {
        Payment = jsonElement.GetProperty(nameof(Payment)).GetDouble();
        WorkedHours = jsonElement.GetProperty(nameof(WorkedHours)).GetInt32();
    }

    public override void CalculateSalary()
    {
        Balance += HourlyPaymentCalculator.calculate(Payment, WorkedHours);
        WorkedHours = 0;
    }

    public override void CopyDataFrom(Employee employee)
    {
        // Never happened but...
        if (employee is not HourlyPayedEmployee hpEmployee)
            return;

        base.CopyDataFrom(employee);

        Payment = hpEmployee.Payment;
        WorkedHours = hpEmployee.WorkedHours;
    }

    public override string ToJson()
    {
        return $@"
    {{
        ""{nameof(Id)}"": {Id},
        ""{nameof(Type)}"": ""{Type}"",
        ""{nameof(Name)}"": ""{Name}"",
        ""{nameof(Balance)}"": {Balance:0.00},
        ""{nameof(Payment)}"": {Payment:0.00},
        ""{nameof(WorkedHours)}"": {WorkedHours}
    }}";
    }
}