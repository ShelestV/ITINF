using ForestFiresML.Converters;
using ForestFiresML.Models;
using System.Collections.Generic;
using System.IO;

namespace ForestFiresML.FileWorkers
{
	class FileWorker
	{
		private readonly string filename;
		private readonly string splitSymbol = "\n";

		public FileWorker(string filename = "forestfires.csv")
		{
			this.filename = filename;
		}

		public IEnumerable<ForestFire> GetForestFires()
		{
			var resultForestFires = new List<ForestFire>();

			using (var reader = new StreamReader(filename))
			{
				var text = reader.ReadToEnd();
				var rows = text.Split(splitSymbol);

				foreach (var row in rows)
				{
					resultForestFires.Add(ForestFireConverter.ConvertFromString(row));
				}
			}

			return resultForestFires;
		}
	}
}
