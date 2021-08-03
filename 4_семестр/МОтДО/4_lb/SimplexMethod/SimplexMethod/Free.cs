namespace SimplexMethod
{
	class Free
	{
		protected double coeficient;

		public double Coeficient { get => coeficient; set => coeficient = value; }

		public Free(double coeficient)
		{
			this.coeficient = coeficient;
		}

		public Free(Free other)
		{
			this.coeficient = other.coeficient;
		}

		public override string ToString()
		{
			return coeficient.ToString();
		}
	}
}
