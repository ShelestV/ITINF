namespace Chess.Rules;

internal static class ArraysExtensions
{
    public static int GetCount<T>(this T[][] array)
    {
        var count = 0;
        for (var i = 0; i < array.Length; i++)
            for (var j = 0; j < array[i].Length; j++)
                count++;

        return count;
    }
}
