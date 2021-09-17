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

		public static bool IsCorrectStringFormat(string str)
		{
			var splited = str.Split(SPLIT_SYMBOL);
			int parseResult; // because Int32 has not method CanParse!!! (angry programmer)
			return splited.Length == 2 && int.TryParse(splited[0], out parseResult);
		}
	}
}
