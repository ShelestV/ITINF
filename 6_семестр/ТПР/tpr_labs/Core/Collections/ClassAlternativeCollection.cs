using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Core;

internal class ClassAlternativeCollection : IClassAlternativeCollection
{
    private IEnumerable<ClassAlternative> alternatives;

    public ClassAlternativeCollection(IEnumerable<Alternative> alternatives)
    {
        this.alternatives = alternatives.ToClassAlternatives();

        this.UpdateAlternativeGroup(0, AlternativeGroup.Good);
        this.UpdateAlternativeGroup(this.alternatives.Count() - 1, AlternativeGroup.Bad);
    }

    public void UpdateAlternativeGroup(int index, AlternativeGroup group)
    {
        var alternative = this.alternatives.ElementAt(index);
        alternative.ChangeGroup(group);
        this.alternatives = this.alternatives.Update(index, alternative);
    }

    public void CalculateDistancesToGoodCenter()
    {
        var goodCenter = this.alternatives.Where(x => x.Group == AlternativeGroup.Good).Center();
        this.alternatives = this.alternatives.CalculateDistancesToGood(goodCenter);
    }

    public void CalculateDistancesToBadCenter()
    {
        var badCenter = this.alternatives.Where(x => x.Group == AlternativeGroup.Bad).Center();
        this.alternatives = this.alternatives.CalculateDistancesToBad(badCenter);
    }

    public void CalculateProximitiesToGoodCenter()
    {
        this.alternatives = this.alternatives.CalculateGoodProximities();
    }

    public void CalculateProximitiesToBadCenter()
    {
        this.alternatives = this.alternatives.CalculateBadProximities();
    }

    public void CalculateNumberOfBetterAlternatives()
    {
        this.alternatives = this.alternatives.CalculateBetterAlternatives();
    }

    public void CalculateNumberOfWorseAlternatives()
    {
        this.alternatives = this.alternatives.CalculateWorseAlternatives();
    }

    public void CalculateGoodInformativenesses()
    {
        this.alternatives = this.alternatives.CalculateGoodInformativenesses();
    }

    public void CalculateBadInformativenesses()
    {
        this.alternatives = this.alternatives.CalculateBadInformativenesses();
    }

    public IEnumerator<ClassAlternative> GetEnumerator() =>
        this.alternatives.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}
