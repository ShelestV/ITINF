namespace TransportProblem.Models
{
	class Undefined
	{
		private int number;
		private bool isUndefined;

		public int Number
		{
			get => number;
			set
			{
				number = value;
				isUndefined = false;
			}
		}
		public bool IsUndefined { get => isUndefined; }

		public Undefined()
		{
			isUndefined = true;
		}

		public Undefined(int number)
		{
			this.number = number;
			isUndefined = false;
		}

		public override string ToString()
		{
			
			return isUndefined ? "?" : number.ToString();
		}
	}
}
