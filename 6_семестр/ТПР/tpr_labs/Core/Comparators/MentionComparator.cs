using System.Collections.Generic;

namespace Core;

internal class MentionComparator : IComparer<Mention>
{
    public int Compare(Mention x, Mention y)
    {
        return x.Value.CompareTo(y.Value);
    }
}
