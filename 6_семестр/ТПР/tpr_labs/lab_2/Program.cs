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

        var service = new ClassAlternativeService(alternatives);

        do
        {
            var altWithMaxInfo = service.DoIteration();
            Console.WriteLine("Alternative\tGroup\t\td1\td2\tp1\tp2\tg1\tg2\tF1\tF2\tF");
            foreach (var alternative in service.Collection)
            {
                Console.Write($"{alternative.Name}\t\t");
                Console.Write($"{alternative.Group}\t");
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
            var group = ConsoleUIHelper.GetAlternativeGroupFromConsole("Enter alternative group (1 good, 2 bad): ");
            service.UpdateAlternativesGroup(altWithMaxInfo, group);
        } while (service.CanDoIteration);
    }
}