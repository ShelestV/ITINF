using System.Collections.Generic;
using System.Text;

namespace SimplexMethod
{
	enum Sign
	{
		EqualOrLess,
		Equal,
		EqualOrGreater		
	}

	class Expression
	{
		private List<Undefined> undefineds;
		private Free free;
		private Sign sign;

		public List<Undefined> Undefineds { get => undefineds; }
		public int NumberOfUndefineds { get => undefineds.Count; }
		public Free Free { get => free; }
		public Sign Sign { get => sign; set => sign = value; }

		public Expression(List<Undefined> undefineds, Free free, Sign sign)
		{
			this.undefineds = new List<Undefined>();
			undefineds.ForEach(x => this.undefineds.Add(new Undefined(x)));

			this.free = new Free(free);
			this.sign = sign;
		}

		public  Expression(Expression other)
		{
			this.undefineds = new List<Undefined>();
			other.undefineds.ForEach(x => this.undefineds.Add(new Undefined(x)));

			this.free = new Free(other.free);
			this.sign = other.sign;
		}

		public void DeleteOnElement(double element)
		{
			undefineds.ForEach(x => x.Coeficient /= element);
			free.Coeficient /= element;
		}

		public void ChangeCoeficientToElementByIndex(double coeficient, int index)
		{
			undefineds[index].Coeficient = coeficient;
		}

		public void MultiplyOnMinusOne()
		{
			undefineds.ForEach(x => x.Coeficient *= -1);
			free.Coeficient *= -1;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < undefineds.Count; ++i)
			{
				sb.Append(undefineds[i].ToString()).Append((i == undefineds.Count - 1) ? " = " : " + ");
			}
			sb.Append(free.ToString());
			return sb.ToString();
		}
	}
}
