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

			var result = gamesService.Games.Games.GroupBy(x => x.GetAttributeValueByName(attributeName));
			foreach (var game in result)
			{
				sheets.Add(new Sheet(attributeName, game.Key, game.ToList()));
			}
			
		}
	}
}
