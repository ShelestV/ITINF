using System.Collections.Generic;

namespace Core;

internal sealed class CriteriaComperator : IComparer<Criteria>
{
    public int Compare(Criteria x, Criteria y)
    {
        return x.Index - y.Index;
    }
}
