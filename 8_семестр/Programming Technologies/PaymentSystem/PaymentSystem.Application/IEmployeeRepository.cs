using PaymentSystem.Domain;

namespace PaymentSystem.Application;

public interface IEmployeeRepository
{
    int Add(Employee employee);
    void Update(Employee employee);
    void Delete(int id);
    Employee? Get(int id);
    IReadOnlyCollection<Employee> GetAll();
    void SaveChanges();
}