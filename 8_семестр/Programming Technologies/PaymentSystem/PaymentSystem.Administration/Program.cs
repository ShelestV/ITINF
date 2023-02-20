using PaymentSystem.Persistence;

namespace PaymentSystem.Administration;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		var repository = new EmployeeRepository();

		ApplicationConfiguration.Initialize();
		System.Windows.Forms.Application.Run(new EmployeesForm(repository));
	}
}