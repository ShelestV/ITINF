using System;

namespace CarEvaluationML.Converters
{
	static class NumberOfPersonsConverter
	{
		public static int ConvertFromString(string text)
		{
			int result;

			if (int.TryParse(text, out result) && 2 <= result && result != 3)
			{
				return result;
			}
			else if (text.Equals("more"))
			{
				return 5;
			}
			throw new ArgumentException();
		}
	}
}
