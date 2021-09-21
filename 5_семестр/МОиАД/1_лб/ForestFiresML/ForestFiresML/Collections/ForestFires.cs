using ForestFiresML.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ForestFiresML.Collections
{
	class ForestFires : IEnumerable<ForestFire>
	{
		private List<ForestFire> forestFires;

		public ForestFires()
		{
			forestFires = new List<ForestFire>();
		}

		public ForestFire this[int index] 
		{
			get => forestFires[index];
			set => forestFires[index] = value;
		}

		public ForestFire FindByIdOrDefault(int id)
		{
			return forestFires.Where(fire => fire.Id == id).FirstOrDefault();
		}

		public ForestFires(IEnumerable<ForestFire> forestFires)
		{
			this.forestFires = new List<ForestFire>();
			foreach (var forestFire in forestFires)
			{
				this.forestFires.Add(forestFire);
			}
		}

		public void Add(ForestFire forestFire)
		{
			forestFires.Add(forestFire);
		}

		public void Shuffle()
		{
			var random = new Random();

			for (int i = forestFires.Count - 1; i >= 1; --i)
			{
				int j = random.Next(i + 1);

				var temp = forestFires[i];
				forestFires[i] = forestFires[j];
				forestFires[j] = temp;
			}
		}

		public IEnumerator<ForestFire> GetEnumerator()
		{
			return forestFires.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return forestFires.GetEnumerator();
		}
	}
}
