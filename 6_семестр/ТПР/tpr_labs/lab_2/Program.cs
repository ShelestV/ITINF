using Core;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var criterias = ConsoleUIHelper.GetCriteriasByUserData(false);

        IAllTheoryAlternativesGetable criteriaService = new CriteriaService(criterias);
        var alternatives = await criteriaService.GetAllTheoryAlternativesAsync();
        ConsoleUIHelper.WriteEnumerableToConsole(alternatives);

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var service = new ClassAlternativeService(alternatives);

        do
        {
            var altWithMaxInfo = await service.DoIterationAsync();
            OutputAlternativesTable(service);
            var group = ConsoleUIHelper.GetAlternativeGroupFromConsole("Enter alternative group (1 good, 2 bad): ");
            service.UpdateAlternativesGroup(altWithMaxInfo, group);
        } while (service.CanDoIteration);

        await service.DoLastIterationAsync();
        OutputAlternativesTable(service);
    }

    private static void OutputAlternativesTable(ClassAlternativeService service)
    {
        Console.Clear();
        Console.WriteLine("Alternative\t\tGroup\t\td1\td2\tp1\tp2\tg1\tg2\tF1\tF2\tF");
        foreach (var alternative in service.Collection)
        {
            Console.Write($"{alternative.Name}\t\t");
            var groupStringLength = alternative.Group.ToString().Length;
            var alternativeGroup = alternative.Group.ToString();
            Console.Write($"{alternativeGroup.Substring(0, groupStringLength < 5 ? groupStringLength : 5)}\t\t");
            Console.Write($"{alternative.DistanceToGoodCenter.Round(3)}\t");
            Console.Write($"{alternative.DistanceToBadCenter.Round(3)}\t");
            Console.Write($"{alternative.ProximityToGoodCenter.Round(3)}\t");
            Console.Write($"{alternative.ProximityToBadCenter.Round(3)}\t");
            Console.Write($"{alternative.NumberOfBetter}\t");
            Console.Write($"{alternative.NumberOfWorse}\t");
            Console.Write($"{alternative.InformativenessOfGood.Round(3)}\t");
            Console.Write($"{alternative.InformativenessOfBad.Round(3)}\t");
            Console.Write($"{alternative.Informativeness.Round(3)}\t\n");
        }
    }
}