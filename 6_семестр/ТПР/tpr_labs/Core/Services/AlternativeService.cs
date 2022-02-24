using Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core;

public class AlternativeService : IAlternativesDivider
{
    private readonly IEnumerable<IAlternative> alternatives;

    /// <summary>
    /// Set all theory alternatives to correct work of get the best and get the worst
    /// </summary>
    /// <param name="alternatives"></param>
    public AlternativeService(IEnumerable<IAlternative> alternatives) =>
        this.alternatives = alternatives;

    public IAlternative GetBest() => this.alternatives.First();
    public IAlternative GetWorst() => this.alternatives.Last();

    public IEnumerable<IAlternative> GetBetterAlternatives(IAlternative comparableAlternative) =>
        this.GetComparedAlternatives(comparableAlternative, AlternativeCompareResult.Better);

    public IEnumerable<IAlternative> GetWorseAlternatives(IAlternative comparableAlternatives) =>
        this.GetComparedAlternatives(comparableAlternatives, AlternativeCompareResult.Worse);

    public IEnumerable<IAlternative> GetIncomparableAlternatives(IAlternative comparableAlternative) =>
        this.GetComparedAlternatives(comparableAlternative, AlternativeCompareResult.Incomparable);

    private IEnumerable<IAlternative> GetComparedAlternatives(IAlternative comparableAlternative, AlternativeCompareResult expectedResult)
    {
        var result = new List<IAlternative>();
        Parallel.ForEach(this.alternatives, alternative =>
        {
            var compareResult = CompareAlternatives(alternative, comparableAlternative);
            if (compareResult == expectedResult)
            {
                lock (result)
                {
                    result.Add(alternative);
                }
            }
        });
        return result;
    }

    private static AlternativeCompareResult CompareAlternatives(IAlternative first, IAlternative second)
    {
        var index = 0;
        int compareResult;
        do
        {
            compareResult = first.Mentions[index].CompareTo(second.Mentions[index]);
            index++;
            if (first.Mentions.Count == index && IsEqualedAlternatives(compareResult))
                return AlternativeCompareResult.Equal;
        } while (IsEqualedAlternatives(compareResult));

        for (var i = index; i < first.Mentions.Count; i++)
        {
            var comp = first.Mentions[i].CompareTo(second.Mentions[i]);
            if (comp != compareResult && comp != 0)
                return AlternativeCompareResult.Incomparable;
        }
        return compareResult == 1 ? AlternativeCompareResult.Better : AlternativeCompareResult.Worse;
    }

    private static bool IsEqualedAlternatives(int compareResult) => compareResult == 0;
}
