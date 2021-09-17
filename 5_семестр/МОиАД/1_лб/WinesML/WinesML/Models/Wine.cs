using System;
using WinesML.Models.Abstract;

namespace WinesML.Models
{
	public class Wine : BaseModel
	{
		private const string SPLIT_SYMBOL = ",";
		private WineType type;

		public Guid Id { get; }
		public int TypeId { get; private set; }
		public WineType Type 
		{ 
			get => type; 
			set 
			{ 
				TypeId = type.Id; 
				type = value;
				OnPropertyChanged(nameof(Type));
			} 
		}
		public double Alcohol { get; set; }
		public double MalicAcid { get; set; }
		public double Ash { get; set; }
		public double AlcanlinityOfAsh { get; set; }
		public int Magnesium { get; set; }
		public double TotalPhenols { get; set; }
		public double Flavanoids { get; set; }
		public double NonflavanoidPhenols { get; set; }
		public double Proanthocyanins { get; set; }
		public double ColorIntensity { get; set; }
		public double Hue { get; set; }
		public double DeterminingTheProteinConcentration { get; set; } // OD280/OD315 of diluted wines
		public int Proline { get; set; }

		public Wine(string data)
		{
			var attributeStrings = data.Split(SPLIT_SYMBOL);

			Id = Guid.NewGuid();
			type = new WineType() { Id = int.Parse(attributeStrings[0]) };
			TypeId = type.Id;
			Alcohol = double.Parse(attributeStrings[1].Replace('.', ','));
			MalicAcid = double.Parse(attributeStrings[2].Replace('.', ','));
			Ash = double.Parse(attributeStrings[3].Replace('.', ','));
			AlcanlinityOfAsh = double.Parse(attributeStrings[4].Replace('.', ','));
			Magnesium = int.Parse(attributeStrings[5]);
			TotalPhenols = double.Parse(attributeStrings[6].Replace('.', ','));
			Flavanoids = double.Parse(attributeStrings[7].Replace('.', ','));
			NonflavanoidPhenols = double.Parse(attributeStrings[8].Replace('.', ','));
			Proanthocyanins = double.Parse(attributeStrings[9].Replace('.', ','));
			ColorIntensity = double.Parse(attributeStrings[10].Replace('.', ','));
			Hue = double.Parse(attributeStrings[11].Replace('.', ','));
			DeterminingTheProteinConcentration = double.Parse(attributeStrings[12].Replace('.', ','));
			Proline = int.Parse(attributeStrings[13]);
		}

		public override bool Equals(object obj)
		{
			return obj is Wine wine &&
				   TypeId == wine.TypeId &&
				   Alcohol == wine.Alcohol &&
				   MalicAcid == wine.MalicAcid &&
				   Ash == wine.Ash &&
				   AlcanlinityOfAsh == wine.AlcanlinityOfAsh &&
				   Magnesium == wine.Magnesium &&
				   TotalPhenols == wine.TotalPhenols &&
				   Flavanoids == wine.Flavanoids &&
				   NonflavanoidPhenols == wine.NonflavanoidPhenols &&
				   Proanthocyanins == wine.Proanthocyanins &&
				   ColorIntensity == wine.ColorIntensity &&
				   Hue == wine.Hue &&
				   DeterminingTheProteinConcentration == wine.DeterminingTheProteinConcentration &&
				   Proline == wine.Proline;
		}

		public override int GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.Add(TypeId);
			hash.Add(Alcohol);
			hash.Add(MalicAcid);
			hash.Add(Ash);
			hash.Add(AlcanlinityOfAsh);
			hash.Add(Magnesium);
			hash.Add(TotalPhenols);
			hash.Add(Flavanoids);
			hash.Add(NonflavanoidPhenols);
			hash.Add(Proanthocyanins);
			hash.Add(ColorIntensity);
			hash.Add(Hue);
			hash.Add(DeterminingTheProteinConcentration);
			hash.Add(Proline);
			return hash.ToHashCode();
		}
	}
}
