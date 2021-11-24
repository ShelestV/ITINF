using TreesWithManyAttributes.Models;

namespace TreesWithManyAttributes.Algorithms
{
    public static class Sorting
    {
        public static void SortDataByAttribute(Data data, string attribute)
        {
            for (var i = 0; i < data.Count - 1; ++i)
            {
                for (var j = 0; j < data.Count - i - 1; ++j)
                {
                    if (data[j][attribute].Compare(data[j + 1][attribute]) < 0)
                        data.Swipe(j, j + 1);
                }
            }
        }
    }
}