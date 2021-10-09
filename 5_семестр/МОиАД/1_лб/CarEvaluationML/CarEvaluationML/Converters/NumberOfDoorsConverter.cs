using System;

namespace CarEvaluationML.Converters
{
	static class NumberOfDoorsConverter
	{
		public static int ConvertFromString(string text)
		{
			int result;

			if (int.TryParse(text, out result) && 2 <= result)
			{
				return result;
			}
			else if (text.Equals("5more"))
			{
				return 5;
			}
			throw new ArgumentException();
		}
	}
}
