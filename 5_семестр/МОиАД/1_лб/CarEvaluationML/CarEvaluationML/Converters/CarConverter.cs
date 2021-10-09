using CarEvaluationML.Builders;
using CarEvaluationML.Models;
using System;

namespace CarEvaluationML.Converters
{
	static class CarConverter
	{
		private readonly static char splitSymbol = ',';

		public static Car ConvertFromString(string text)
		{
			var attributes = text.Split(splitSymbol);

			if (attributes.Length == 7)
			{
				var builder = new CarBuilder();

				builder.BuildBuying(BuyingConverter.ConvertFromString(attributes[0]));
				builder.BuildMaint(MaintConverter.ConvertFromString(attributes[1]));
				builder.BuildNumberOfDoors(NumberOfDoorsConverter.ConvertFromString(attributes[2]));
				builder.BuildNumberOfPersons(NumberOfPersonsConverter.ConvertFromString(attributes[3]));
				builder.BuildLugBoot(LugBootConverter.ConvertFromString(attributes[4]));
				builder.BuildSafety(SafetyConverter.ConvertFromString(attributes[5]));
				builder.BuildClass(ClassConverter.ConvertToString(attributes[6]));

				return builder.GetCar();
			}

			throw new ArgumentException();
		}
	}
}
