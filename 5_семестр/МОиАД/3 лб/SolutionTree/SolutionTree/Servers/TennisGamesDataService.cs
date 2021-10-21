using System.Collections.Generic;
using SolutionTree.Models;

namespace SolutionTree.Servers
{
	internal class TennisGamesDataService
	{
		private TennisGamesData data;
		
		public TennisGamesData Data => data;
		public ICollection<TennisGame> Games => data.Games;

		public TennisGamesDataService(TennisGamesData data)
		{
			this.data = data;
		}


		public ICollection<object> GetAttributeValues(string attributeName)
		{
			var resultList = new List<object>();			
			foreach (var game in data.Games)
			{
				resultList.Add(game.GetAttributeValueByName(attributeName));
			}
			return resultList;
		}

		public IEnumerable<string> GetAttributeNames()
		{
			return TennisGame.GetAttributeNames();
		}

		public int GetAmountOfRows()
		{
			return data.Games.Count;
		}
		
		public int GetAmountOfPositiveRows()
		{
			return GetAmountByPositivity(true);
		}

		public int GetAmountOfNegativeRows()
		{
			return GetAmountByPositivity(false);
		}

		private int GetAmountByPositivity(bool positivity)
		{
			int result = 0;

			foreach (var game in data.Games)
			{
				if (game.PlayTennis == positivity)
				{
					++result;
				}
			}

			return result;
		}

		public int GetAmountOfPositiveRowsForAttribute(string attributeName, object attributeValue)
		{
			return GetAmountOfRowsByPositivityForAttribute(attributeName, attributeValue, true);
		}

		public int GetAmountOfNegativeRowsForAttribute(string attributeName, object attributeValue)
		{
			return GetAmountOfRowsByPositivityForAttribute(attributeName, attributeValue, false);
		}

		private int GetAmountOfRowsByPositivityForAttribute(string attributeName, object attributeValue, bool positivity)
		{
			int result = 0;
			
			foreach (var game in data.Games)
			{
				if (attributeValue.Equals(game.GetAttributeValueByName(attributeName)) && 
				    game.PlayTennis == positivity)
				{
					++result;
				}
			}

			return result;
		}

		public int GetTotalAmountOfRowsForAttribute(string attributeName, object attributeValue)
		{
			int result = 0;
			
			foreach (var game in data.Games)
			{
				if (attributeValue.Equals(game.GetAttributeValueByName(attributeName)))
				{
					++result;
				}
			}

			return result;
		}
	}
}
