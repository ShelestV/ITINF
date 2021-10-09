using CarEvaluationML.Models;
using CarEvaluationML.Models.Enums;

namespace CarEvaluationML.Builders
{
	class CarBuilder
	{
		private Car car;
		private static int nextId = 0;

		public CarBuilder()
		{
			Reset();
		}

		public void Reset()
		{
			car = new Car();
			car.Id = ++nextId;
		}

		public void BuildBuying(Buying buying)
		{
			car.Buying = buying;
		}

		public void BuildMaint(Maint maint)
		{
			car.Maint = maint;
		}

		public void BuildNumberOfDoors(int numberOfDoors)
		{
			car.NumberOfDoors = numberOfDoors;
		}

		public void BuildNumberOfPersons(int numberOfPersons)
		{
			car.NumberOfPersons = numberOfPersons;
		}

		public void BuildLugBoot(LugBoot lugBoot)
		{
			car.LugBoot = lugBoot;
		}

		public void BuildSafety(Safety safety)
		{
			car.Safety = safety;
		}

		public void BuildClass(Class @class)
		{
			car.Class = @class;
		}

		public Car GetCar()
		{
			return car;
		}
	}
}
