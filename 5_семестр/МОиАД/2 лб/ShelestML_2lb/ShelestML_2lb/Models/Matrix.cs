using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShelestML_2lb.Models
{
	class Matrix : IEnumerable<MatrixRow>
	{
		private PositiveMatrix data;
		private List<bool> positivitiesOfRows;

		public Matrix(IEnumerable<string> attributeNames)
		{
			data = new PositiveMatrix(attributeNames);
			positivitiesOfRows = new List<bool>();
		}

		public IEnumerable<string> this[string attributeName]
		{
			get
			{
				return data[attributeName];
			}
		}

		public MatrixRow this[int index]
		{
			get
			{
				return new MatrixRow { Row = data[index], Positivity = positivitiesOfRows[index] };
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
				return data.CountRows;
			}
		}
		public int CountAttributes 
		{
			get
			{
				return data.CountAttributes;
			}
		}

		public void Add(IEnumerable<string> row, bool positivity)
		{
			data.Add(row);
			positivitiesOfRows.Add(positivity);
		}

		public IEnumerator<MatrixRow> GetEnumerator()
		{
			var result = new List<MatrixRow>();

			for (int i = 0; i < data.CountRows; ++i)
			{
				result.Add(new MatrixRow { Row = data[i], Positivity = positivitiesOfRows[i] });
			}

			return result.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
