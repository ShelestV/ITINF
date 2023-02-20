using PaymentSystem.Administration.ViewModels;
using PaymentSystem.Application;

namespace PaymentSystem.Administration;

public partial class EmployeesForm : Form
{
	private readonly IEmployeeRepository employeeRepository;

	private IList<EmployeeViewModel> employees;
	private int selectedEmployeeId = -1;

	public EmployeesForm(IEmployeeRepository employeeRepository)
	{
		this.employeeRepository = employeeRepository;
		employees = new List<EmployeeViewModel>();

		InitializeComponent();
	}

	private void EmployeesForm_Load(object sender, EventArgs e)
	{
		PopulateEmployees();
	}

	private void PopulateEmployees()
	{
		employees = this.employeeRepository.GetAll().Select(e => new EmployeeViewModel(e)).ToList();
		this.employeesDataGridView.DataSource = employees;
	}

	private void AddButtonClick(object sender, EventArgs e)
	{
		var form = new SaveEmployeeForm(this.employeeRepository);
		form.ShowDialog();
		PopulateEmployees();
	}

	private void UpdateButtonClick(object sender, EventArgs e)
	{
		if (selectedEmployeeId <= 0)
			return;

		var employeeVM = employees.FirstOrDefault(e => e.Id == selectedEmployeeId);
		if (employeeVM is null)
			return;

		var form = new SaveEmployeeForm(this.employeeRepository, employeeVM.GetEmployee());
		form.ShowDialog();
		PopulateEmployees();
	}

	private void DeleteButtonClick(object sender, EventArgs e)
	{
		if (selectedEmployeeId <= 0)
			return;

		var result = MessageBox.Show("Are you sure?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (result != DialogResult.Yes)
			return;

		var employeeVM = employees.FirstOrDefault(x => x.Id == selectedEmployeeId);
		if (employeeVM is null)
			return;

		this.employeeRepository.Delete(employeeVM.Id);
		this.employeeRepository.SaveChanges();
		PopulateEmployees();
	}

	private void EmployeesDataGridRowClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (int.TryParse(employeesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out var id))
			selectedEmployeeId = id;
	}
}
