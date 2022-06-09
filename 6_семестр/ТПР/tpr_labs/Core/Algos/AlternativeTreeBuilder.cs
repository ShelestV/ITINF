namespace Core.Algos;

internal sealed class AlternativeTreeBuilder
{
    private readonly Alternative[][] alternativesChains;
    private readonly TreeAlternative root;


    private readonly TreeAlternativesFactory factory;

    public AlternativeTreeBuilder(Alternative[][] alternativesChains)
    {
        this.factory = TreeAlternativesFactory.Instance;

        this.alternativesChains = alternativesChains;
        this.root = this.factory.Create(alternativesChains[0][0]);
    }

    public TreeAlternative Build()
    {
        foreach (var chain in this.alternativesChains)
        {
            TreeAlternative? previous = null;
            foreach (var alternative in chain)
            {
                var newTreeAlternative = previous is null ? this.root : this.factory.Create(alternative);

                if (previous is not null)
                    previous.Neighbours.Add(newTreeAlternative);

                previous = newTreeAlternative;
            }
        }

        return this.root;
    }
}
