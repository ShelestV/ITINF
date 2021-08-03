using System;
using System.Windows;

namespace TransportProblem.Rools
{
	class Rool
	{
		public static bool IsCorrectUnsignedInt(string str, out int number)
		{
			if (!(IsCorrectInt(str, out number) && number > 0))
			{
				MessageBox.Show("Uncorrect value!" + Environment.NewLine + "Value must be numeric and be greater than zero!", 
					"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			return true;
		}

		public static bool IsCorrectInt(string str, out int number)
		{
			if (!Int32.TryParse(str, out number))
			{
				MessageBox.Show("Uncorrect value!" + Environment.NewLine + "Value must be numeric!",
					"Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			return true;
		}
	}
}
