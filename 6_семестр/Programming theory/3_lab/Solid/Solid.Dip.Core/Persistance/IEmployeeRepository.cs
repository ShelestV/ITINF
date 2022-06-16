namespace Solid.Dip.Persistance;

// ToDo: use generics instead this
// GetAll() => Items { get; }
public interface IEmployeeRepository
{
    IList<Personnel.Employee> GetAll();
    void Save(Personnel.Employee employee);
}
