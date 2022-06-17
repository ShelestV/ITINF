namespace Solid.Dip.Persistance;

public sealed class FileRepository<TItem> : IRepository<TItem>
{
    private readonly ISerializable<TItem> serializer;
    private readonly IFileWorkable fileWorker;

    public FileRepository(IFileWorkable fileWorker, ISerializable<TItem> serializer)
    {
        this.fileWorker = fileWorker;
        this.serializer = serializer;
    }

    public IList<TItem> Items
    {
        get
        {
            var employees = new List<TItem>();
            var lines = this.fileWorker.ReadLines();
            foreach (var line in lines)
                employees.Add(this.serializer.Serialize(line));
            return employees;
        }
    }

    public void Save(TItem employee)
    {
        var serializedString = this.serializer.Serialize(employee);
        this.fileWorker.WriteLine(serializedString);
    }
}
