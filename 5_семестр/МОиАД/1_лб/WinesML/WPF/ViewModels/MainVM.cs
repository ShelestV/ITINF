using System.Collections.ObjectModel;
using WinesML.Collections;
using WinesML.DataProcessings;
using WinesML.DataProcessings.Adstract;
using WinesML.Models;

namespace WPF.ViewModels
{
	class MainVM : Abstract.BaseVM
	{
		private Wines wines;

		public ObservableCollection<Wine> Wines { get; set; }
			= new ObservableCollection<Wine>();

		public MainVM()
		{
			IWineFileWorker fileWorker = new FileWorker();
			wines = fileWorker.GetWines();

			foreach (var wine in wines)
				Wines.Add(wine);
		}
	}
}
