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
        
        var ways = new IList<TreeAlternative>[current.Neighbours.Count];
        var wayIndex = 0;
        foreach (var neighbour in current.Neighbours)
        {
            var changedWay = new List<TreeAlternative>(way) { neighbour };
            ways[wayIndex++] = GoToNext(neighbour, changedWay);
        }

        var maxWayCount = ways.Select(x => x.Count).Max();
        return ways.First(x => x.Count == maxWayCount);
    }
}
