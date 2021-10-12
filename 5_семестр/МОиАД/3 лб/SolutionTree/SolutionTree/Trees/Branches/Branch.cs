using SolutionTree.Trees.Branches.Sheets;
using System.Collections.Generic;
using System.Linq;

namespace SolutionTree.Trees.Branches
{
	class Branch
	{
		private ICollection<Sheet> sheets;

		public Branch()
		{
			sheets = new List<Sheet>();
		}

		public void AddSheet(string attributeName, object value, bool isPositive)
		{
			Sheet sheet;
			if (sheets.Any(s => s.AttributeName.Equals(attributeName)))
			{
				sheet = new Sheet(attributeName, value);
				sheets.Add(sheet);
			}
			else
			{
				sheet = sheets.FirstOrDefault(s => s.AttributeName.Equals(attributeName));
			}

			sheet.SetPositivity(isPositive);
		}
	}
}
