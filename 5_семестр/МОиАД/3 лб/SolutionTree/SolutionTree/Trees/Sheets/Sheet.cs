using System.Collections;
using System.Collections.Generic;

namespace SolutionTree.Trees.Sheets
{
	internal class Sheet
	{
		public string AttributeName { get; }
		public object AttributeValue { get; }
		public int AmountOfPositive { get; private set; }
		public int AmountOfNegative { get; private set; }
		
		public ICollection<Sheet> Sheets { get; }

		public Sheet(string attributeName, object attributeValue, ICollection<Sheet> sheets = null)
		{
			AttributeName = attributeName;
			AttributeValue = attributeValue;
			AmountOfPositive = 0;
			AmountOfNegative = 0;

			Sheets = sheets;
		}

		public void SetPositivity(bool isPositive)
		{
			if (isPositive)
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
