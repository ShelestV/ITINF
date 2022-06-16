using Solid.Dip.Personnel;

namespace Solid.Dip.Notifications;

public class EmployeeNotifier : IEmployeeNotifable
{
    public void Notify(Employee employee)
    {
        var message = new EmailMessage
        {
            From = "payment@globomantics.com",
            To = employee.Email,
            Subject = "Salary Notification",
            Body = $"Salary sent; Value: ${employee.MonthlyIncome}"
        };

        // Send message
        Thread.Sleep(500);

        Console.WriteLine($"Notified {message}");
    }
}
