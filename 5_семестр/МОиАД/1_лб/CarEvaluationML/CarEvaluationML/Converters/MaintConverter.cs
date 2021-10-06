using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Converters
{
	static class MaintConverter
	{
		public static Maint ConvertFromString(string text)
		{
			switch (text)
			{
				case "vhigh":
					return Maint.VHigh;
				case "high":
					return Maint.High;
				case "med":
					return Maint.Med;
				case "low":
					return Maint.Low;
				default:
					throw new ArgumentException();
			}
		}
	}
}
