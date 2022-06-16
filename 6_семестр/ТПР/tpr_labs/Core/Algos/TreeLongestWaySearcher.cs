using System.Collections.Generic;
using System.Linq;

namespace Core.Algos;

internal class TreeLongestWaySearcher
{
    private readonly TreeAlternative root;

    public TreeLongestWaySearcher(TreeAlternative root)
    {
        this.root = root;
    }

    public IList<TreeAlternative> FindLongestWay()
    {
        return this.GoToNext(root, new List<TreeAlternative>() { root });
    }

    private IList<TreeAlternative> GoToNext(TreeAlternative current, IList<TreeAlternative> way)
    {
        if (current.Neighbours.Count == 0)
            return way;

        var ways = new List<IList<TreeAlternative>>();
        foreach (var neighbour in current.Neighbours)
        {
            if (way.Contains(neighbour))
                continue;

            var changedWay = new List<TreeAlternative>(way) { neighbour };
            ways.Add(this.GoToNext(neighbour, changedWay));
        }

        if (ways.Count == 0)
            return way;

        var maxWayCount = ways.Select(x => x.Count).Max();
        return ways.First(x => x.Count == maxWayCount);
    }
}
