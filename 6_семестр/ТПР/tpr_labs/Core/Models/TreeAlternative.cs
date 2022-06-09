using System;
using System.Collections.Generic;

namespace Core;

internal class TreeAlternative
{
    public Alternative Alternative { get; set; }
    public IList<TreeAlternative> Neighbours { get; init; } = new DistinctedList<TreeAlternative>();

    public override bool Equals(object? obj)
    {
        return obj is TreeAlternative treeAlternative &&
            this.Alternative.Equals(treeAlternative.Alternative);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Alternative);
    }
}
