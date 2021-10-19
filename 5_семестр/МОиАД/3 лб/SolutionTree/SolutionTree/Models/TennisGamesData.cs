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
	}
}
