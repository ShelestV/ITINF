using System.Collections.Generic;
using System.Threading.Tasks;
using WinesML.Collections;
using WinesML.Models;

namespace WinesML.DataProcessings.Adstract
{
	public interface IWineFileWorker 
	{
		Task<Wines> GetWinesAsync();
		Task WriteNamesAsync(Dictionary<int, string> names);
		Task<List<WineType>> GetWineTypesAsync();
	}
}
