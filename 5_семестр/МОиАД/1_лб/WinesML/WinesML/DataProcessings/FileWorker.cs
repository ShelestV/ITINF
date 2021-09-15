using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinesML.Collections;
using WinesML.DataProcessings.Adstract;
using WinesML.Models;
using WinesML.Services;
using WinesML.Services.Adstract;

namespace WinesML.DataProcessings
{
	public class FileWorker : IWineFileWorker
	{
		private const string SPLIT_SYMBOL = "\n";

		private const string DATA_FILENAME = "wine.data";
		private const string NAMES_FILENAME = "names.txt";

		public async Task<Wines> GetWinesAsync()
		{
			var reader = new StreamReader(DATA_FILENAME);
			string data = await reader.ReadToEndAsync();
			return new Wines(data);
		}

		public async Task WriteNamesAsync(Dictionary<int, string> names)
		{
			var writer = new StreamWriter(NAMES_FILENAME);
			var namesBuilder = new StringBuilder();
			names.Select(name => namesBuilder.Append(name.Key).Append("-").Append(name.Value).Append("\n"));
			await writer.WriteAsync(namesBuilder.ToString());
		}

		public async Task<List<WineType>> GetWineTypesAsync()
		{
			var reader = new StreamReader(NAMES_FILENAME);
			string data = await reader.ReadToEndAsync();

			var result = new List<WineType>();

			var splitedData = data.Split(SPLIT_SYMBOL);
			IWineTypeParser wineTypeService = new WineTypeService();
			foreach (var part in splitedData)
				result.Add(wineTypeService.ParseFromString(part));

			return result;
		}
	}
}
