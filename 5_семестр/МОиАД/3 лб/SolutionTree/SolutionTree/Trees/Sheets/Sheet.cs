using System.Collections;
using System.Collections.Generic;
using SolutionTree.Models;

namespace SolutionTree.Trees.Sheets
{
	internal class Sheet
	{
		public string AttributeName { get; }
		public object AttributeValue { get; }
		public int AmountOfPositive { get; private set; }
		public int AmountOfNegative { get; private set; }
		
		public ICollection<Sheet> Sheets { get; }

		public Sheet(string attributeName, object attributeValue)
		{
			AttributeName = attributeName;
			AttributeValue = attributeValue;
			AmountOfPositive = 0;
			AmountOfNegative = 0;

			Sheets = new List<Sheet>();
		}

		public void CalculatePositivity(IEnumerable<TennisGame> games)
		{
			foreach (var game in games)
			{
				if (game.PlayTennis)
				{
					++AmountOfPositive;
				}
				else
				{
					++AmountOfNegative;
				}
			}
		}
	}
}
