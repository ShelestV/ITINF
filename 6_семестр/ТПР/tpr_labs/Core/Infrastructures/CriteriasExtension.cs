using System;
using System.Linq;
using System.Collections.Generic;

namespace Core;

internal static class CriteriasExtension
{
    private static readonly CriteriaComperator criteriaComperator = new();
    private static readonly MentionComparator mentionComperator = new();

    public static Alternative[,] GetFirstReferenceSituation(this IEnumerable<Criteria> criterias, int alternativesCount, bool useUserNames)
    {
        var criteriasCount = criterias.Count();

        criterias.ToList().Sort(criteriaComperator);
        foreach (var criteria in criterias)
            criteria.Mentions.ToList().Sort(mentionComperator);

        var bestAlternative = GetBestAlternative(criterias, useUserNames);

        var maxNumberOfMentions = criterias.Select(x => x.Mentions.Count).Max();
        var alternativesResult = new Alternative[alternativesCount, criteriasCount];
        for (var mentionIndex = 0; mentionIndex < maxNumberOfMentions; mentionIndex++)
        {
            foreach (var criteria in criterias)
            {
                var alternative = bestAlternative.Copy();
                alternative.Name = GetAlternativeName(criteriasCount, bestAlternative.Name, criteria.Index, mentionIndex + 1);
                var mentionName = GetMentionName(criteria, criteria.Mentions[mentionIndex], useUserNames);
                alternative.Mentions[criteria.Index] = new() { Name = mentionName, Value = mentionIndex + 1 };
                alternativesResult[mentionIndex, criteria.Index] = alternative;
            }
        }

        return alternativesResult;
    }

    private static Alternative GetBestAlternative(IEnumerable<Criteria> criterias, bool useUserNames)
    {
        var bestAlternativeName = Concat('1', criterias.Count());
        return new()
        {
            Index = 0,
            Name = bestAlternativeName,
            Mentions = new(criterias.Select(x => new Mention { Name = GetMentionName(x, x.Mentions[0], useUserNames), Value = x.Mentions[0].Value }))
        };
    }

    private static string GetMentionName(Criteria criteria, Mention mention, bool useUserNames)
    {
        return useUserNames ? mention.Name : Mention.GetAlternativeName(criteria.Mentions[0], criteria);
    } 

    private static string GetAlternativeName(int mentionsCount, string bestAlternativeName, int changedMentionIndex, int newValue)
    {
        var alternative = new char[mentionsCount];
        Array.Copy(bestAlternativeName.ToCharArray(), alternative, mentionsCount);
        alternative[changedMentionIndex] = newValue.ToString()[0];
        return new string(alternative);
    }

    private static string Concat(char symbol, int repeatNumber)
    {
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < repeatNumber; i++)
            sb.Append(symbol);
        return sb.ToString();
    }
}
