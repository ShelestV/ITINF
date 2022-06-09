using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core;

internal static class TwoDimensionalArrayExtension
{
    public static int GetRowsCount<T>(this T[,] data) => data.GetLength(0);
    public static int GetColumnsCount<T>(this T[,] data) => data.GetLength(1);

    public static T[] GetRow<T>(this T[,] data, int rowIndex)
    {
        var columnsCount = data.GetColumnsCount();
        var result = new T[columnsCount];
        Parallel.For(0, columnsCount, i => result[i] = data[rowIndex, i]);
        return result;
    }

    public static T[] GetColumn<T>(this T[,] data, int columnIndex)
    {
        var rowsCount = data.GetRowsCount();
        var result = new T[rowsCount];
        Parallel.For(0, rowsCount, i => result[i] = data[i, columnIndex]);
        return result;
    }

    public static void OutputWithHeader<T>(this T[,] data, IEnumerable<string> headers)
    {
        CheckOnCorrectSizes(headers.Count(), data.GetColumnsCount());
        OutputHeaders(headers);

        for (var i = 0; i < data.GetRowsCount(); i++)
        {
            for (var j = 0; j < data.GetColumnsCount(); j++)
                Console.Write(data[i, j] + "\t");
            Console.WriteLine();
        }
    }

    public static void OutputWithHeader(this Alternative[,] alternatives, IEnumerable<string> headers)
    {
        CheckOnCorrectSizes(headers.Count(), alternatives.GetColumnsCount());
        OutputHeaders(headers);

        for (var i = 0; i < alternatives.GetRowsCount(); i++)
        {
            for (var j = 0; j < alternatives.GetColumnsCount(); j++)
                Console.Write(alternatives[i, j].Name + "\t");
            Console.WriteLine();
        }
    }

    private static void CheckOnCorrectSizes(int dataCount, int headersCount)
    {
        if (dataCount != headersCount)
            throw new ArgumentException("Number of columns should be equaled to headers number");
    }

    private static void OutputHeaders(IEnumerable<string> headers)
    {
        foreach (var header in headers)
            Console.Write(header + "\t");
        Console.WriteLine();
    }
}
