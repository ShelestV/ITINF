using PaymentSystem.Application;
using PaymentSystem.Domain;

namespace PaymentSystem.Administration;

public partial class SaveEmployeeForm : Form
{
	private readonly IEmployeeRepository employeeRepository;
	private readonly Employee? employee;

	public SaveEmployeeForm(IEmployeeRepository employeeRepository, Employee? employee = null)
	{
		this.employeeRepository = employeeRepository;
		this.employee = employee;

		InitializeComponent();

		if (employee is null)
		{
			Text = "Add Employee";

			workedHoursUpDown.Visible = false;
			workedHoursLabel.Visible = false;
			hoursSymbolLabel.Visible = false;
		}
		else
		{
			Text = "Update Employee";

			nameTextBox.Text = employee.Name;
			var isHourly = employee is HourlyPayedEmployee hpEmployee;
			isHourlyPayedEmployeeCheckBox.Checked = isHourly;

			if (!isHourly)
			{
				workedHoursUpDown.Visible = false;
				workedHoursLabel.Visible = false;
				hoursSymbolLabel.Visible = false;
			}
			else
			{
				workedHoursUpDown.Value = ((HourlyPayedEmployee)employee).WorkedHours;
			}

			paymentUpDown.Value = (decimal)employee.Payment;
		}
	}

	private void SaveButtonClick(object sender, EventArgs e)
	{
		var result = MessageBox.Show("Are you sure?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (result != DialogResult.Yes)
			return;

		if (employee is null)
			AddEmployee();
		else
			UpdateEmployee();

		employeeRepository.SaveChanges();
		Close();
	}

	private void AddEmployee()
	{
		var name = this.nameTextBox.Text;
		var isHourlyPayment = this.isHourlyPayedEmployeeCheckBox.Checked;
		var payment = this.paymentUpDown.Value;

		if (payment <= 0)
			return;

		Employee employee = isHourlyPayment ? new HourlyPayedEmployee() : new MonthlyPayedEmployee();
		employee.Name = name;
		employee.Balance = 0.0;
		if (employee is HourlyPayedEmployee hpEmployee)
		{
			hpEmployee.Payment = (double)payment;
			hpEmployee.WorkedHours = 0;
		}
		else
		{
			((MonthlyPayedEmployee)employee).Payment = (double)payment;
		}

		this.employeeRepository.Add(employee);
	}

	private void UpdateEmployee()
	{
		var name = this.nameTextBox.Text;
		var isHourlyPayment = this.isHourlyPayedEmployeeCheckBox.Checked;
		var payment = this.paymentUpDown.Value;
		var workedHours = this.workedHoursUpDown.Value;

		if (payment <= 0)
			return;

		Employee newEmployee = isHourlyPayment ? new HourlyPayedEmployee() : new MonthlyPayedEmployee();
		newEmployee.Id = employee!.Id;
		newEmployee.Name = name;
		if (newEmployee is HourlyPayedEmployee hpEmployee)
		{
			hpEmployee.Payment = (double)payment;
			hpEmployee.WorkedHours = (int)workedHours;
		}
		else
		{
			((MonthlyPayedEmployee)newEmployee).Payment = (double)payment;
		}

		this.employeeRepository.Update(newEmployee);
	}

	private void IsHourlyPayedCheckedChanged(object sender, EventArgs e)
	{
		if (sender is not CheckBox cb)
			return;

		var isHourly = cb.Checked;
		workedHoursUpDown.Visible = isHourly;
		workedHoursLabel.Visible = isHourly;
		hoursSymbolLabel.Visible = isHourly;
	}
}
