using Core;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var amountOfCriterias = GetIntFromConsole("Enter an amount of criteria: ");

        var criterias = new Criteria[amountOfCriterias];
        for (var criteriaIndex = 0; criteriaIndex < amountOfCriterias; criteriaIndex++)
        {
            var mentionsAmount = GetIntFromConsole($"Enter an amount of mentions for { criteriaIndex + 1 } criteria: ");
            var mentions = new Mention[mentionsAmount];
            for (var mentionIndex = 0; mentionIndex < mentionsAmount; mentionIndex++)
            {
                mentions[mentionIndex] = new Mention { Name = $"k{ mentionIndex + 1 }", Value = 1.0 / (mentionIndex + 1.0) };
            }
            criterias[criteriaIndex] = new Criteria { Index = criteriaIndex, Name = $"K{ criteriaIndex + 1 }", Mentions = new MentionCollection(mentions) };
        }

        Console.WriteLine("Your criterias: ");
        WriteEnumerableToConsole(criterias);

        ClearConsoleAfterAnyUserAct();

        IAllTheoryAlternativesGetable criteriaService = new CriteriaService(criterias);
        var alternatives = await criteriaService.GetAllTheoryAlternativesAsync();
        WriteEnumerableToConsole(alternatives);

        ClearConsoleAfterAnyUserAct();

        var theoryAlternativesCount = criteriaService.TheoryAlternativesCount;
        Console.WriteLine($"Theory number of all alternatives = { theoryAlternativesCount }");

        ClearConsoleAfterAnyUserAct();
    
        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        Console.WriteLine($"The best alternative is { alternativeService.GetBest() }");
        Console.WriteLine($"The worst alternative is { alternativeService.GetWorst() }");

        ClearConsoleAfterAnyUserAct();

        var isFound = false;
        Alternative compareAlternative = default;
        do
        {
            Console.WriteLine("Enter alternative name to compare: ");
            var name = Console.ReadLine();
            if (alternatives.Any(x => x.Name.Equals(name)))
            {
                compareAlternative = alternatives.First(x => x.Name.Equals(name));
                isFound = true;
            }
        } while (!isFound);

        var task1 = Task.Run(() => alternativeService.GetBetterAlternatives(compareAlternative));
        var task2 = Task.Run(() => alternativeService.GetWorseAlternatives(compareAlternative));
        var task3 = Task.Run(() => alternativeService.GetIncomparableAlternatives(compareAlternative));

        await Task.WhenAll(task1, task2, task3);

        var betterAlternatives = task1.Result;
        var worseAlternatives = task2.Result;
        var incomparableAlternatives = task3.Result;

        var betterCount = betterAlternatives.Count();
        Console.WriteLine($"Better alternatives({ betterCount }): ");
        WriteEnumerableToConsole(betterAlternatives);

        var worseCount = worseAlternatives.Count();
        Console.WriteLine($"\nWorse alternatives({ worseCount }): ");
        WriteEnumerableToConsole(worseAlternatives);

        var incomparableCount = incomparableAlternatives.Count();
        Console.WriteLine($"\nIncomparable alternatives({ incomparableCount }): ");
        WriteEnumerableToConsole(incomparableAlternatives);

        var sum = betterCount + worseCount + incomparableCount + 1;
        Console.WriteLine($"\n{ betterCount } + { worseCount } + { incomparableCount } + 1 = { sum }");
        Console.WriteLine($"Theory number of all alternatives = { theoryAlternativesCount }");
    }

    private static int GetIntFromConsole(string message)
    {
        uint number;
        string? input;
        do
        {
            Console.Write(message);
            input = Console.ReadLine();
        } while (!uint.TryParse(input, out number));
        return (int)number;
    }

    private static void WriteEnumerableToConsole<T>(IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
            Console.WriteLine(item);
    }

    private static void ClearConsoleAfterAnyUserAct()
    {
        Console.ReadKey();
        Console.Clear();
    }
}