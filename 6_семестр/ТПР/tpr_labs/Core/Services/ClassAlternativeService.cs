using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async Task<ClassAlternative> DoIterationAsync()
    {
        await this.IterationAsync();

        var undefinedAlternatives = this.collection.Where(x => x.Group == AlternativeGroup.Undefined);
        var maxInformativeness = undefinedAlternatives.Select(x => x.Informativeness).Max();
        return undefinedAlternatives.First(x => x.Informativeness == maxInformativeness);
    }

    public async Task DoLastIterationAsync()
    {
        await this.IterationAsync();
    }

    private async Task IterationAsync()
    {
        var goodCentersCalculatingTask = Task.Run(this.collection.CalculateDistancesToGoodCenter);
        var badCentersCalculatingTask = Task.Run(this.collection.CalculateDistancesToBadCenter);
        await Task.WhenAll(goodCentersCalculatingTask, badCentersCalculatingTask);

        var proximitiesToGoodCenterCalculatingTask = Task.Run(this.collection.CalculateProximitiesToGoodCenter);
        var proximitiesToBadCenterCalculatingTask = Task.Run(this.collection.CalculateProximitiesToBadCenter);
        await Task.WhenAll(proximitiesToGoodCenterCalculatingTask, proximitiesToBadCenterCalculatingTask);

        var numberOfBetterAlternativesCalculatingTask = Task.Run(this.collection.CalculateNumberOfBetterAlternatives);
        var numberOfWorseAlternativesCalculatingTask = Task.Run(this.collection.CalculateNumberOfWorseAlternatives);
        await Task.WhenAll(numberOfBetterAlternativesCalculatingTask, numberOfWorseAlternativesCalculatingTask);

        var goodInformativenessCalculatingTask = Task.Run(this.collection.CalculateGoodInformativenesses);
        var badInformativenessCalculatingTask = Task.Run(this.collection.CalculateBadInformativenesses);
        await Task.WhenAll(goodInformativenessCalculatingTask, badInformativenessCalculatingTask);
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
