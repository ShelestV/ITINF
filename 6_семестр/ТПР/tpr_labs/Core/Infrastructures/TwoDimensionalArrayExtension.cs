using System.Threading.Tasks;

namespace Core;

internal static class TwoDimensionalArrayExtension
{
    public static int GetRowsCount<T>(this T[,] data) => data.GetLength(0);
    public static int GetColumnsCount<T>(this T[,] data) => data.GetLength(1);

    public static T[] GetRow<T>(this T[,] data, int rowIndex)
    {
        var result = new T[data.GetColumnsCount()];
        Parallel.For(0, data.GetColumnsCount(), i => result[i] = data[rowIndex, i]);
        return result;
    }
}
