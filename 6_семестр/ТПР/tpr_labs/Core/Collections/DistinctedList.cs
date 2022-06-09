using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Core;

internal sealed class DistinctedList<T> : IList<T>
{
    private readonly IList<T> data;

    public DistinctedList()
    {
        this.data = new List<T>();
    }

    public DistinctedList(IEnumerable<T> data)
    {
        this.data = data.ToList();
    }

    public T this[int index] 
    { 
        get => this.data[index]; 
        set => this.data[index] = value; 
    }

    public int Count => this.data.Count;

    public bool IsReadOnly => this.data.IsReadOnly;

    public void Add(T item)
    {
        if (data.All(x => !(x!.Equals(item))))
            this.data.Add(item);
        
    }

    public void Clear()
    {
        this.data.Clear();
    }

    public bool Contains(T item)
    {
        return this.data.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        this.data.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this.data.GetEnumerator();
    }

    public int IndexOf(T item)
    {
        return this.data.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
        this.data.Insert(index, item);
    }

    public bool Remove(T item)
    {
        return this.data.Remove(item);
    }

    public void RemoveAt(int index)
    {
        this.data.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.data.GetEnumerator();
    }
}
