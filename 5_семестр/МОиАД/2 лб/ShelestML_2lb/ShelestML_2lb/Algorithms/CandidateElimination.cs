using ShelestML_2lb.Models;
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

		public List<string> ParticularHypotheses 
		{
			get
			{
				return particularHypotheses;
			}
		}

		public List<List<string>> GeneralHypotheses
		{
			get
			{
				return generalHypotheses;
			}
		}

		public CandidateElimination(Matrix matrix)
		{
			dataMatrix = matrix;

			particularHypotheses = new List<string>();
			for (int attributeIndex = 0; attributeIndex < dataMatrix.CountAttributes; ++attributeIndex)
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
				if (generalHypotheses.Count > 0)
				{
					DeleteUncertainty();
				}
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
				if (generalHypotheses.Count == 0)
				{
					CalculateNegativeRowWithEmptyGeneralizedHypotheses(row.Row);
				}
				else
				{
					CalculateNegativeRow(row.Row);
				}
			}
		}

		private void CalculatePositiveRow(IEnumerable<string> row)
		{
			var listRow = row.ToList();
			for (int attributeIndex = 0; attributeIndex < listRow.Count; ++attributeIndex)
			{
				string value = IsEmptyHypotesis(attributeIndex) ? listRow[attributeIndex] :
					IsGeneralizedHypotesis(listRow[attributeIndex], particularHypotheses[attributeIndex]) ? generalizedValue :
					defaultValue;

				UpdateParticularHypotesisByIndex(attributeIndex, value);
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

		private void UpdateParticularHypotesisByIndex(int index, string value)
		{
			if (!value.Equals(defaultValue))
			{
				particularHypotheses[index] = value;
			}
		}

		private void CalculateNegativeRowWithEmptyGeneralizedHypotheses(IEnumerable<string> row)
		{
			var listRow = row.ToList();
			for (int attributeIndex = 0; attributeIndex < listRow.Count; ++attributeIndex)
			{
				if (!listRow[attributeIndex].Equals(particularHypotheses[attributeIndex]))
				{
					var generalHypothesis = new List<string>();
					for (int secondAttributeIndex = 0; secondAttributeIndex < listRow.Count; ++secondAttributeIndex)
					{
						generalHypothesis.Add(attributeIndex == secondAttributeIndex ? particularHypotheses[attributeIndex] : generalizedValue);
					}
					generalHypotheses.Add(generalHypothesis);
				}
			}
		}

		private void CalculateNegativeRow(IEnumerable<string> row)
		{
			var intermediateGeneralizedHypothesesSpace = new List<List<string>>();
			var listRow = row.ToList();

			for (int attributeIndex = 0; attributeIndex < listRow.Count; ++attributeIndex)
			{
				if (listRow[attributeIndex].Equals(particularHypotheses[attributeIndex]))
				{
					intermediateGeneralizedHypothesesSpace.AddRange(CheckRowElementsAndAddGeneralHypothesis(listRow, attributeIndex));
				}
			}
			DeleteParticularHypotesesInGeneralizedOne(intermediateGeneralizedHypothesesSpace);
			DeleteElementsFromSpaceByGeneralizedHypoteses(intermediateGeneralizedHypothesesSpace);
			generalHypotheses.AddRange(intermediateGeneralizedHypothesesSpace);
			DeleteParticularHypotesesInGeneralizedOne(generalHypotheses);
		}

		private List<List<string>> CheckRowElementsAndAddGeneralHypothesis(List<string> row, int datarowAttributeIndex)
		{
			var intermediateGeneralizedHypothesesSpace = new List<List<string>>();
			for (int hypothesisAttributeIndex = 0; hypothesisAttributeIndex < row.Count; ++hypothesisAttributeIndex)
			{
				if (!row[datarowAttributeIndex].Equals(particularHypotheses[hypothesisAttributeIndex]))
				{
					var list = new List<string>();
					for (int thirdAttributeIndex = 0; thirdAttributeIndex < row.Count; ++thirdAttributeIndex)
					{
						list.Add(thirdAttributeIndex == datarowAttributeIndex || thirdAttributeIndex == hypothesisAttributeIndex 
							? particularHypotheses[thirdAttributeIndex] : generalizedValue);
					}
					intermediateGeneralizedHypothesesSpace.Add(list);
				}
			}

			return intermediateGeneralizedHypothesesSpace;
		}

		private void DeleteParticularHypotesesInGeneralizedOne(List<List<string>> space)
		{
			var indexesForDeleting = GetIndexesForDeleting(space).ToList();

			if (indexesForDeleting.Count > 0)
			{
				indexesForDeleting.Sort();
				indexesForDeleting.Distinct();

				foreach (var index in indexesForDeleting)
				{
					space.RemoveAt(index);
				}
			}
		}

		private IEnumerable<int> GetIndexesForDeleting(List<List<string>> space)
		{
			var indexesForDeleting = new List<int>();

			for (int firstIndex = 0; firstIndex < space.Count - 1; ++firstIndex)
			{
				for (int secondIndex = firstIndex + 1; secondIndex < space.Count; ++secondIndex)
				{
					var result = HasFirstToBeDeleted(space[firstIndex], space[secondIndex]);
					if (result.HasValue)
					{
						indexesForDeleting.Add(result.Value ? firstIndex : secondIndex);
					}
				}
			}

			return indexesForDeleting;
		}

		private bool? HasFirstToBeDeleted(List<string> firstList, List<string> secondList)
		{
			bool hasFirstToBeDeleted = false;
			bool hasSecondToBeDeleted = false;

			for (int attributeIndex = 0; attributeIndex < firstList.Count; ++attributeIndex)
			{
				if (firstList[attributeIndex].Equals(generalizedValue) &&
					!secondList[attributeIndex].Equals(generalizedValue))
				{
					hasFirstToBeDeleted = true;
				}
				else if (!firstList[attributeIndex].Equals(generalizedValue) &&
						 secondList[attributeIndex].Equals(generalizedValue))
				{
					hasSecondToBeDeleted = true;
				}
			}

			return hasFirstToBeDeleted != hasSecondToBeDeleted ? (hasFirstToBeDeleted ? true : false) : null;
		}

		private void DeleteElementsFromSpaceByGeneralizedHypoteses(List<List<string>> space)
		{
			for (int spaceIndex = 0; spaceIndex < space.Count; ++spaceIndex)
			{
				for (int hypothesesIndex = 0; hypothesesIndex < generalHypotheses.Count; ++hypothesesIndex)
				{
					if (HasSpaceElementToBeDeleted(space[spaceIndex], generalHypotheses[hypothesesIndex]))
					{
						space.RemoveAt(spaceIndex);
						if (space.Count == 0)
						{
							return;
						}	
					}
				}
			}
		}

		private bool HasSpaceElementToBeDeleted(List<string> spaceRow, List<string> hypothesesRow)
		{
			bool haveToRemove = false;

			for (int attributeIndex = 0; attributeIndex < spaceRow.Count; ++attributeIndex)
			{
				if (hypothesesRow[attributeIndex].Equals(generalizedValue) &&
					!spaceRow[attributeIndex].Equals(generalizedValue))
				{
					haveToRemove = true;
				}
				else if (!hypothesesRow[attributeIndex].Equals(generalizedValue) &&
						 spaceRow[attributeIndex].Equals(generalizedValue))
				{
					return false;
				}
			}

			return haveToRemove;
		}

		private void DeleteUncertainty()
		{
			for (int particularIndex = 0; particularIndex < particularHypotheses.Count; ++particularIndex)
			{
				for (int generalIndex = 0; generalIndex < generalHypotheses.Count; ++generalIndex)
				{
					if (particularHypotheses[particularIndex].Equals(generalizedValue) && 
						!generalHypotheses[generalIndex][particularIndex].Equals(generalizedValue))
					{
						generalHypotheses.RemoveAt(generalIndex);
					}
				}
			}
		}
	}
}
