using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportProblem.Models
{
	class TransportationProblem
	{
		private Clients clients;
		private Warehouses warehouses;
		private Tarrifs tarrifs;

		private V[] v;
		private U[] u;

		public TransportationProblem(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			this.clients = new Clients(clients);
			this.warehouses = new Warehouses(warehouses);
			this.tarrifs = new Tarrifs(tarrifs);

			Balance();
		}

		private void Balance()
		{
			int totalDemand = clients.TotalDemand;
			int totalStock = warehouses.TotalStock;

			if (totalDemand > totalStock)
			{
				warehouses.AddBasis(totalDemand - totalStock);
				tarrifs.AddStockBasis();
			}
			else if (totalDemand < totalStock)
			{
				clients.AddBasis(totalStock - totalDemand);
				tarrifs.AddDemandBasis();
			}
		}

		public Tarrifs GetOptimalPlanByNorthWestAngleMethod()
		{
			var basePlan = NorthWestAngleMethod.GetBasePlan(tarrifs, warehouses, clients);
			return GetOptimalePlan(basePlan);
		}

		public Tarrifs GetOptimalPlanByMinimalElementsMethod() 
		{
			var basePlan = MinimalElementsMethod.GetBasePlan(tarrifs, warehouses, clients);
			return GetOptimalePlan(basePlan);
		}

		private Tarrifs GetOptimalePlan(Tarrifs plan)
		{
			RecalculateVandU(plan);
			while (!plan.IsOptimal(u, v))
			{
				PotentialMethod.OptimisePlan(plan, u, v);
				RecalculateVandU(plan);
			}
			return plan;
		}

		private void RecalculateVandU(Tarrifs tarrifs)
		{
			u = new U[tarrifs.NumberOfRows];
			v = new V[tarrifs.NumberOfColumns];

			for (int i = 0; i < u.Length; ++i)
				u[i] = new U();
			for (int i = 0; i < v.Length; ++i)
				v[i] = new V();

			u[0].Number = 0;

			Tarrifs previous;
			while (!IsAllDefined(u) || !IsAllDefined(v))
			{
				Console.WriteLine(tarrifs.ToString());
				Console.Write("u { ");
				for (int i = 0; i < u.Length; ++i)
					Console.Write(u[i].ToString() + (i != u.Length - 1 ? ", " : " }"));
				Console.WriteLine();
				Console.Write("v { ");
				for (int j = 0; j < v.Length; ++j)
					Console.Write(v[j].ToString() + (j != v.Length - 1 ? ", " : " }"));
				Console.WriteLine();

				previous = new Tarrifs(tarrifs);
				NextIteration(tarrifs);
				if (tarrifs.NumberOfOccupied < tarrifs.TeoreticNumberOfOccupied)
					SetImaginary(tarrifs);

				if (tarrifs.Equals(previous))
					ChangeImaginary(tarrifs);
			}
		}

		private void NextIteration(Tarrifs tarrifs)
		{
			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
				{
					if (!tarrifs[i, j].HasProduct)
						continue;

					if (v[j].IsUndefined && !u[i].IsUndefined)
						v[j].Number = tarrifs[i, j].Cost - u[i].Number;

					if (!v[j].IsUndefined && u[i].IsUndefined)
						u[i].Number = tarrifs[i, j].Cost - v[j].Number;
				}
			}
		}

		private void SetImaginary(Tarrifs tarrifs)
		{
			int row = -1;
			int column = -1;
			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
				{
					if (tarrifs[i, j].HasProduct ||
						(v[j].IsUndefined && u[i].IsUndefined))
						continue;

					if ((row == -1 && column == -1) ||
						tarrifs[i, j].Cost <= tarrifs[row, column].Cost)
					{
						row = i;
						column = j;
					}
				}
			}

			if (row != -1 && column != -1)
			{
				tarrifs[row, column].SetImaginary();
				tarrifs.NumberOfOccupied += 1;
			}
		}

		private void ChangeImaginary(Tarrifs tarrifs)
		{
			int row = u.ToList().FindIndex(u => u.IsUndefined);
			int column = v.ToList().FindIndex(v => v.IsUndefined);

			if (row == -1 || column == -1)
				return;

			int min = -1;

			int rowOfMin = -1;
			int columnOfMin = -1;

			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				if (!tarrifs[i, column].HasProduct &&
					((rowOfMin == -1 && columnOfMin == -1) ||
					 tarrifs[i, column].Cost < min))
				{
					min = tarrifs[i, column].Cost;

					rowOfMin = i;
					columnOfMin = column;
				}
			}

			for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
			{
				if ((rowOfMin == -1 && columnOfMin == -1) ||
					(!tarrifs[row, j].HasProduct && tarrifs[row, j].Cost < min))
				{
					min = tarrifs[row, j].Cost;

					rowOfMin = row;
					columnOfMin = j;
				}
			}

			if (rowOfMin != -1 && columnOfMin != -1)
			{
				if (rowOfMin == row)
				{
					for (int i = 0; i < tarrifs.NumberOfRows; ++i)
					{
						if (tarrifs[i, columnOfMin].HasProduct && tarrifs[i, columnOfMin].QuantityOfProduct == 0)
							tarrifs[i, columnOfMin].QuantityOfProduct = 0;
					}
				}
				if (columnOfMin == column)
				{
					for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
					{
						if (tarrifs[rowOfMin, j].HasProduct && tarrifs[rowOfMin, j].QuantityOfProduct == 0)
							tarrifs[rowOfMin, j].QuantityOfProduct = 0;
					}
				}

				tarrifs[rowOfMin, columnOfMin].SetImaginary();
			}
		}

		private bool IsAllDefined(Undefined[] undefineds)
		{
			for (int i = 0; i < undefineds.Length; ++i)
				if (undefineds[i].IsUndefined)
					return false;
			return true;
		}
	}
}
