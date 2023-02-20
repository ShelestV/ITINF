using PaymentSystem.Application;

namespace PaymentSystem.BusinessLogic;

public sealed class PaymentService : IPaymentService
{
    private readonly IEmployeeRepository employeeRepository;

    public PaymentService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public void SendPayment()
    {
        foreach (var employee in employeeRepository.GetAll())
        {
            employee.CalculateSalary();
            employeeRepository.Update(employee);
        }

        employeeRepository.SaveChanges();
    }
}