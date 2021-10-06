using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Converters
{
	static class LugBootConverter
	{
		public static LugBoot ConvertFromString(string text)
		{
			switch (text)
			{
				case "small":
					return LugBoot.Small;
				case "med":
					return LugBoot.Med;
				case "big":
					return LugBoot.Big;
				default:
					throw new ArgumentException();
			}
		}
	}
}
