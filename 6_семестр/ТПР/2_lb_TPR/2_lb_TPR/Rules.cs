using System;

namespace _2_lb_TPR
{
	class Rules
	{
		public static bool isCorrectValue(string str, out int number)
		{
			bool result = false;
			if (Int32.TryParse(str, out number))
			{
				if (number > 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}

			if (!result) { Console.WriteLine("Введите корректное значение!"); }

			return result;
		}
	}
}
