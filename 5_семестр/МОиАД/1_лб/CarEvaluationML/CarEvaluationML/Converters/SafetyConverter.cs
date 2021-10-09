using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Converters
{
	static class SafetyConverter
	{
		public static Safety ConvertFromString(string text)
		{
			switch (text)
			{
				case "low":
					return Safety.Low;
				case "med":
					return Safety.Med;
				case "high":
					return Safety.High;
				default:
					throw new ArgumentException();
			}
		}
	}
}
