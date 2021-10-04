using IrisML.Converters;
using IrisML.Models;
using System.Collections.Generic;
using System.IO;

namespace IrisML.FileWorkers
{
	class FileWorker
	{
		private readonly string filename;
		private readonly string splitSymbol = "\n";

		public FileWorker(string filename = "iris.data")
		{
			this.filename = filename;
		}

		public IEnumerable<Iris> GetForestFires()
		{
			var resultForestFires = new List<Iris>();

			using (var reader = new StreamReader(filename))
			{
				var text = reader.ReadToEnd();
				var rows = text.Split(splitSymbol);

				foreach (var row in rows)
				{
					if (!string.IsNullOrEmpty(row))
					{
						resultForestFires.Add(IrisConverter.ConvertFromString(row));
					}
				}
			}

			return resultForestFires;
		}
	}
}
