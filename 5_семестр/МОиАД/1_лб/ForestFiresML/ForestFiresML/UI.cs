﻿using ForestFiresML.Collections;
using System;
using System.Linq;

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
			string userInput;

			#region User communication
			Console.WriteLine();
			Console.WriteLine("Выберите действие: ");
			Console.WriteLine("1. Получить информацию о элементе.");
			Console.WriteLine("2. Перемешать.");
			Console.WriteLine("3. Показать все записи о пожарах.");
			Console.WriteLine("4. Посчитать статистические характеристики.");
			Console.WriteLine("5. Поменять Id на название.");
			Console.WriteLine("6. Очистить консоль.");
			Console.WriteLine("0. Завершить.");

			userInput = Console.ReadLine();
			#endregion

			switch (userInput)
			{
				case "1":
					GetElementInfo();
					break;
				case "2":
					Shuffle();
					break;
				case "3":
					ShowForestFires();
					break;
				case "4":
					CalculateStatistics();
					break;
				case "5":
					ChangeIdToName();
					break;
				case "6":
					ClearConsole();
					break;
				case "0":
					Exit();
					break;
				default:
					DoNothing();
					break;
			}
		}

		private void GetElementInfo()
		{
			Console.WriteLine("Введите id: ");
			string userInput = Console.ReadLine();
			int id;
			if (int.TryParse(userInput, out id))
			{
				Console.WriteLine(forestFires.FindByIdOrDefault(id));
			}
			else
			{
				Console.WriteLine("В следующий раз введите корректное значение!");
			}
		}

		private void Shuffle()
		{
			forestFires.Shuffle();
			Console.WriteLine("Записи о лесных пожарах были перемешаны. Чтобы увидеть результат выберите \"3\"");
		}

		private void ShowForestFires()
		{ 
			foreach (var forestFire in forestFires)
			{
				Console.WriteLine(forestFire.ShortForm());
			}
		}

		private void CalculateStatistics()
		{
			Console.WriteLine("Величины\tМатематическое ожидание\tСреднее квадратичное отклонение\tДисперсия");
			#region Output
			#region X
			Console.Write("X\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => (double)fire.X)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => (double)fire.X)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => (double)fire.X)), 3) + "\t");
			#endregion
			#region Y
			Console.Write("Y\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => (double)fire.Y)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => (double)fire.Y)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => (double)fire.Y)), 3) + "\t");
			#endregion
			#region Month
			Console.Write("Month\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => (double)fire.Month)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => (double)fire.Month)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => (double)fire.Month)), 3) + "\t");
			#endregion
			#region Day
			Console.Write("Day\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => (double)fire.Day)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => (double)fire.Day)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => (double)fire.Day)), 3) + "\t");
			#endregion
			#region FFMC
			Console.Write("FFMC\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.FFMC)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.FFMC)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.FFMC)), 3) + "\t");
			#endregion
			#region DMC
			Console.Write("DMC\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.DMC)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.DMC)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.DMC)), 3) + "\t");
			#endregion
			#region DC
			Console.Write("DC\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.DC)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.DC)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.DC)), 3) + "\t");
			#endregion
			#region ISI
			Console.Write("ISI\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.ISI)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.ISI)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.ISI)), 3) + "\t");
			#endregion
			#region Temp
			Console.Write("Temp\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.Temp)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.Temp)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.Temp)), 3) + "\t");
			#endregion
			#region RH
			Console.Write("RH\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.RH)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.RH)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.RH)), 3) + "\t");
			#endregion
			#region Wind
			Console.Write("Wind\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.Wind)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.Wind)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.Wind)), 3) + "\t");
			#endregion
			#region Rain
			Console.Write("Rain\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.Rain)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.Rain)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.Rain)), 3) + "\t");
			#endregion
			#region Area
			Console.Write("Area\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(forestFires.Select(fire => fire.Area)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(forestFires.Select(fire => fire.Area)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(forestFires.Select(fire => fire.Area)), 3) + "\t");
			#endregion
			#endregion
		}

		private void ChangeIdToName()
		{
			Console.WriteLine("Введите id: ");
			string userInput = Console.ReadLine();
			int id;
			if (int.TryParse(userInput, out id))
			{
				var fire = forestFires.FindByIdOrDefault(id);
				Console.WriteLine(fire);
				Console.WriteLine("Введите название: ");
				string name = Console.ReadLine();
				fire.Name = name;
			}
			else
			{
				Console.WriteLine("В следующий раз введите корректное значение!");
			}
		}

		private void ClearConsole()
		{
			Console.Clear();
		}

		private void Exit()
		{
			IsWorking = false;
		}

		private void DoNothing()
		{
			
		}
	}
}
