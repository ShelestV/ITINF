namespace Solid.Dip.Persistance;

public interface ISerializable<T>
{
    string Serialize(T item);
    T Serialize(string str);
}
