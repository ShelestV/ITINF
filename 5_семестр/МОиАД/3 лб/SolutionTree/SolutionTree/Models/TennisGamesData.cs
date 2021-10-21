using System.Collections.Generic;

namespace SolutionTree.Models
{
	internal class TennisGamesData
	{
		public ICollection<TennisGame> Games { get; }

		public TennisGamesData()
		{
			Games = new List<TennisGame>();
		}

		public TennisGamesData(IEnumerable<TennisGame> games)
		{
			Games = new List<TennisGame>();
			foreach (var game in games)
			{
				Games.Add(game);
			}
		}
	}
}
