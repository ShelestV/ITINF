using System.Collections.Generic;
using System.Text;

namespace SimplexMethod
{
	enum Objective
	{ 
		Min,
		Max
	}

	class ObjectiveFunction
	{
		private List<Undefined> undefineds;
		private Free free;
		private Objective objective;

		public List<Undefined> Undefineds { get => undefineds; }
		public int NumberOfUndefineds { get => undefineds.Count; }
		public Free Free { get => free; }
		public Objective Objective { get => objective; }

		public ObjectiveFunction(List<Undefined> undefineds, Objective objective)
		{
			this.undefineds = new List<Undefined>();
			undefineds.ForEach(x => this.undefineds.Add(new Undefined(x)));

			this.free = new Free(0);
			this.objective = objective;
		}

		public ObjectiveFunction(ObjectiveFunction other)
		{
			this.undefineds = new List<Undefined>();
			other.undefineds.ForEach(x => this.undefineds.Add(new Undefined(x)));

			this.free = new Free(other.free);
			this.objective = other.objective;
		}

		public bool IsOptimal()
		{
			return (objective == Objective.Min) ? IsOptimalMin() : IsOptimalMax();
		}

		private bool IsOptimalMin()
		{
			foreach (var x in undefineds)
			{
				if (x.Coeficient > 0)
				{
					return false;
				}
			}
			return true;
		}

		private bool IsOptimalMax()
		{
			foreach (var x in undefineds)
			{
				if (x.Coeficient < 0)
				{
					return false;
				}
			}
			return true;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("F = ");
			for (int i = 0; i < undefineds.Count; ++i)
			{
				sb.Append(undefineds[i].ToString()).Append((i == undefineds.Count - 1) ? " - " : " + ");
			}
			sb.Append(free.ToString());
			return sb.ToString();
		}
	}
}
