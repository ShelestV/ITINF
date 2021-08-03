using System;
using System.Collections.Generic;

namespace TransportProblem.Models
{
	class MinimalElementsMethod
	{
		private static List<int> rowIndexs;
		private static List<int> columnIndexs;

		public static Tarrifs GetBasePlan(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			rowIndexs = new List<int>();
			columnIndexs = new List<int>();

			var basePlan = new Tarrifs(tarrifs);

			while (!IsAllResourcesUsed(basePlan, warehouses, clients))
			{
				NextIteration(basePlan, warehouses, clients);
			}

			return basePlan;
		}

		private static bool IsAllResourcesUsed(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				if (tarrifs.GetTotalStock(i) != warehouses[i])
					return false;
			}

			for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
			{
				if (tarrifs.GetTotalDemand(j) != clients[j])
					return false;
			}

			return true;
		}

		private static void NextIteration(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			int row = -1;
			int column = -1;

			int minCost = -1;

			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
				{
					if (!rowIndexs.Contains(i) && !columnIndexs.Contains(j) && tarrifs[i, j].Cost != 0)
						SetMinIfItsLess(tarrifs[i, j], ref minCost, ref row, ref column);
				}
			}

			if (row != -1 && column != -1)
				OccupyCell(tarrifs, row, column, warehouses, clients);
			else
				SetRemaining(tarrifs, warehouses, clients);
		}

		private static void SetMinIfItsLess(Tarrif tarrif, ref int minCost, ref int row, ref int column)
		{
			if ((row == -1 && column == -1) || tarrif.Cost < minCost)
			{
				minCost = tarrif.Cost;

				row = tarrif.Row;
				column = tarrif.Column;
			}
		}

		private static void OccupyCell(Tarrifs tarrifs, int row, int column, Warehouses warehouses, Clients clients)
		{
			int demand = clients[column] - tarrifs.GetTotalDemand(column);
			int stock = warehouses[row] - tarrifs.GetTotalStock(row);

			int quantity = Math.Min(demand, stock);

			if (quantity >= 0)
			{
				if (quantity > 0)
					tarrifs[row, column].QuantityOfProduct = quantity;
				else if (quantity == 0)
					tarrifs[row, column].SetImaginary();

				tarrifs.NumberOfOccupied += 1;

				if (clients[column] - tarrifs.GetTotalDemand(column) == 0)
					columnIndexs.Add(column);
				if (warehouses[row] - tarrifs.GetTotalStock(row) == 0)
					rowIndexs.Add(row);
			}
		}

		private static void SetRemaining(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			int row = -1;
			int column = -1;

			int minCost = -1;

			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
				{
					if (!rowIndexs.Contains(i) && !columnIndexs.Contains(j))
						SetMinIfItsLess(tarrifs[i, j], ref minCost, ref row, ref column);
				}
			}

			if (row != -1 && column != -1)
				OccupyCell(tarrifs, row, column, warehouses, clients);
			else
				SetRemaining(tarrifs, warehouses, clients);
		}
	}
}
