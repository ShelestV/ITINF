using System.Collections.Generic;

namespace SolutionTree.Models
{
	class TennisGamesData
	{
		public ICollection<TennisGame> Games { get; }

		public TennisGamesData()
		{
			Games = new List<TennisGame>();
		}

		public ICollection<object> GetAttributeValues(string attributeName)
		{
			var resultList = new List<object>();			
			foreach (var game in Games)
			{
				resultList.Add(game.GetAttributeValueByName(attributeName));
			}
			return resultList;
		}
	}
}
