using System;

namespace TransportProblem.Models
{
	class NorthWestAngleMethod
	{
		static int row;
		static int column;

		static public Tarrifs GetBasePlan(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			row = 0;
			column = 0;

			var basePlan = new Tarrifs(tarrifs);

			bool inLimits = row <= basePlan.NumberOfRows &&
				   column <= basePlan.NumberOfColumns;
			while (inLimits && !IsBuiltBasePlan(tarrifs, warehouses, clients))
			{
				NextIteration(basePlan, warehouses, clients, row, column);

				if (basePlan.GetTotalDemand(column) - clients[column] == 0)
					++column;

				if (basePlan.GetTotalStock(row) - warehouses[row] == 0)
					++row;

				inLimits = row < basePlan.NumberOfRows &&
				   column < basePlan.NumberOfColumns;
			}

			return basePlan;
		}

		static private bool IsBuiltBasePlan(Tarrifs tarrifs, Warehouses warehouses, Clients clients)
		{
			for (int j = 0; j < clients.Count; ++j)
				if (clients[j] > tarrifs.GetTotalDemand(j))
					return false;

			for (int i = 0; i < warehouses.Count; ++i)
				if (warehouses[i] > tarrifs.GetTotalStock(i))
					return false;

			return true;
		}

		static private void NextIteration(Tarrifs tarrifs, 
										  Warehouses warehouses, Clients clients, 
										  int row, int column)
		{
			int demand = clients[column] - tarrifs.GetTotalDemand(column);
			int stock = warehouses[row] - tarrifs.GetTotalStock(row);

			int quantity = Math.Min(demand, stock);

			if (quantity >= 0)
			{
				tarrifs[row, column].QuantityOfProduct = quantity;
				tarrifs.NumberOfOccupied += 1;
			}
		}
	}
}
