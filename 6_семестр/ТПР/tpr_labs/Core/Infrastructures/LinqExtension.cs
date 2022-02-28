using System;
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

    public static double Round(this double number, int dots)
    {
        return Math.Round(number * Math.Pow(10, dots)) / Math.Pow(10, dots);
    }

    public static IEnumerable<T> GetRow<T>(this IEnumerable<IEnumerable<T>> enumerable, int index)
    {
        return enumerable.ElementAt(index);
    }

    public static IEnumerable<T> GetColumn<T>(this IEnumerable<IEnumerable<T>> enumerable, int index)
    {
        foreach (var row in enumerable)
        {
            var current = 0;
            foreach (var item in row)
            {
                if (current == index)
                {
                    yield return item;
                    break;
                }
                current++;
            }
        }
    } 

    public static IEnumerable<T> Join<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        foreach (var item in first)
            yield return item;

        foreach (var item in second)
            yield return item;
    }
}
