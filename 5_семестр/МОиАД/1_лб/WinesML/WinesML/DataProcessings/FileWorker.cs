using System.IO;
using WinesML.Collections;
using WinesML.DataProcessings.Adstract;

namespace WinesML.DataProcessings
{
	public class FileWorker : IWineFileWorker
	{
		private string filename = "wine.data";

		public Wines GetWines()
		{
			var reader = new StreamReader(filename);
			string data = reader.ReadToEnd();
			return new Wines(data);
		}
	}
}
