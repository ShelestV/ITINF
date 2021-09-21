using ForestFiresML.Models;
using System;

namespace ForestFiresML.Converters
{
	class MonthConverter
	{
		public static Month ConvertFromString(string month)
		{
			switch (month)
			{
				case "jan":
					return Month.January;
				case "feb":
					return Month.February;
				case "mar":
					return Month.March;
				case "apr":
					return Month.April;
				case "may":
					return Month.May;
				case "jun":
					return Month.June;
				case "jul":
					return Month.July;
				case "aug":
					return Month.August;
				case "sep":
					return Month.September;
				case "oct":
					return Month.October;
				case "nov":
					return Month.November;
				case "dec":
					return Month.December;
				default:
					throw new ArgumentException("It`s impossible to convert this string");
			}
		}
	}
}
