using ForestFiresML.Builders;
using ForestFiresML.Models;
using System;

namespace ForestFiresML.Converters
{
	class ForestFireConverter
	{
		private readonly static string splitSymbol = ",";

		public static ForestFire ConvertFromString(string forestFire)
		{
			var attributes = forestFire.Split(splitSymbol);
			if (attributes.Length == 13)
			{
				var builder = new ForestFireBuilder();
				builder.BuildX(int.Parse(attributes[0]));
				builder.BuildY(int.Parse(attributes[1]));
				builder.BuildMonth(MonthConverter.ConvertFromString(attributes[2]));
				builder.BuildDay(DayConverter.ConvertFromString(attributes[3]));
				builder.BuildFFMC(double.Parse(attributes[4]));
				builder.BuildDMC(double.Parse(attributes[5]));
				builder.BuildDC(double.Parse(attributes[6]));
				builder.BuildISI(double.Parse(attributes[7]));
				builder.BuildTemp(double.Parse(attributes[8]));
				builder.BuildRH(double.Parse(attributes[9]));
				builder.BuildWind(double.Parse(attributes[10]));
				builder.BuildRain(double.Parse(attributes[11]));
				builder.BuildArea(double.Parse(attributes[12]));

				return builder.GetForestFire();
			}
			throw new ArgumentException("It`s impossible to convert from this string");
		}
	}
}
