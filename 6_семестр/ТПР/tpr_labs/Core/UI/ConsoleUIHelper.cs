using System;
using System.Linq;
using System.Collections.Generic;

namespace Core;

internal static class ConsoleUIHelper
{
    internal static IList<Criteria> GetCriteriasByUserData(bool useUserNames)
    {
        var amountOfCriterias = GetIntFromConsole("Enter an amount of criteria: ");

        var criterias = new Criteria[amountOfCriterias];
        for (var criteriaIndex = 0; criteriaIndex < amountOfCriterias; criteriaIndex++)
        {
            var criteriaName = useUserNames ? GetUserName($"Enter a name of { criteriaIndex + 1 } criteria: ") : GetDefaultCriteriaName(criteriaIndex);
            var mentionsAmount = GetIntFromConsole($"Enter an amount of mentions for { criteriaName }: ");
            var mentions = new Mention[mentionsAmount];
            for (var mentionIndex = 0; mentionIndex < mentionsAmount; mentionIndex++)
            {
                var mentionName = useUserNames ? GetUserName($"Enter a name of { mentionIndex + 1 } mention: ") : GetDefaultMentionName(mentionIndex);
                mentions[mentionIndex] = new Mention { Name = mentionName, Value = mentionIndex + 1.0 };
            }
            criterias[criteriaIndex] = new Criteria { Index = criteriaIndex, Name = criteriaName, Mentions = new MentionCollection(mentions) };
        }

        Console.WriteLine("Your criterias: ");
        WriteEnumerableToConsole(criterias);

        ClearConsoleAfterAnyUserAct();

        return criterias;
    }

    internal static IList<Alternative> GetAlternativesByUserData(IEnumerable<Criteria> criterias, bool useUserNames)
    {
        var amountOfAlternatives = GetIntFromConsole("Enter amount of alternatives: ");

        var alternatives = new Alternative[amountOfAlternatives];
        var criteriasCount = criterias.Count();
        for (var alternativeIndex = 0; alternativeIndex < amountOfAlternatives; alternativeIndex++)
            alternatives[alternativeIndex] = CreateAlternative(criterias, criteriasCount, alternativeIndex, useUserNames);

        Console.WriteLine("Your alternatives: ");
        WriteEnumerableToConsole(alternatives);

        ClearConsoleAfterAnyUserAct();

        return alternatives;
    }

    private static Alternative CreateAlternative(IEnumerable<Criteria> criterias, int criteriasCount, int alternativeIndex, bool useUserNames)
    {
        var mentions = new Mention[criteriasCount];
        var alternativeName = useUserNames ? GetUserName($"Enter { alternativeIndex + 1 } alternative name: ") : GetDefaultAlternativeName(alternativeIndex);
        Console.WriteLine($"{alternativeName}:");

        var criteriaIndex = 0;
        foreach (var criteria in criterias)
            ChooseAlternativeMentions(mentions, criteria, criteriasCount, ref criteriaIndex, useUserNames);

        return new()
        {
            Name = alternativeName,
            Index = alternativeIndex,
            Mentions = new(mentions)
        };
    }

    private static string GetUserName(string message)
    {
        return GetStringFromConsole(message);
    }

    private static string GetDefaultCriteriaName(int criteriaIndex)
    {
        return $"K{criteriaIndex + 1}";
    }

    private static string GetDefaultMentionName(int mentionIndex)
    {
        return $"k{mentionIndex + 1}";
    }

    private static string GetDefaultAlternativeName(int alternativeIndex)
    {
        return $"A{alternativeIndex + 1}";
    }

    private static void ChooseAlternativeMentions(Mention[] mentions, Criteria criteria, int criteriasCount, ref int criteriaIndex, bool useUserNames)
    {
        var numeratedMentions = string.Join("; ", criteria.Mentions.NumerateFrom1());
        var message = $"Choose a mention of criteria({criteria.Name}): \n{numeratedMentions}: ";
        var choice = GetDiapazonNumberFromConsole(message, 1, criteriasCount);
        mentions[criteriaIndex] = new() 
        {
            Name = useUserNames ? criteria.Mentions[choice - 1].Name : Mention.GetAlternativeName(criteria.Mentions[choice - 1], criteria), 
            Value = criteria.Mentions[choice - 1].Value 
        };
        ++criteriaIndex;
        Console.WriteLine();
    }

    internal static void WriteEnumerableToConsole<T>(IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
            Console.WriteLine(item);
    }

    internal static string GetStringFromConsole(string message)
    {
        Console.Write(message);
        return Console.ReadLine()!;
    }

    internal static bool GetBooleanFromConsole(string message)
    {
        message += "\n1) Yes\n2) No\n";
        var choice = GetDiapazonNumberFromConsole(message, 1, 2);
        return choice == 1;
    }

    internal static int GetDiapazonNumberFromConsole(string message, int from, int to)
    {
        return GetNumberFromConsoleByCondition(message, x => from <= x && x <= to);
    }

    internal static int GetIntFromConsole(string message)
    {
        return GetNumberFromConsoleByCondition(message, x => x > 0);
    }

    internal static AlternativeGroup GetAlternativeGroupFromConsole(string message)
    {
       
        return (AlternativeGroup)GetNumberFromConsoleByCondition(message, x => x == 1 || x == 2);
    }

    private static int GetNumberFromConsoleByCondition(string message, Func<int, bool> condition)
    {
        int number;
        string? input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
        } while (!int.TryParse(input, out number) || !condition(number));
        return number;
    }

    internal static void ClearConsoleAfterAnyUserAct()
    {
        Console.ReadKey();
        Console.Clear();
    }
}