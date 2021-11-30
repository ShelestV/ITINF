using System;

namespace ForestFiresML
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			
			var ui = new UI();
			ui.Start();

			while (ui.IsWorking)
			{
				ui.Communicate();
			}
		}
	}
}
