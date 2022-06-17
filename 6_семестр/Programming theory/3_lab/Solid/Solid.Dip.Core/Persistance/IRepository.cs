namespace Solid.Dip.Persistance;

public interface IRepository<TItem>
{
    IList<TItem> Items { get; }
    void Save(TItem item);
}
