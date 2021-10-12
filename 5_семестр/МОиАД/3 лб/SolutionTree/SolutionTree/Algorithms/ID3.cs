using SolutionTree.Models;
using System.Collections.Generic;

namespace SolutionTree.Algorithms
{
	class ID3
	{
		private readonly ICollection<TennisGame> games;

		public ID3(ICollection<TennisGame> games)
		{
			this.games = games;
		}

		public Trees.SolutionTree GetSolutionTree()
		{

		}

		private double CalculateInformationGain()
		{

		}

		private double CalculateEntropy()
		{

		}
	}
}
