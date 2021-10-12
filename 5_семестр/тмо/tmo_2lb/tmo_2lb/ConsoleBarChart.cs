using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tmo_2lb
{
	class ConsoleBarChart
	{
		private List<int> data;

		public ConsoleBarChart(List<int> data) => this.data = data;

		public void Output()
		{
			Console.WriteLine("^");
			Console.WriteLine("|");

			for (int value = data.Max(); value > 0; --value)
			{
				Console.Write("|");
				foreach (int number in data)
					Console.Write(number >= value ? " O " : "   ");
				Console.WriteLine();
			}
			
			for (int i = 0; i < data.Count; ++i)
				Console.Write("---");
			Console.WriteLine(">");
			Console.Write("|");
			for (int i = 1; i <= data.Count; ++i)
				Console.Write(i > 0 && i < 10 ? " " + i + " " : i < 100 ? " " + i : i.ToString());
			Console.WriteLine();
		}
	}
}
