using ForestFiresML.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestFiresML
{
	class UI
	{
		private ForestFires forestFires;

		public bool IsWorking { get; private set; } = false;

		public void Start()
		{
			IsWorking = true;

			var fileWorker = new FileWorkers.FileWorker();
			forestFires = new ForestFires(fileWorker.GetForestFires());
		}

		public void Communicate()
		{
			string userInput = "";

			#region User communication
			Console.WriteLine();
			Console.WriteLine("Choose operation: ");
			Console.WriteLine("1. Get element info.");
			Console.WriteLine("2. Shuffle.");
			Console.WriteLine("3. Show list of forestfires.");
			Console.WriteLine("4. Calculate statistic characteristics.");
			Console.WriteLine("5. Change id to name.");
			Console.WriteLine("6. Clear console.");
			Console.WriteLine("0. Exit.");

			userInput = Console.ReadLine();
			#endregion

			switch (userInput)
			{
				case "1":
					GetElementInfo();
				case "2":
					Shuffle();
				case "3":
					ShowForestFires();
				case "4":
					CalculateStatistics();
				case "5":
				case "6":
				case "0":
					IsWorking = false;
				default:

			}
		}

		private void GetElementInfo()
		{
			Console.WriteLine("Enter id: ");
			string userInput = Console.ReadLine();
			int id;
			if (int.TryParse(userInput, out id))
			{
				Console.WriteLine(forestFires.FindByIdOrDefault(id));
			}
			else
			{
				Console.WriteLine("Next time enter correct value!");
			}
		}

		private void Shuffle()
		{
			forestFires.Shuffle();
			Console.WriteLine("Forestfires has been shuffled. To see result choose \"3\"");
		}

		private void ShowForestFires()
		{ 
			foreach (var forestFire in forestFires)
			{
				Console.WriteLine(forestFire);
			}
		}

		private void CalculateStatistics()
		{

		}
	}
}
