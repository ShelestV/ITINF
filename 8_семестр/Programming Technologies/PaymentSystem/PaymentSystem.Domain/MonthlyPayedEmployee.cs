using System.Text.Json;

namespace PaymentSystem.Domain;

public sealed class MonthlyPayedEmployee : Employee
{
    public const string Type = "Monthly";
    
    public MonthlyPayedEmployee() : base()
    {
    }

    public MonthlyPayedEmployee(JsonElement jsonElement) : base(jsonElement)
    {
		Payment = jsonElement.GetProperty(nameof(Payment)).GetDouble();
	}

    public override void CalculateSalary()
    {
        Balance += Payment;
    }

    public override void CopyDataFrom(Employee employee)
    {
        // Never happened but...
        if (employee is not MonthlyPayedEmployee mpEmployee)
            return;
        
        base.CopyDataFrom(employee);

        Payment = mpEmployee.Payment;
    }

    public override string ToJson()
    {
		return $@"
{{
    ""{nameof(Id)}"": {Id},
    ""{nameof(Type)}"": ""{Type}"",
    ""{nameof(Name)}"": ""{Name}"",
    ""{nameof(Balance)}"": {Balance:0.00},
    ""{nameof(Payment)}"": {Payment:0.00}
}}";
	}
}