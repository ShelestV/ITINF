using ShelestML_2lb.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShelestML_2lb.Algorithms
{
	class FindS
	{
		private readonly string defaultValue = "null";
		private readonly string generalizedValue = "?";

		private PositiveMatrix dataMatrix;
		private List<string> particularHypotheses;

		public IEnumerable<string> ParticularHypotheses { get => particularHypotheses; }

		public FindS(PositiveMatrix matrix)
		{
			dataMatrix = matrix;

			particularHypotheses = new List<string>();
			for (int i = 0; i < dataMatrix.CountAttributes; ++i)
			{
				particularHypotheses.Add("");
			}
		}

		public void CalculateHypotheses()
		{
			foreach(var row in dataMatrix)
			{
				CalculateHypothesis(row);
			}
		}

		private void CalculateHypothesis(IEnumerable<string> row)
		{
			var listRow = row.ToList();
			for (int i = 0; i < listRow.Count; ++i)
			{
				string value = IsEmptyHypothesis(i) ? listRow[i] : 
					IsGeneralizedHypotesis(listRow[i], particularHypotheses[i]) ? generalizedValue : 
					defaultValue;

				UpdateHypothesisByIndex(i, value);
			}
		}

		private bool IsEmptyHypothesis(int index)
		{
			return string.IsNullOrEmpty(particularHypotheses[index]);
		}

		private bool IsGeneralizedHypotesis(string hypothesis, string value)
		{
			return !hypothesis.Equals(value);
		}

		private void UpdateHypothesisByIndex(int index, string value)
		{
			if (!value.Equals(defaultValue))
			{
				particularHypotheses[index] = value;
			}
		}
	}
}
