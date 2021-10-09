using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShelestML_2lb.Models
{
	class PositiveMatrix : IEnumerable<IEnumerable<string>>
	{
		private List<string> attributeNames;
		private List<List<string>> data;

		public PositiveMatrix(IEnumerable<string> attributeNames)
		{
			data = new List<List<string>>();
			this.attributeNames = attributeNames.ToList();
		}

		public IEnumerable<string> this[string attributeName]
		{
			get
			{
				var index = attributeNames.FindIndex(x => attributeName.Equals(x));
				var result = new List<string>();
				for (int i = 0; i < data.Count; ++i)
				{
					result.Add(data[i][index]);
				}
				return result;
			}
		}

		public IEnumerable<string> this[int index]
		{
			get
			{
				return data[index];
			}
		}

		public string this[string attributeName, int index]
		{
			get
			{
				var attributeValues = this[attributeName].ToList();
				return attributeValues[index];
			}
		}

		public int CountRows
		{
			get
			{
				return data.Count;
			}
		}

		public int CountAttributes
		{
			get
			{
				return data[0]?.Count ?? 0;
			}
		}

		public void Add(IEnumerable<string> row)
		{
			if (row.ToList().Count == attributeNames.Count)
			{
				data.Add(row.ToList());
			}
		}

		public IEnumerator<IEnumerable<string>> GetEnumerator()
		{
			return data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return data.GetEnumerator();
		}
	}
}
