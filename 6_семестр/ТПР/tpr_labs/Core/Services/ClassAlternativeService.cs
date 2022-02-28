using System.Collections.Generic;
using System.Linq;

namespace Core;

public class ClassAlternativeService : IAlternativesClassificable
{
    private readonly IClassAlternativeCollection collection;

    public IClassAlternativeCollection Collection => this.collection;

    public ClassAlternativeService(IEnumerable<Alternative> alternatives)
    {
        this.collection = new ClassAlternativeCollection(alternatives);
        this.CanDoIteration = this.collection.Any(x => x.Group == AlternativeGroup.Undefined);
    }

    public bool CanDoIteration { get; private set; }

    public ClassAlternative DoIteration()
    {
        this.Iteration();

        var undefinedAlternatives = this.collection.Where(x => x.Group == AlternativeGroup.Undefined);
        var maxInformativeness = undefinedAlternatives.Select(x => x.Informativeness).Max();
        return undefinedAlternatives.First(x => x.Informativeness == maxInformativeness);
    }

    public void LastIteration()
    {
        this.Iteration();
    }

    private void Iteration()
    {
        this.collection.CalculateDistancesToGoodCenter();
        this.collection.CalculateDistancesToBadCenter();
        this.collection.CalculateProximitiesToGoodCenter();
        this.collection.CalculateProximitiesToBadCenter();
        this.collection.CalculateNumberOfBetterAlternatives();
        this.collection.CalculateNumberOfWorseAlternatives();
        this.collection.CalculateGoodInformativenesses();
        this.collection.CalculateBadInformativenesses();
    }

    public void UpdateAlternativesGroup(ClassAlternative altWithMaxInfo, AlternativeGroup group)
    {
        if (group == AlternativeGroup.Good)
        {
            this.UpdateAlternatives(altWithMaxInfo, group, AlternativeCompareResult.Better);
        }
        else if (group == AlternativeGroup.Bad)
        {
            this.UpdateAlternatives(altWithMaxInfo, group, AlternativeCompareResult.Worse);
        }
        this.CanDoIteration = this.collection.Any(x => x.Group == AlternativeGroup.Undefined);
    }

    private void UpdateAlternatives(ClassAlternative altWithMaxInfo, AlternativeGroup group, AlternativeCompareResult expectedResult)
    {
        var count = this.collection.Count();
        this.collection.UpdateAlternativeGroup(altWithMaxInfo.Index, group);
        for (var alternativeIndex = 0; alternativeIndex < count; ++alternativeIndex)
        {
            var alternative = this.collection.ElementAt(alternativeIndex);
            if (alternative.Group != AlternativeGroup.Undefined)
                continue;

            var compareResult = AlternativeService.CompareAlternatives(alternative, altWithMaxInfo);
            if (compareResult == expectedResult)
            {
                this.collection.UpdateAlternativeGroup(alternativeIndex, group);
            }
        }
    }
}
