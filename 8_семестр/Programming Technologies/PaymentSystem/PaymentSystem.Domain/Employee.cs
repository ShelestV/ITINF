using System.Text.Json;

namespace PaymentSystem.Domain;

public abstract class Employee : Model
{
    public string Name { get; set; } = null!;
    public double Payment { get; set; }
    public double Balance { get; set; }

    protected Employee()
    {
    }

    protected Employee(JsonElement jsonElement)
    {
		Id = jsonElement.GetProperty(nameof(Id)).GetInt32();
		Name = jsonElement.GetProperty(nameof(Name)).GetString()!;
        Payment = jsonElement.GetProperty(nameof(Payment)).GetDouble();
		Balance = jsonElement.GetProperty(nameof(Balance)).GetDouble();
	}

    public abstract void CalculateSalary();
    public abstract string ToJson();

    public virtual void CopyDataFrom(Employee employee)
    {
        Name = employee.Name;
        Balance = employee.Balance;
    }
}