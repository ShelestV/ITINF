using IrisML.Builders;
using IrisML.Models;
using System;

namespace IrisML.Converters
{
	static class IrisConverter
	{
		private readonly static string splitSymbol = ",";

		public static Iris ConvertFromString(string irisString)
		{
			var attributes = irisString.Split(splitSymbol);
			if (attributes.Length == 5)
			{
				var builder = new IrisBuilder();

				try
				{
					builder.BuildSepalLength(double.Parse(attributes[0].Replace('.', ',')));
					builder.BuildSepalWidth(double.Parse(attributes[1].Replace('.', ',')));
					builder.BuildPetalLength(double.Parse(attributes[2].Replace('.', ',')));
					builder.BuildPetalWidth(double.Parse(attributes[3].Replace('.', ',')));
					builder.BuildName(attributes[4]);
				}
				catch (FormatException e)
				{
					Console.WriteLine(e.Message);
					Console.ReadKey();
				}

				return builder.GetIris();
			}

			throw new ArgumentException();
		}
	}
}
