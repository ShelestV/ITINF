using ShelestML_2lb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShelestML_2lb.Algorithms
{
	class CandidateElimination
	{
		private readonly string defaultValue = "null";
		private readonly string generalizedValue = "?";

		private Matrix dataMatrix;
		private List<string> particularHypotheses;
		private List<List<string>> generalHypotheses;

		public CandidateElimination(Matrix matrix)
		{
			dataMatrix = matrix;

			particularHypotheses = new List<string>();
			for (int i = 0; i < dataMatrix.CountAttributes; ++i)
			{
				particularHypotheses.Add("");
			}

			generalHypotheses = new List<List<string>>();
		}

		public void CalculateHypotheses()
		{
			foreach (var row in dataMatrix)
			{
				CalculateHypothesis(row);
			}
		}

		private void CalculateHypothesis(MatrixRow row)
		{
			if (row.Positivity)
			{
				CalculatePositiveRow(row.Row);
			}
			else
			{
				CalculateNegativeRow(row.Row);
			}
		}

		private void CalculatePositiveRow(IEnumerable<string> row)
		{
			var listRow = row.ToList();
			for (int i = 0; i < listRow.Count; ++i)
			{
				string value = IsEmptyHypotesis(i) ? listRow[i] :
					IsGeneralizedHypotesis(listRow[i], particularHypotheses[i]) ? generalizedValue :
					defaultValue;
			}
		}

		private bool IsEmptyHypotesis(int index)
		{
			return string.IsNullOrEmpty(particularHypotheses[index]);
		}

		private bool IsGeneralizedHypotesis(string hypothesis, string value)
		{
			return !hypothesis.Equals(value);
		}

		private void CalculateNegativeRow(IEnumerable<string> row)
		{
			
		}

	}
}
