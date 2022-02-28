using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Core;

public static class AlternativesExtension
{
    public static IEnumerable<ClassAlternative> ToClassAlternatives(this IEnumerable<Alternative> alternatives)
    {
        var classAlternatives = new ClassAlternative[alternatives.Count()];

        var alternativeIndex = 0;
        foreach (var alternative in alternatives)
        {
            classAlternatives[alternativeIndex] = alternative.ToClassAlternative();
            ++alternativeIndex;
        }

        return classAlternatives;
    }

    public static ClassAlternative Center(this IEnumerable<ClassAlternative> alternatives)
    {
        var mentionsCount = alternatives.First().Mentions.Count;
        var centerMentionsValues = new double[mentionsCount];
        centerMentionsValues = centerMentionsValues.Zero().ToArray();

        Parallel.For(0, mentionsCount, (mentionIndex) =>
        {
            foreach (var alternative in alternatives)
            {
                centerMentionsValues[mentionIndex] += alternative.Mentions[mentionIndex].Value;
            }
            centerMentionsValues[mentionIndex] /= (double)alternatives.Count();
        });

        return new ClassAlternative
        {
            Mentions = new MentionCollection(centerMentionsValues.Select(x => new Mention { Value = x }))
        };
    }

    public static IEnumerable<ClassAlternative> CalculateDistancesToGood(this IEnumerable<ClassAlternative> alternatives, ClassAlternative to)
    {
        return CalculateDistances(alternatives, to, AlternativeGroup.Good);
    }

    public static IEnumerable<ClassAlternative> CalculateDistancesToBad(this IEnumerable<ClassAlternative> alternatives, ClassAlternative to)
    {
        return CalculateDistances(alternatives, to, AlternativeGroup.Bad);
    }

    private static IEnumerable<ClassAlternative> CalculateDistances(IEnumerable<ClassAlternative> alternatives, ClassAlternative to, AlternativeGroup group)
    {
        foreach (var alternative in alternatives)
        {
            var classAlternative = alternative;
            switch (group)
            {
                case AlternativeGroup.Good:
                    classAlternative.DistanceToGoodCenter = alternative.DistanceTo(to);
                    break;
                case AlternativeGroup.Bad:
                    classAlternative.DistanceToBadCenter = alternative.DistanceTo(to);
                    break;
            }
            yield return classAlternative;
        }
    }

    public static IEnumerable<ClassAlternative> CalculateGoodProximities(this IEnumerable<ClassAlternative> alternatives)
    {
        return CalculateProximities(alternatives, AlternativeGroup.Good);
    }

    public static IEnumerable<ClassAlternative> CalculateBadProximities(this IEnumerable<ClassAlternative> alternatives)
    {
        return CalculateProximities(alternatives, AlternativeGroup.Bad);
    }

    private static IEnumerable<ClassAlternative> CalculateProximities(IEnumerable<ClassAlternative> alternatives, AlternativeGroup group)
    {
        var goodDistances = alternatives.Select(x => x.DistanceToGoodCenter);
        var badDistances = alternatives.Select(x => x.DistanceToBadCenter);
        var maxDistance = goodDistances.Join(badDistances).Max();

        foreach (var alternative in alternatives)
        {
            var good = alternative.DistanceToGoodCenter;
            var bad = alternative.DistanceToBadCenter;
            switch (group)
            {
                case AlternativeGroup.Good:
                    yield return CalculateGoodProximity(alternative, good, bad, maxDistance);
                    break;
                case AlternativeGroup.Bad:
                    yield return CalculateBadProximity(alternative, good, bad, maxDistance);
                    break;
                default:
                    throw new ArgumentException("Cannot calculate proximity");
            }
        }
    }

    private static ClassAlternative CalculateGoodProximity(ClassAlternative alternative, double distanceToGoodCenter, double distanceToBadCenter, double maxDistance)
    {
        var proximity = alternative.Group == AlternativeGroup.Good ? 1 : alternative.Group == AlternativeGroup.Bad ? 0 :
            (maxDistance - distanceToGoodCenter) / ((2 * maxDistance) - distanceToGoodCenter - distanceToBadCenter);
        alternative.ProximityToGoodCenter = proximity;
        return alternative;
    }

