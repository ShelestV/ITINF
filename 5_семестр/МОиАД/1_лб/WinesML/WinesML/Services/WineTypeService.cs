using System.Collections.Generic;
using WinesML.Models;
using WinesML.Services.Adstract;

namespace WinesML.Services
{
	class WineTypeService : IWineTypeParser
	{
		private const char SPLIT_SYMBOL = '-';

		public WineType ParseFromString(string str)
		{
			var splitedStr = str.Split(SPLIT_SYMBOL);
			var result = new WineType
			{
				Id = int.Parse(splitedStr[0]),
				Name = splitedStr[1]
			};
			return result;
		}

		public KeyValuePair<int, string> ParseToDictionaryValue(WineType wineType)
		{
			return new KeyValuePair<int, string>(wineType.Id, wineType.Name);
		}
	}
}
