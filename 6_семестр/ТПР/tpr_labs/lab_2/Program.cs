using Core;
using UI;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var criterias = ConsoleUIHelper.GetCriteriasByUserData();

        IAllTheoryAlternativesGetable criteriaService = new CriteriaService(criterias);
        var alternatives = await criteriaService.GetAllTheoryAlternativesAsync();
        ConsoleUIHelper.WriteEnumerableToConsole(alternatives);

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();
    }
}