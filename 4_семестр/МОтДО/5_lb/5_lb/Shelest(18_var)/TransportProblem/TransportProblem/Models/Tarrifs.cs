using System;
using System.Collections.Generic;
using System.Text;

namespace TransportProblem.Models
{
	class Tarrifs
	{
		private Tarrif[][] tarrifs;
		private int numberOfOccupied;
		private bool isDegeneratePlan;

		private int originalNumberOfRows;
		private int originalNumberOfColumns;

		public int TeoreticNumberOfOccupied { get => NumberOfColumns + NumberOfRows - 1; }
		public int NumberOfOccupied { get => numberOfOccupied; set => numberOfOccupied = value; }
		public int NumberOfRows { get => tarrifs.Length; }
		public int NumberOfColumns { get => tarrifs[0].Length; }

		public Tarrif this[int row, int column]
		{
			get => tarrifs[row][column];
			set => tarrifs[row][column] = new Tarrif(value);
		}

		public Tarrifs(int numberOfRows, int numberOfColumns)
		{
			if (numberOfRows <= 0 || numberOfColumns <= 0)
				throw new FormatException();

			originalNumberOfRows = numberOfRows;
			originalNumberOfColumns = numberOfColumns;

			tarrifs = new Tarrif[numberOfRows][];
			for (int i = 0; i < numberOfRows; ++i)
				tarrifs[i] = new Tarrif[numberOfColumns];

			numberOfOccupied = 0;
			isDegeneratePlan = true;
		}

		public Tarrifs(Tarrifs other)
		{
			this.originalNumberOfRows = other.originalNumberOfRows;
			this.originalNumberOfColumns = other.originalNumberOfColumns;

			tarrifs = new Tarrif[other.NumberOfRows][];
			for (int i = 0; i < other.NumberOfRows; ++i)
				tarrifs[i] = new Tarrif[other.NumberOfColumns];

			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					tarrifs[i][j] = new Tarrif(other[i, j]);
				}
			}

