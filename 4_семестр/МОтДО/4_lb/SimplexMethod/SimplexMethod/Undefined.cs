using System;

namespace SimplexMethod
{
	class Undefined : Free
	{
		private int id;

		public int Index { get => id - 1; }

		public Undefined(int index, double coeficient) : base(coeficient)
		{
			this.id = index + 1;
		}

		public Undefined(Undefined other) : base(other.coeficient)
		{
			this.id = other.id;
		}

		public override string ToString()
		{
			return "(" + (Math.Round(coeficient * 1000) / 1000) + ")x" + id;
		}

		public string ToStringWithoutCoeficient()
		{
			return "x" + id;
		}

		public override bool Equals(object obj)
		{
			if (this == obj) return true;
			return obj != null &&
				this.GetType() == obj.GetType() &&
				this.id == ((Undefined)obj).id;
		}

		public override int GetHashCode()
		{
			int hashCode = -1727115840;
			hashCode = hashCode * -1521134295 + coeficient.GetHashCode();
			hashCode = hashCode * -1521134295 + id.GetHashCode();
			return hashCode;
		}
	}
}
