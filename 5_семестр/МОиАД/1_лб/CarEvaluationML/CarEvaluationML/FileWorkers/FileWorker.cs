using CarEvaluationML.Converters;
using CarEvaluationML.Models;
using System.Collections.Generic;
using System.IO;

namespace CarEvaluationML.FileWorkers
{
	class FileWorker
	{
		private readonly string filename;
		private readonly string splitSymbol = "\n";

		public FileWorker(string filename = "car.data")
		{
			this.filename = filename;
		}

		public IEnumerable<Car> GetCars()
		{
			var resultCars = new List<Car>();

			using (var reader = new StreamReader(filename))
			{
				var text = reader.ReadToEnd();
				var rows = text.Split(splitSymbol);

				foreach (var row in rows)
				{
					if (!string.IsNullOrEmpty(row))
					{
						resultCars.Add(CarConverter.ConvertFromString(row));
					}
				}
			}

			return resultCars;
		}
	}
}
