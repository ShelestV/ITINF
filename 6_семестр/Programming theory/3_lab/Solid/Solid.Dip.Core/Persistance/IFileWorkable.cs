namespace Solid.Dip.Persistance;

public interface IFileWorkable
{
    void WriteLine(string content);
    IList<string> ReadLines();
}
