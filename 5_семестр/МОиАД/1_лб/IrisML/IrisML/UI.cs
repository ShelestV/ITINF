using IrisML.Collections;
using System;
using System.Linq;

namespace IrisML
{
	class UI
	{
		private Irises irises;

		public bool IsWorking { get; private set; } = false;

		public void Start()
		{
			IsWorking = true;

			var fileWorker = new FileWorkers.FileWorker();
			irises = new Irises(fileWorker.GetIrises());
		}

		public void Communicate()
		{
			string userInput;

			#region User communication
			Console.WriteLine();
			Console.WriteLine("Выберите действие: ");
			Console.WriteLine("1. Получить информацию о элементе.");
			Console.WriteLine("2. Перемешать.");
			Console.WriteLine("3. Показать все записи об ирисах.");
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
					ShowIrises();
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
				Console.WriteLine(irises.FindByIdOrDefault(id));
			}
			else
			{
				Console.WriteLine("В следующий раз введите корректное значение!");
			}
		}

		private void Shuffle()
		{
			irises.Shuffle();
			Console.WriteLine("Записи об ирисах были перемешаны. Чтобы увидеть результат выберите \"3\"");
		}

		private void ShowIrises()
		{ 
			foreach (var iris in irises)
			{
				Console.WriteLine(iris);
			}
		}

		private void CalculateStatistics()
		{
			Console.WriteLine("Величины\tМатематическое ожидание\tСреднее квадратичное отклонение\tДисперсия");
			#region Output
			#region Sepal length
			Console.Write("Sepal length\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(irises.Select(iris => iris.SepalLength)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(irises.Select(iris => iris.SepalLength)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(irises.Select(iris => iris.SepalLength)), 3));
			#endregion
			#region Sepal width
			Console.Write("Sepal width\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(irises.Select(iris => iris.SepalWidth)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(irises.Select(iris => iris.SepalWidth)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(irises.Select(iris => iris.SepalWidth)), 3));
			#endregion
			#region Petal length
			Console.Write("Sepal length\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(irises.Select(iris => iris.PetalLength)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(irises.Select(iris => iris.PetalLength)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(irises.Select(iris => iris.PetalLength)), 3));
			#endregion
			#region Petal width
			Console.Write("Petal width\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(irises.Select(iris => iris.PetalWidth)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(irises.Select(iris => iris.PetalWidth)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(irises.Select(iris => iris.PetalWidth)), 3));
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
				var iris = irises.FindByIdOrDefault(id);
				Console.WriteLine(iris);
				Console.WriteLine("Введите название: ");
				string name = Console.ReadLine();
				iris.IdName = name;
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
