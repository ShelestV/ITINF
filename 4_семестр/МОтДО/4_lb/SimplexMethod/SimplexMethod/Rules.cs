using System;
using System.Collections.Generic;

namespace SimplexMethod
{
	class Rules
	{
		public static bool IsCorrectUnsignedInt(string str, out int number)
		{
			return IsCorrectInt(str, out number) && number > 0;
		}

		public static bool IsCorrectInt(string str, out int number)
		{
			return Int32.TryParse(str, out number);
		}

		public static bool IsAllEquations(List<Expression> expression)
		{
			foreach (var equation in expression)
			{
				if (equation.Sign != Sign.Equal)
				{
					return false;
				}
			}
			return true;
		}
	} 
}
