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
	}
}
