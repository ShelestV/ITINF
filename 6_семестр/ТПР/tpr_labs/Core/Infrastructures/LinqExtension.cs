using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core;

public static class LinqExtension
{
    public static int Multiply(this IEnumerable<int> ints)
    {
        var result = ints.Any() ? 1 : 0;
        foreach (var i in ints)
            result *= i;
        return result;
    }

    public static int GetRowsCount<T>(this T[,] data) => data.GetLength(0);
    public static int GetColumnsCount<T>(this T[,] data) => data.GetLength(1);

    public static T[] GetRow<T>(this T[,] data, int rowIndex)
    {
        var result = new T[data.GetColumnsCount()];
        Parallel.For(0, data.GetColumnsCount(), i => result[i] = data[rowIndex, i]);
        return result;
    }

    public static IEnumerable<T> Insert<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        var current = 0;
        foreach (var item in enumerable)
        {
            if (current == index)
                yield return value;

            yield return item;
            current++;
        }
    }
}
