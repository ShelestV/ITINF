using Solid.Dip.Notifications;
using Solid.Dip.Persistance;
using Solid.Dip.Personnel;

namespace Solid.Dip.Payment;

public sealed class PaymentProcessor<TEmployee> where TEmployee : Employee
{
    private readonly IRepository<TEmployee> employeeRepository;
    private readonly INotifable employeeNotifier;

    public PaymentProcessor(IRepository<TEmployee> employeeRepository, INotifable employeeNotifier)
    {
        this.employeeRepository = employeeRepository;
        this.employeeNotifier = employeeNotifier;
    }

    public int SendPayments()
    {
        var employees = this.employeeRepository.Items;
        var totalPayments = 0;

        foreach (var employee in employees)
        {
            totalPayments += employee.MonthlyIncome;
            var email = FormEmail(employee.Email, employee.MonthlyIncome);
            employeeNotifier.Notify(email);
        }

        return totalPayments;
    }

    private static EmailMessage FormEmail(string email, int monthlyIncome)
    {
        return new()
        {
            From = "payment@globomantics.com",
            To = email,
            Subject = "Salary Notification",
            Content = $"Salary sent; Value: ${monthlyIncome}"
        };
    }
}
