using System;
using System.Collections.Generic;

namespace Core;

internal static class ConsoleUIHelper
{
    internal static IEnumerable<Criteria> GetCriteriasByUserData()
    {
        var amountOfCriterias = GetIntFromConsole("Enter an amount of criteria: ");

        var criterias = new Criteria[amountOfCriterias];
        for (var criteriaIndex = 0; criteriaIndex < amountOfCriterias; criteriaIndex++)
        {
            var mentionsAmount = GetIntFromConsole($"Enter an amount of mentions for { criteriaIndex + 1 } criteria: ");
            var mentions = new Mention[mentionsAmount];
            for (var mentionIndex = 0; mentionIndex < mentionsAmount; mentionIndex++)
            {
                mentions[mentionIndex] = new Mention { Name = $"k{ mentionIndex + 1 }", Value = mentionIndex + 1.0 };
            }
            criterias[criteriaIndex] = new Criteria { Index = criteriaIndex, Name = $"K{ criteriaIndex + 1 }", Mentions = new MentionCollection(mentions) };
        }

        Console.WriteLine("Your criterias: ");
        WriteEnumerableToConsole(criterias);

        ClearConsoleAfterAnyUserAct();

        return criterias;
    }

    internal static void WriteEnumerableToConsole<T>(IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
            Console.WriteLine(item);
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