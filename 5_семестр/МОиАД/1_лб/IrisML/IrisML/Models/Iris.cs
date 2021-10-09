namespace IrisML.Models
{
	class Iris
	{
		private int id;
		private string idName;

		public int Id 
		{
			get => id;
			set => id = value; 
		}

		public string IdName 
		{
			get => string.IsNullOrEmpty(idName) ? id.ToString() : idName; 
			set => idName = value;
		}

		public string Name { get; set; }
		public double SepalLength { get; set; }
		public double SepalWidth { get; set; }
		public double PetalLength { get; set; }
		public double PetalWidth { get; set; }

		public override string ToString()
		{
			return IdName + ": " + Name + " -> " +
				"Sepal length = " + SepalLength + "; " +
				"Sepal width = " + SepalWidth + "; " +
				"Petal length = " + PetalLength + "; " +
				"Petal width = " + PetalWidth + ";\n";
		}
	}
}
