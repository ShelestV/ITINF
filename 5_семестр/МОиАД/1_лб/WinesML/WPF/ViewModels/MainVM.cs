using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WinesML.Collections;
using WinesML.DataProcessings;
using WinesML.DataProcessings.Adstract;
using WinesML.Models;
using WPF.Commands;
using WPF.Models;

namespace WPF.ViewModels
{
	class MainVM : Abstract.BaseVM
	{
		private Wines wines;
		private Wines shuffleWines;
		private List<WineType> wineTypes;

		private Wine selectedWine;

		public ObservableCollection<MathWinesModel> MathModels { get; set; }
			= new ObservableCollection<MathWinesModel>();

		private bool isLoadedData = false;

		public ObservableCollection<Wine> Wines { get; set; }
			= new ObservableCollection<Wine>();

		public Wine SelectedWine 
		{ 
			get => selectedWine;
			set
			{
				selectedWine = value;
				OnPropertyChanged(nameof(SelectedWine));

				if (value is not null)
				{
					SelectedWineTypeName = selectedWine.Type.Name ?? "";
					OnPropertyChanged(nameof(SelectedWineTypeName));
				}
			}
		}
		
		public string SelectedWineTypeName { get; set; }
		
		public ICommand LoadDataCommand { get; }
		public ICommand ShuffleCommand { get; }
		public ICommand ToFirstSequenceCommand { get; }
		public ICommand SaveTypeNameCommand { get; }

		public MainVM()
		{
			LoadDataCommand = new RelayCommand(execute: async () => await LoadData());
			ShuffleCommand = new RelayCommand(execute: Shuffle);
			ToFirstSequenceCommand = new RelayCommand(execute: ToFirstSequence);
			SaveTypeNameCommand = new RelayCommand(execute: async () => await SaveTypeName(),
												   canExecute: IsSelectedWine);
		}

		private async Task LoadData()
		{
			if (!isLoadedData)
			{
				IWineFileWorker fileWorker = new FileWorker();
				
				// Load wines list
				wines = await fileWorker.GetWinesAsync();
				shuffleWines = wines.GetCopy();
				foreach (var wine in wines)
					Wines.Add(wine);

				// Load names for wine types
				wineTypes = await fileWorker.GetWineTypesAsync();
				foreach (var wine in wines)
				{
					foreach (var wineType in wineTypes)
					{
						if (wine.TypeId == wineType.Id)
						{
							wine.Type = wineType;
						}
					}
				}

				CalculateMathModels();

				isLoadedData = true;
			}
		}

		private void CalculateMathModels()
		{
			MathModels.Add(new MathWinesModel("Alcohol", wines.Select(wine => wine.Alcohol)));
			MathModels.Add(new MathWinesModel("Malic acid", wines.Select(wine => wine.MalicAcid)));
			MathModels.Add(new MathWinesModel("Ash", wines.Select(wine => wine.Ash)));
			MathModels.Add(new MathWinesModel("Alcalinity of ash", wines.Select(wine => wine.AlcanlinityOfAsh)));
			MathModels.Add(new MathWinesModel("Magnesium", wines.Select(wine => (double)wine.Magnesium)));
			MathModels.Add(new MathWinesModel("Total phenols", wines.Select(wine => wine.TotalPhenols)));
			MathModels.Add(new MathWinesModel("Flavanoids", wines.Select(wine => wine.Flavanoids)));
			MathModels.Add(new MathWinesModel("Nonflavanoid phenols", wines.Select(wine => wine.NonflavanoidPhenols)));
			MathModels.Add(new MathWinesModel("Proanthocyanins", wines.Select(wine => wine.Proanthocyanins)));
			MathModels.Add(new MathWinesModel("Color intensity", wines.Select(wine => wine.ColorIntensity)));
			MathModels.Add(new MathWinesModel("Hue", wines.Select(wine => wine.Hue)));
			MathModels.Add(new MathWinesModel("OD280/OD315 of diluted wines", wines.Select(wine => wine.DeterminingTheProteinConcentration)));
			MathModels.Add(new MathWinesModel("Proline", wines.Select(wine => (double)wine.Proline)));
			OnPropertyChanged(nameof(MathModels));
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

		private bool IsSelectedWine()
		{
			return selectedWine is not null;
		}

		private async Task SaveTypeName()
		{
			foreach (var wine in wines)
			{
				if (wine.TypeId == selectedWine.TypeId)
				{
					wine.Type.Name = SelectedWineTypeName;
				}
			}
			OnPropertyChanged(nameof(Wines));

			// Check on existing 
			bool isNew = true;
			foreach (var wineType in wineTypes)
			{
				if (wineType.EqualsById(selectedWine.TypeId))
				{
					wineType.Name = SelectedWineTypeName;
					isNew = false;
				}
			}

			// Create new if wine type does not exist
			if (isNew)
			{
				wineTypes.Add(new WineType() 
				{ 
					Id = selectedWine.TypeId, 
					Name = SelectedWineTypeName 
				});
			}

			var dict = new Dictionary<int, string>();
			foreach (var wineType in wineTypes)
			{
				dict.Add(wineType.Id, wineType.Name);
			}

			IWineFileWorker fileWorker = new FileWorker();
			await fileWorker.WriteNamesAsync(dict);
		}
	}
}
