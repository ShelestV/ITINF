using System.Collections.Generic;
using System.Linq;

namespace Core;

internal sealed class TreeAlternativesFactory
{
    private readonly IList<TreeAlternative> alternatives;


    private static readonly TreeAlternativesFactory instance = new();
    public static TreeAlternativesFactory Instance => instance;
    private TreeAlternativesFactory()
    {
        this.alternatives = new DistinctedList<TreeAlternative>();
    }

    public TreeAlternative Create(Alternative alternative)
    {
        var treeAlternative = this.alternatives
            .FirstOrDefault(x => x.Alternative.Equals(alternative));
        return treeAlternative ?? CreateNew(alternative);
    }

    private TreeAlternative CreateNew(Alternative alternative)
    {
        var newTreeAlternative = new TreeAlternative() { Alternative = alternative };
        this.alternatives.Add(newTreeAlternative);
        return newTreeAlternative;
    }
}
