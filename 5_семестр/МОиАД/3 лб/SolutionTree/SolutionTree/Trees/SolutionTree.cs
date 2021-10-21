using System.Collections.Generic;
using System.Linq;
using SolutionTree.Models;
using SolutionTree.Servers;
using SolutionTree.Trees.Sheets;

namespace SolutionTree.Trees
{
	internal class SolutionTree
	{
		private string rootAttributeName;
		private TennisGamesDataService gamesService;
		private ICollection<Sheet> sheets;
	
		public SolutionTree(TennisGamesData games)
		{
			gamesService = new TennisGamesDataService(games);
			sheets = new List<Sheet>();
		}
 
		public void BuildByRoot(string attributeName)
		{
			rootAttributeName = attributeName;
			AddSheets();
			
			
		}

		private void AddSheets()
		{
			var result = gamesService.Games
				.GroupBy(game => game.GetAttributeValueByName(rootAttributeName))
				.Select(group => new
				{
					AttributeName = group.Key,
					Count = group.Count(),
					Games = group.Select(game => game)
				});
			
			foreach (var group in result)
			{
				var sheet = new Sheet(rootAttributeName, group.AttributeName);
				sheets.Add(sheet);
				sheet.CalculatePositivity(group.Games);
			}
		}
	}
}
