using CarEvaluationML.Collections;
using System;
using System.Linq;

namespace CarEvaluationML
{
	class UI
	{
		private Cars cars;

		public bool IsWorking { get; private set; } = false;

		public void Start()
		{
			IsWorking = true;

			var fileWorker = new FileWorkers.FileWorker();
			cars = new Cars(fileWorker.GetCars());
		}

		public void Communicate()
		{
			string userInput;

			#region User communication
			Console.WriteLine();
			Console.WriteLine("Выберите действие: ");
			Console.WriteLine("1. Получить информацию о элементе.");
			Console.WriteLine("2. Перемешать.");
			Console.WriteLine("3. Показать все записи о машинах.");
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
					ShowCars();
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
				Console.WriteLine(cars.FindByIdOrDefault(id));
			}
			else
			{
				Console.WriteLine("В следующий раз введите корректное значение!");
			}
		}

		private void Shuffle()
		{
			cars.Shuffle();
			Console.WriteLine("Записи о машинах были перемешаны. Чтобы увидеть результат выберите \"3\"");
		}

		private void ShowCars()
		{ 
			foreach (var cars in cars)
			{
				Console.WriteLine(cars);
			}
		}

		private void CalculateStatistics()
		{
			Console.WriteLine("Величины\tМатематическое ожидание\tСреднее квадратичное отклонение\tДисперсия");
			#region Output
			#region Buying
			Console.Write("Buying\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => (int)car.Buying)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => (int)car.Buying)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => (int)car.Buying)), 3));
			#endregion
			#region Maint
			Console.Write("Maint\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => (int)car.Maint)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => (int)car.Maint)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => (int)car.Maint)), 3));
			#endregion
			#region NumberOfDoors
			Console.Write("Doors\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => car.NumberOfDoors)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => car.NumberOfDoors)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => car.NumberOfDoors)), 3));
			#endregion
			#region NumberOfPersons
			Console.Write("Persons\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => car.NumberOfPersons)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => car.NumberOfPersons)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => car.NumberOfPersons)), 3));
			#endregion
			#region Lug boot
			Console.Write("Lug boot\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => (int)car.LugBoot)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => (int)car.LugBoot)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => (int)car.LugBoot)), 3));
			#endregion
			#region Safety
			Console.Write("Safety\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateExpectedValue(cars.Select(car => (int)car.Safety)), 3) + "\t\t\t");
			Console.Write(Math.Round(Mathematics.Statistics.CalculateRmsBiasFromMean(cars.Select(car => (int)car.Safety)), 3) + "\t\t\t\t");
			Console.WriteLine(Math.Round(Mathematics.Statistics.CalculateDispersion(cars.Select(car => (int)car.Safety)), 3));
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
				var iris = cars.FindByIdOrDefault(id);
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
