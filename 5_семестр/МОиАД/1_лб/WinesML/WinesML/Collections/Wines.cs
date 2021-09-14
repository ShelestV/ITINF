using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WinesML.Models;

namespace WinesML.Collections
{
	public class Wines : IEnumerable<Wine>
	{
		private const string SPLIT_SYMBOL = "\n";
		private List<Wine> wines = new List<Wine>();

		//public int Count => data.Count;

		public Wine this[int index]
		{ 
			get => wines[index];
		}

		public Wines(string data)
		{
			var wineStrings = data.Split("\n");
			foreach (var wineString in wineStrings)
			{
				if (!string.IsNullOrEmpty(wineString))
					wines.Add(new Wine(wineString));
			}
		}

		public void Add(Wine wine)
		{
			wines.Add(wine);
		}

		//public void AddRange(ICollection<Wine> wines)
		//{
		//	foreach (var wine in wines)
		//		this.wines.Add(wine);
		//}

		//public void AddRange(Wines wines)
		//{
		//	foreach (var wine in wines)
		//		this.wines.Add(wine);
		//}

		//public void Clear()
		//{
		//	wines.Clear();
		//}

		public bool Contains(Wine wine)
		{
			return wines.Contains(wine);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wine"></param>
		/// <returns>Returns wine object or null</returns>
		public Wine Find(Wine wine)
		{
			return wines.FirstOrDefault(w => wine.Equals(w));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wineIndex"></param>
		/// <returns>Returns wine object or null</returns>
		public Wine FindAt(int wineIndex)
		{
			return wines[wineIndex];
		}

		public void Shuffle()
		{
			var random = new Random();

			for (int i = wines.Count - 1; i >= 1; --i)
			{
				int j = random.Next(i + 1);

				var temp = wines[i];
				wines[i] = wines[j];
				wines[j] = temp;
			}
		}

		//public void CopyTo(Wine[] wines, int wineIndex)
		//{
		//	wines.CopyTo(this.wines.ToArray(), wineIndex);
		//}

		//public bool Remove(Wine wine)
		//{
		//	return wines.Remove(wine);
		//}

		public IEnumerator<Wine> GetEnumerator()
		{
			return wines.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return wines.GetEnumerator();
		}
	}
}
