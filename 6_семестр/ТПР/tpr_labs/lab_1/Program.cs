using Core;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var criterias = ConsoleUIHelper.GetCriteriasByUserData(true);

        IAllTheoryAlternativesGetable criteriaService = new CriteriaService(criterias);
        var alternatives = await criteriaService.GetAllTheoryAlternativesAsync();
        ConsoleUIHelper.WriteEnumerableToConsole(alternatives);

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var theoryAlternativesCount = criteriaService.TheoryAlternativesCount;
        Console.WriteLine($"Theory number of all alternatives = { theoryAlternativesCount }");

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();
    
        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        Console.WriteLine($"The best alternative is { alternativeService.GetBest() }");
        Console.WriteLine($"The worst alternative is { alternativeService.GetWorst() }");

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var isFound = false;
        var compareAlternative = default(Alternative);
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
        ConsoleUIHelper.WriteEnumerableToConsole(betterAlternatives);

        var worseCount = worseAlternatives.Count();
        Console.WriteLine($"\nWorse alternatives({ worseCount }): ");
        ConsoleUIHelper.WriteEnumerableToConsole(worseAlternatives);

        var incomparableCount = incomparableAlternatives.Count();
        Console.WriteLine($"\nIncomparable alternatives({ incomparableCount }): ");
        ConsoleUIHelper.WriteEnumerableToConsole(incomparableAlternatives);

        var sum = betterCount + worseCount + incomparableCount + 1;
        Console.WriteLine($"\n{ betterCount } + { worseCount } + { incomparableCount } + 1 = { sum }");
        Console.WriteLine($"Theory number of all alternatives = { theoryAlternativesCount }");
    }
}