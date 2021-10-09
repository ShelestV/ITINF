using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Converters
{
	static class BuyingConverter
	{
		public static Buying ConvertFromString(string text)
		{
			switch (text)
			{
				case "vhigh":
					return Buying.VHigh;
				case "high":
					return Buying.High;
				case "med":
					return Buying.Med;
				case "low":
					return Buying.Low;
				default:
					throw new ArgumentException();
			}
		}
	}
}
