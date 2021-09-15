using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WinesML.Collections;
using WinesML.DataProcessings;
using WinesML.DataProcessings.Adstract;
using WinesML.Models;
using WPF.Commands;

namespace WPF.ViewModels
{
	class MainVM : Abstract.BaseVM
	{
		private Wines wines;
		private List<WineType> wineNames;

		private bool isLoadedData = false;

		public ObservableCollection<Wine> Wines { get; set; }
			= new ObservableCollection<Wine>();
		public ObservableCollection<WineType> WineNames { get; set; }
			= new ObservableCollection<WineType>();

		public ICommand LoadDataCommand { get; }

		public MainVM()
		{
			LoadDataCommand = new RelayCommand(execute: async () => await LoadData());
		}

		private async Task LoadData()
		{
			if (!isLoadedData)
			{
				IWineFileWorker fileWorker = new FileWorker();
				
				wines = await fileWorker.GetWinesAsync();
				foreach (var wine in wines)
					Wines.Add(wine);

				wineNames = await fileWorker.GetWineTypesAsync();
				foreach (var wineName in wineNames)
					WineNames.Add(wineName);

				isLoadedData = true;
			}
		}
	}
}
