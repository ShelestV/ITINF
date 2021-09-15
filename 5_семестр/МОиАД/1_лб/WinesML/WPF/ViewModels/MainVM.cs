using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WinesML.Collections;
using WinesML.DataProcessings;
using WinesML.DataProcessings.Adstract;
using WinesML.Models;
using WinesML.Services;
using WinesML.Services.Abstract;
using WPF.Commands;

namespace WPF.ViewModels
{
	class MainVM : Abstract.BaseVM
	{
		private Wines wines;
		private Wines shuffleWines;
		private List<WineType> wineNames;

		private Wine selectedWine;

		private bool isLoadedData = false;

		public ObservableCollection<Wine> Wines { get; set; }
			= new ObservableCollection<Wine>();
		public ObservableCollection<WineType> WineNames { get; set; }
			= new ObservableCollection<WineType>();

		public Wine SelectedWine 
		{ 
			get => selectedWine;
			set
			{
				selectedWine = value;
				OnPropertyChanged(nameof(SelectedWineExpectedValue));
				OnPropertyChanged(nameof(SelectedWineRmsDiasFromMean));
				OnPropertyChanged(nameof(SelectedWineDispersion));
			}
		}
		public string SelectedWineExpectedValue 
		{
			get
			{
				if (selectedWine != null)
				{
					IWineService service = new WineService(selectedWine);
					return service.CalculateExpectedValue().ToString();
				}
				return "";
			}
		}
		public string SelectedWineRmsDiasFromMean
		{
			get
			{
				if (selectedWine != null)
				{
					IWineService service = new WineService(selectedWine);
					return service.CalculateRmsBiasFromMean().ToString();
				}
				return "";
			}
		}
		public string SelectedWineDispersion 
		{
			get
			{
				if (selectedWine != null)
				{
					IWineService service = new WineService(selectedWine);
					return service.CalculateDispersion().ToString();
				}
				return "";
			}
		}

		public ICommand LoadDataCommand { get; }
		public ICommand ShuffleCommand { get; }
		public ICommand ToFirstSequenceCommand { get; }

		public MainVM()
		{
			LoadDataCommand = new RelayCommand(execute: async () => await LoadData());
			ShuffleCommand = new RelayCommand(execute: Shuffle);
			ToFirstSequenceCommand = new RelayCommand(execute: ToFirstSequence);
		}

		private async Task LoadData()
		{
			if (!isLoadedData)
			{
				IWineFileWorker fileWorker = new FileWorker();
				
				wines = await fileWorker.GetWinesAsync();
				shuffleWines = wines.GetCopy();
				foreach (var wine in wines)
					Wines.Add(wine);

				wineNames = await fileWorker.GetWineTypesAsync();
				foreach (var wineName in wineNames)
					WineNames.Add(wineName);

				isLoadedData = true;
			}
		}

		private void Shuffle()
		{
			if (isLoadedData)
			{
				shuffleWines.Shuffle();
				Wines.Clear();
				foreach (var wine in shuffleWines)
					Wines.Add(wine);
			}
		}

		private void ToFirstSequence()
		{
			if (isLoadedData)
			{
				Wines.Clear();
				foreach (var wine in wines)
					Wines.Add(wine);
			}
		}
	}
}
