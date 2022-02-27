using System.Collections.Generic;
using System.Linq;

namespace Core;

internal static class LinqExtension
{
    public static int Multiply(this IEnumerable<int> ints)
    {
        var result = ints.Any() ? 1 : 0;
        foreach (var i in ints)
            result *= i;
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

    public static IEnumerable<T> Update<T>(this IEnumerable<T> enumerable, int index, T value)
    {
        var current = 0;
        foreach (var item in enumerable)
        {
            if (current == index)
            {
                yield return value;
                current++;
                continue;
            }

            yield return item;
            current++;
        }
    }

    public static IEnumerable<double> Zero(this IEnumerable<double> enumerable)
    {
        return enumerable.Select(item => 0.0);
    }
}
