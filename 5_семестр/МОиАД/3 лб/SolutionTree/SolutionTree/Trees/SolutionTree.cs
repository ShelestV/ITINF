using SolutionTree.Trees.Branches;
using System.Collections.Generic;

namespace SolutionTree.Trees
{
	class SolutionTree
	{
		private ICollection<Branch> branches;

		public SolutionTree()
		{
			branches = new List<Branch>();
		}
	}
}
