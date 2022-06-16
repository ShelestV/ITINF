using Solid.Dip.Notifications;
using Solid.Dip.Persistance;

namespace Solid.Dip.Payment;

internal sealed class PaymentProcessor
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IEmployeeNotifable employeeNotifier;

    public PaymentProcessor(IEmployeeRepository employeeRepository, IEmployeeNotifable employeeNotifier)
    {
        this.employeeRepository = employeeRepository;
        this.employeeNotifier = employeeNotifier;
    }

    public int SendPayments()
    {
        var employees = this.employeeRepository.GetAll();
        var totalPayments = 0;

        foreach (var employee in employees)
        {
            totalPayments += employee.MonthlyIncome;
            employeeNotifier.Notify(employee);
        }

        return totalPayments;
    }
}
