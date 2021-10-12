namespace SolutionTree.Trees.Branches.Sheets
{
	class Sheet
	{
		public string AttributeName { get; set; }
		public object AttributeValue { get; set; }
		public int NumberOfPositive { get; private set; }
		public int NumberOfNegative { get; private set; }

		public Sheet()
		{
			NumberOfPositive = 0;
			NumberOfNegative = 0;
		}

		public Sheet(string attributeName, object attributeValue) : this()
		{
			AttributeName = attributeName;
			AttributeValue = attributeValue;
		}

		public void SetPositivity(bool isPositive)
		{
			if (isPositive)
			{
				++NumberOfPositive;
			}
			else
			{
				++NumberOfNegative;
			}
		}
	}
}
