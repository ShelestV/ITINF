using WinesML.Models;

namespace WinesML.Services.Adstract
{
	interface IWineTypeParser
	{
		WineType ParseFromString(string str);
	}
}