    private static ClassAlternative CalculateBadProximity(ClassAlternative alternative, double distanceToGoodCenter, double distanceToBadCenter, double maxDistance)
    {
        var proximity = alternative.Group == AlternativeGroup.Bad ? 1 : alternative.Group == AlternativeGroup.Good ? 0 :
            (maxDistance - distanceToBadCenter) / ((2 * maxDistance) - distanceToGoodCenter - distanceToBadCenter);
        alternative.ProximityToBadCenter = proximity;
        return alternative;
    }

    public static IEnumerable<ClassAlternative> CalculateBetterAlternatives(this IEnumerable<ClassAlternative> alternatives)
    {
        var compareAlternatives = GetUndefinedAlternatives(alternatives);
        compareAlternatives = CalculateGroupAlternatives(compareAlternatives, AlternativeCompareResult.Better);
        return WriteResult(alternatives, compareAlternatives);
    }

    public static IEnumerable<ClassAlternative> CalculateWorseAlternatives(this IEnumerable<ClassAlternative> alternatives)
    {
        var compareAlternatives = GetUndefinedAlternatives(alternatives);
        compareAlternatives = CalculateGroupAlternatives(compareAlternatives, AlternativeCompareResult.Worse);
        return WriteResult(alternatives, compareAlternatives);
     }

    private static IEnumerable<ClassAlternative> GetUndefinedAlternatives(IEnumerable<ClassAlternative> allAlternatives)
    {
        var alternatives = allAlternatives.Where(x => x.Group == AlternativeGroup.Undefined);
        return alternatives;
    }

    private static IEnumerable<ClassAlternative> CalculateGroupAlternatives(this IEnumerable<ClassAlternative> alternatives, AlternativeCompareResult expectedResult)
    {
        var resultAlternatives = new List<ClassAlternative>();
        foreach (var alternative in alternatives)
        {
            var result = 0;
            var compareAlternatives = alternatives;
            foreach (var compAlt in compareAlternatives)
            {
                var compareResult = AlternativeService.CompareAlternatives(compAlt, alternative);
                if (compareResult == expectedResult)
                    result++;
            }

            var resultAlternative = alternative;
            if (expectedResult == AlternativeCompareResult.Better)
            {
                resultAlternative.NumberOfBetter = result;
            }
            else if (expectedResult == AlternativeCompareResult.Worse)
            {
                resultAlternative.NumberOfWorse = result;
            }
            resultAlternatives.Add(resultAlternative);
        }
        return resultAlternatives;
    }

    private static IEnumerable<ClassAlternative> WriteResult(IEnumerable<ClassAlternative> allAlternatives, IEnumerable<ClassAlternative> compareAlternatives)
    {
        var index = 0;
        foreach (var alternative in allAlternatives)
        {
            if (compareAlternatives.Any(x => x.Index == alternative.Index))
            {
                yield return compareAlternatives.First(x => x.Index == alternative.Index);
                index++;
                continue;
            }
            var compareAlternative = alternative;
            compareAlternative.NumberOfBetter = 0;
            compareAlternative.NumberOfWorse = 0;
            yield return compareAlternative;
        }
    }

    public static IEnumerable<ClassAlternative> CalculateGoodInformativenesses(this IEnumerable<ClassAlternative> alternatives)
    {
        foreach (var alternative in alternatives)
        {
            var classAlternative = alternative;
            classAlternative.InformativenessOfGood = classAlternative.ProximityToGoodCenter * classAlternative.NumberOfBetter;
            yield return classAlternative;
        }
    }

    public static IEnumerable<ClassAlternative> CalculateBadInformativenesses(this IEnumerable<ClassAlternative> alternatives)
    {
        foreach (var alternative in alternatives)
        {
            var classAlternative = alternative;
            classAlternative.InformativenessOfBad = classAlternative.ProximityToBadCenter * classAlternative.NumberOfWorse;
            yield return classAlternative;
        }
    }
}