			numberOfOccupied = other.numberOfOccupied;
			isDegeneratePlan = other.isDegeneratePlan;
		}

		public void AddStockBasis()
		{
			var newTarrifs = GetTarrifsCopy();
			var newDemand = new List<Tarrif>();
			for (int j = 0; j < NumberOfColumns; ++j)
				newDemand.Add(new Tarrif(0, NumberOfRows, j));
			newTarrifs.Add(newDemand);

			NewTarrifs(newTarrifs);
		}

		public void AddDemandBasis()
		{
			var newTarrifs = GetTarrifsCopy();
			for (int i = 0; i < NumberOfRows; ++i)
				newTarrifs[i].Add(new Tarrif(0, i, NumberOfColumns));

			NewTarrifs(newTarrifs);
		}

		private List<List<Tarrif>> GetTarrifsCopy()
		{
			var copy = new List<List<Tarrif>>();
			for (int i = 0; i < NumberOfRows; ++i)
			{
				var inter = new List<Tarrif>();
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					inter.Add(this.tarrifs[i][j]);
				}
				copy.Add(inter);
			}
			return copy;
		}

		private void NewTarrifs(List<List<Tarrif>> tarrifs)
		{
			this.tarrifs = new Tarrif[tarrifs.Count][];
			for (int i = 0; i < tarrifs.Count; ++i)
				this.tarrifs[i] = new Tarrif[tarrifs[i].Count];

			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					this.tarrifs[i][j] = tarrifs[i][j];
				}
			}
		}

		public int GetTotalDemand(int column)
		{
			int total = 0;
			for (int i = 0; i < NumberOfRows; ++i)
				total += tarrifs[i][column].QuantityOfProduct;
			return total;
		}

		public int GetTotalStock(int row)
		{
			int total = 0;
			for (int j = 0; j < NumberOfColumns; ++j)
				total += tarrifs[row][j].QuantityOfProduct;
			return total;
		}

		public bool IsOptimal(U[] u, V[] v)
		{
			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					if (tarrifs[i][j].HasProduct)
						continue;

					if (u[i].Number + v[j].Number > tarrifs[i][j].Cost)
						return false;
				}
			}
			return true;
		}

		public string GetF()
		{
			var F = new StringBuilder("Minimum costs: F = ");
			var numericF = 0;
			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					if (tarrifs[i][j].HasProduct)
					{
						numericF += tarrifs[i][j].Cost * tarrifs[i][j].QuantityOfProduct;
						F.Append(tarrifs[i][j].Cost).
							Append("*").
							Append(tarrifs[i][j].QuantityOfProduct).
							Append((i == NumberOfRows - 1 && j == NumberOfColumns - 1) ? "" : " + ");
					}
				}
			}
			F.Append(" = ").Append(numericF);
			return F.ToString();
		}

		public string ToTransportProblemString()
		{
			var message = new StringBuilder("Must be sended :" + Environment.NewLine);
			for (int i = 0; i < originalNumberOfRows; ++i)
			{
				message.Append("From the ").Append(i + 1).Append(" warehouse to : ");
				for (int j = 0; j < originalNumberOfColumns; ++j)
				{
					if (tarrifs[i][j].HasProduct)
					{
						message.Append("the ").Append(j + 1).Append(" client ").
							Append(tarrifs[i][j].QuantityOfProduct).Append(" units of product").
							Append(j != originalNumberOfColumns - 1 ? ", " : "");
					}
				}
				message.Append(Environment.NewLine);
			}

			if (originalNumberOfRows != NumberOfRows)
			{
				for (int i = originalNumberOfRows; i < NumberOfRows; ++i)
				{
					for (int j = 0; j < NumberOfColumns; ++j)
					{
						if (tarrifs[i][j].HasProduct && tarrifs[i][j].Cost == 0)
						{
							message.Append("The ").
								Append(j + 1).
								Append(" client is reserved less ").
								Append(tarrifs[i][j].QuantityOfProduct).
								Append(" units of product").
								Append(Environment.NewLine);
						}
					}
				}
			}
			if (originalNumberOfColumns != NumberOfColumns)
			{
				for (int i = 0; i < NumberOfRows; ++i)
				{
					for (int j = originalNumberOfColumns; j < NumberOfColumns; ++j)
					{
						if (tarrifs[i][j].HasProduct && tarrifs[i][j].Cost == 0)
						{
							message.Append(tarrifs[i][j].QuantityOfProduct).
								Append(" units of unclaimed product stay on the ").
								Append(i + 1).
								Append(" warehouse").
								Append(Environment.NewLine);							
						}
					}
				}
			}

			bool isCongenitalPlan = false;
			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					if (tarrifs[i][j].HasProduct && tarrifs[i][j].Cost == 0)
					{
						isCongenitalPlan = true;
						message.Append("Optimal plan is congenital because x(").
							Append(i + 1).
							Append(", ").
							Append(j + 1).
							Append(") = 0").
							Append(Environment.NewLine);
					}
				}
			}

			if (!isCongenitalPlan)
				message.Append("Optimal plan is incongenital.");
			
			return message.ToString();
		}

		public string ToAssignmentProblemString()
		{
			var message = new StringBuilder();
			for (int i = 0; i < originalNumberOfRows; ++i)
			{
				for (int j = 0; j < originalNumberOfColumns; ++j)
				{
					if (tarrifs[i][j].HasProduct && tarrifs[i][j].QuantityOfProduct != 0)
						message.Append("The ").Append(j + 1).Append(" candidate is suitable ").
							    Append("for ").Append(i + 1).Append(" vacancy").
								Append(Environment.NewLine);
				}
			}

			return message.ToString();
		}

		public override string ToString()
		{
			var tarrifsString = new StringBuilder("Tarrifs");
			for (int i = 0; i < NumberOfRows; ++i)
			{
				tarrifsString.Append(Environment.NewLine);
				for (int j = 0; j < NumberOfColumns; ++j)
					tarrifsString.Append(tarrifs[i][j]).Append("\t");
			}
			return tarrifsString.ToString() + Environment.NewLine;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Tarrifs))
				return false;
			if (obj == this)
				return true;

			var other = obj as Tarrifs;

			if (this.NumberOfRows != other.NumberOfRows ||
				this.NumberOfColumns != other.NumberOfColumns)
				return false;

			for (int i = 0; i < NumberOfRows; ++i)
			{
				for (int j = 0; j < NumberOfColumns; ++j)
				{
					if (!this.tarrifs[i][j].Equals(other.tarrifs[i][j]))
						return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			return -1518437750 + EqualityComparer<Tarrif[][]>.Default.GetHashCode(tarrifs);
		}
	}
}
