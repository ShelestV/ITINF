using CarEvaluationML.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarEvaluationML.Collections
{
	class Cars : IEnumerable<Car>
	{
		private List<Car> cars;

		public Cars(IEnumerable<Car> cars)
		{
			this.cars = new List<Car>();
			this.cars.AddRange(cars);
		}

		public Car FindByIdOrDefault(int id)
		{
			return cars.Where(car => car.Id == id).FirstOrDefault();
		}

		public void Shuffle()
		{
			var random = new Random();

			for (int i = cars.Count - 1; i >= 1; --i)
			{
				int j = random.Next(i + 1);

				var temp = cars[i];
				cars[i] = cars[j];
				cars[j] = temp;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return cars.GetEnumerator();
		}

		public IEnumerator<Car> GetEnumerator()
		{
			return cars.GetEnumerator();
		}

	}
}
