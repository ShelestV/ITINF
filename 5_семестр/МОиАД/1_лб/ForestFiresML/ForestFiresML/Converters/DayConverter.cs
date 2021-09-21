using ForestFiresML.Models;
using System;

namespace ForestFiresML.Converters
{
	class DayConverter
	{
		public static Day ConvertFromString(string day)
		{
			switch (day)
			{
				case "mon":
					return Day.Monday;
				case "tue":
					return Day.Tuesday;
				case "wed":
					return Day.Wednesday;
				case "thu":
					return Day.Thursday;
				case "fri":
					return Day.Friday;
				case "sut":
					return Day.Saturday;
				case "sun":
					return Day.Sunday;
				default:
					throw new ArgumentException("It`s impossible to convert this string");
			}
		}
	}
}
