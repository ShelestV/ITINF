using IrisML.Models;

namespace IrisML.Builders
{
	class IrisBuilder
	{
		private Iris iris;
		private static int nextId = 0;

		public IrisBuilder()
		{
			Reset();
		}

		public void Reset()
		{
			iris = new Iris();
			iris.Id = ++nextId;
		}

		public void BuildName(string name)
		{
			iris.Name = name;
		}

		public void BuildSepalLength(double sepalLength)
		{
			iris.SepalLength = sepalLength;
		}

		public void BuildSepalWidth(double sepalWidth)
		{
			iris.SepalWidth = sepalWidth;
		}

		public void BuildPetalLength(double petalLength)
		{
			iris.PetalLength = petalLength;
		}

		public void BuildPetalWidth(double petalWidth)
		{
			iris.PetalWidth = petalWidth;
		}

		public Iris GetIris()
		{
			return iris;
		}
	}
}
