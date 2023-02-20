using PaymentSystem.Domain;

namespace PaymentSystem.Administration.ViewModels;

public sealed class EmployeeViewModel
{
	private readonly Employee employee;

	public int Id { get; set; }
	public string Name { get; set; } = null!;
	private readonly double payment;
	private readonly bool isHourly;
	public string Payment => payment.ToString() + (isHourly ? "/hour" : "/month");

	public EmployeeViewModel(Employee employee)
	{
		this.employee = employee;

		Id = employee.Id;
		Name = employee.Name;
		isHourly = employee is HourlyPayedEmployee;
		payment = employee is HourlyPayedEmployee hpEmployee ? hpEmployee.Payment :
			employee is MonthlyPayedEmployee mpEmployee ? mpEmployee.Payment : 0.0;
	}

	public Employee GetEmployee()
	{
		return employee;
	}
}
