using System;
using System.Collections.Generic;
using System.Text;

namespace TransportProblem.Models
{
	class Clients
	{
		private int[] demand;

		public int this[int index]
		{
			get
			{
				if (0 <= index && index < Count)
					return demand[index];
				else
					throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (0 <= index && index < Count)
					demand[index] = value;
				else
					throw new ArgumentOutOfRangeException();
			}
		}

		public int Count { get => demand.Length; }

		public int TotalDemand 
		{
			get
			{
				int total = 0;
				for (int i = 0; i < Count; ++i)
					total += demand[i];
				return total;
			}
		}

		public Clients(int size)
		{
			demand = new int[size];
		}

		public Clients(Clients other)
		{
			this.demand = new int[other.Count];
			for (int i = 0; i < other.Count; ++i)
				this.demand[i] = other.demand[i];
		}

		public void AddBasis(int demand)
		{
			List<int> newDemand = new List<int>(Count + 1);
			for (int i = 0; i < Count; ++i)
				newDemand.Add(this.demand[i]);
			newDemand.Add(demand);

			this.demand = new int[newDemand.Count];
			for (int i = 0; i < newDemand.Count; ++i)
				this.demand[i] = newDemand[i];
		}

		public override string ToString()
		{
			var clientsString = new StringBuilder("{ ");
			for (int i = 0; i < Count; ++i)
				clientsString.Append(i != Count - 1 ? demand[i] + ", " : demand[i] + " }");
			return "Clients " + clientsString.ToString();
		}
	}
}
