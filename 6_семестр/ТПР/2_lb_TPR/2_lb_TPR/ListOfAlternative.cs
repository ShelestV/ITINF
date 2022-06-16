using System.Collections.Generic;
using System.Linq;

namespace _2_lb_TPR
{
	class ListOfAlternatives : IMyList<Alternative>
	{
		protected List<Alternative> alternatives;
		public List<Alternative> Alternatives { get => alternatives; }

		private List<Alternative> goodAlternatives;
		private List<Alternative> badAlternatives;

		private Alternative theBestAlternative;
		private Alternative theWorstAlternative;

		public List<Alternative> GoodAlternatives { get => goodAlternatives; }
		public List<Alternative> BadAlternatives { get => badAlternatives; }

		public Alternative TheBestAlternative { get => theBestAlternative; }
		public Alternative TheWorstAlternative { get => theWorstAlternative; }

		public ListOfAlternatives()
		{
			alternatives = new List<Alternative>();

			goodAlternatives = new List<Alternative>();
			badAlternatives = new List<Alternative>();
		}

		public ListOfAlternatives(List<Criteria> criterias)
		{
			alternatives = new List<Alternative>();

			goodAlternatives = new List<Alternative>();
			badAlternatives = new List<Alternative>();

			// 0 is index of first criteria
			// list is needed inside of AddNewAlternative
			GoToTheNextCriteriaToCreateAlternative(criterias, 0, new List<Mention>());

			theBestAlternative = alternatives[0];
			theBestAlternative.Group = Group.Good; // First alternative in the list is the best
			goodAlternatives.Add(theBestAlternative);

			theWorstAlternative = alternatives[alternatives.Count - 1];
			theWorstAlternative.Group = Group.Bad; // Last alternative in the list is the worst
			badAlternatives.Add(theWorstAlternative);
		}

		private void GoToTheNextCriteriaToCreateAlternative(List<Criteria> criterias, int indexOfCriteria, List<Mention> mentions)
		{
			if (indexOfCriteria == criterias.Count - 1)
			{
				// If it is last criteria in the list
				// We will add last alternative mention
				// Create new alternative of this mentions
				// And add alternative to the list
				foreach (var mention in criterias[indexOfCriteria].Mentions)
				{
					var alternativeMentions = new List<Mention>();
					mentions.ForEach(m => alternativeMentions.Add(new Mention(m)));
					alternativeMentions.Add(new Mention(mention));
					alternatives.Add(new Alternative(alternativeMentions));
				}
			}
			else
			{
				// Recurtion

				// If it is not the last criteria in the list
				// We will add mention to alternative mentions
				// And go to the next criteria
				foreach (var mention in criterias[indexOfCriteria].Mentions)
				{
					var passingMentions = new List<Mention>();
					mentions.ForEach(m => passingMentions.Add(new Mention(m)));
					passingMentions.Add(new Mention(mention));
					GoToTheNextCriteriaToCreateAlternative(criterias, indexOfCriteria + 1, passingMentions);
				}
			}
		}

		public void Add(Alternative alternative)
		{
			if (alternative != null &&
				!alternatives.Contains(alternative))
			{
				this.alternatives.Add(alternative);
			}
		}

		public void Remove(Alternative alternative)
		{
			if (alternative != null)
			{
				alternatives.Remove(alternative);
			}
		}

		public Alternative FindByStringOrReturnNull(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				return alternatives.FirstOrDefault(a => a.ToString().Equals(str));
			}
			return null;
		}

		public bool IsContained(string str)
		{
			if (string.IsNullOrEmpty(str)) { return false; }
			return alternatives.Find(a => a.ToString().Equals(str)) != null;
		}

		private Alternative CalculateCenter(List<Alternative> alternatives)
		{
			DivideOnGoodAndBadGroups();

			var centerMentions = new List<Mention>();
			centerMentions.AddRange(alternatives[0].Mentions);
			var center = new Alternative(centerMentions);

			for (int i = 1; i < alternatives.Count; ++i)
			{
				center = center + alternatives[i];
			}

			center.DivideMentionsValues(alternatives.Count);
			return center;
		}

		public Alternative CalculateGoodCenter(List<Alternative> alternatives)
		{
			return CalculateCenter(goodAlternatives);
		}

		public Alternative CalculateBadCenter(List<Alternative> alternatives)
		{
			return CalculateCenter(badAlternatives);
		}

		private Alternative GetFirstUncertainAlternativeOrNull()
		{
			foreach (var alternative in alternatives)
			{
				if (alternative.Group == Group.Uncertain)
				{
					return alternative;
				}
			}
			return null;
		}

		public Alternative GetUncertainAlternativeWithMaxInformativenessOrNull()
		{
			Alternative result = GetFirstUncertainAlternativeOrNull();
			if (result == null) { return null; }
			foreach (var alternative in alternatives)
			{
				if (alternative.Informativeness > result.Informativeness)
				{
					result = alternative;
				}
			}
			return result;
		}

		private bool IsContainedInGoodAlternatives(Alternative alternative)
		{
			foreach (var good in goodAlternatives)
			{
				if (alternative.Equals(good))
				{
					return true;
				}
			}
			return false;
		}

		private bool IsContainedInBadAlternatives(Alternative alternative)
		{
			foreach (var bad in badAlternatives)
			{
				if (alternative.Equals(bad))
				{
					return true;
				}
			}
			return false;
		}

		public void DivideOnGoodAndBadGroups()
		{
			foreach (var alternative in alternatives)
			{
				if (alternative.Group == Group.Good &&
					!IsContainedInGoodAlternatives(alternative))
				{
					goodAlternatives.Add(alternative);
				}
				else if (alternative.Group == Group.Bad &&
					!IsContainedInBadAlternatives(alternative))
				{
					badAlternatives.Add(alternative);
				}
			}
		}

		public bool HasAnyElementUncertainGroup()
		{
			foreach (var alternative in alternatives)
			{
				if (alternative.Group == Group.Uncertain)
				{
					return true;
				}
			}
			return false;
		}
	}
}
