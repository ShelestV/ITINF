using IrisML.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IrisML.Collections
{
	class Irises : IEnumerable<Iris>
	{
		private List<Iris> irises;

		public Irises(IEnumerable<Iris> irises)
		{
			this.irises = new List<Iris>();
			this.irises.AddRange(irises);
		}

		public Iris FindByIdOrDefault(int id)
		{
			return irises.Where(iris => iris.Id == id).FirstOrDefault();
		}

		public void Shuffle()
		{
			var random = new Random();

			for (int i = irises.Count - 1; i >= 1; --i)
			{
				int j = random.Next(i + 1);

				var temp = irises[i];
				irises[i] = irises[j];
				irises[j] = temp;
			}	
		}

		public IEnumerator<Iris> GetEnumerator()
		{
			return irises.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return irises.GetEnumerator();
		}
	}
}
