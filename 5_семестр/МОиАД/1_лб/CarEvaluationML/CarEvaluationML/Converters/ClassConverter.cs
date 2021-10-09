using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Converters
{
	static class ClassConverter
	{
		public static Class ConvertToString(string text)
		{
			switch (text)
			{
				case "unacc":
					return Class.Unacc;
				case "acc":
					return Class.Acc;
				case "good":
					return Class.Good;
				case "vgood":
					return Class.VGood;
				default: 
					throw new ArgumentException();
			}
		}
	}
}
