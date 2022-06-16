using System;
using System.Collections.Generic;
using System.Text;

namespace _2_lb_TPR
{
	enum Group
	{
		Uncertain = 0,
		Good = 1,
		Bad = 2
	}

	class Alternative
	{
		private Group group;
		private List<Mention> mentions;

		private ListOfAlternatives betterAlternatives;
		private ListOfAlternatives worseAlternatives;

		public Group Group { get => group; set => group = value; }
		public List<Mention> Mentions { get => mentions; }
		public double DistanceToGoodCenter { get; set; }
		public double DistanceToBadCenter { get; set; }
		public double ProximityToGoodCenter { get; set; }
		public double ProximityToBadCenter { get; set; }
		public double InformativenessOfGood { get; set; }
		public double InformativenessOfBad { get; set; }
		public double Informativeness { get => InformativenessOfGood + InformativenessOfBad; }

		public Alternative(List<Mention> mentions)
		{
			this.mentions = mentions;
			group = Group.Uncertain;

			betterAlternatives = new ListOfAlternatives();
			worseAlternatives = new ListOfAlternatives();
		}

		public void AddBetter(Alternative alternative)
		{
			if (!this.Equals(alternative) &&
				!worseAlternatives.IsContained(alternative.ToString()))
			{
				betterAlternatives.Add(alternative);
			}
		}

		public List<Alternative> GetBetterAlternatives(ListOfAlternatives alternatives)
		{
			this.CompareWithUncertainAlternatives(alternatives.Alternatives);
			return betterAlternatives.Alternatives;
		}

		public int GetCountOfBetterAlternatives(ListOfAlternatives alternatives)
		{
			if (this.group == Group.Uncertain)
			{
				this.CompareWithUncertainAlternatives(alternatives.Alternatives);
				return betterAlternatives.Alternatives.Count;
			}
			else
			{
				return 0;
			}
		}

		public List<Alternative> GetWorseAlternatives(ListOfAlternatives alternatives)
		{
			this.CompareWithUncertainAlternatives(alternatives.Alternatives);
			return worseAlternatives.Alternatives;
		}

		public int GetCountOfWorseAlternatives(ListOfAlternatives alternatives)
		{
			if (this.group == Group.Uncertain)
			{
				this.CompareWithUncertainAlternatives(alternatives.Alternatives);
				return worseAlternatives.Alternatives.Count;
			}
			else
			{
				return 0;
			}
		}

		public void AddWorse(Alternative alternative)
		{
			if (!this.Equals(alternative) &&
				!betterAlternatives.IsContained(alternative.ToString()))
			{
				worseAlternatives.Add(alternative);
			}
		}

		private static int GreaterOrLess(Alternative first, Alternative second, int index)
		{
			int compareResult = 0;
			if (first.mentions[index].isGreater(second.mentions[index]))
			{
				compareResult = -1;
			}
			else if (first.mentions[index].isLess(second.mentions[index]))
			{
				compareResult = 1;
			}
			return compareResult;
		}

		/// <summary>
		/// This method compare 2 alternatives
		/// </summary>
		/// <param name="other">Another alternative to compare</param>
		/// <returns>
		///  0 : if alternatives are not comparable;<br/>
		///  1 : if other alternative is worse;<br/>
		/// -1 : if other alternative is better.</returns>
		public int Compare(Alternative other)
		{
			// The order of the elements in the array is the same
			if (other is null || other.mentions is null) { throw new ArgumentNullException(); }
			if (this.mentions.Count != other.mentions.Count) { throw new Exception(); } // Not all elements will be compared

			int result = GreaterOrLess(this, other, 0);

			for (int i = 1; i < this.mentions.Count; ++i)
			{
				int compareResult = GreaterOrLess(this, other, i);

				if (compareResult == result || compareResult == 0) { continue; }

				if (result == 0)
				{
					result = compareResult;
				}
				else
				{
					return 0;
				}
			}

			return result;
		}

		public double CalculateDistanceTo(Alternative other)
		{
			if (other == null || this.mentions.Count != other.mentions.Count) { return -1; }
			double result = 0;
			for (int i = 0; i < this.mentions.Count; ++i)
			{
				result += this.mentions[i].CalculateDistanceTo(other.mentions[i]);
			}
			return result;
		}

		public void DivideMentionsValues(int number)
		{
			for (int i = 0; i < mentions.Count; ++i)
			{
				mentions[i].NumericName /= (double)number;
			}
		}

		public static Alternative operator +(Alternative first, Alternative second)
		{
			if (first != null && second != null &&
				first.Mentions.Count == second.Mentions.Count)
			{
				List<Mention> mentions = new List<Mention>();
				for (int i = 0; i < first.Mentions.Count; ++i)
				{
					mentions.Add(first.Mentions[i] + second.Mentions[i]);
				}
				foreach (var mention in mentions)
				{
					if (mention == null)
					{
						return null;
					}
				}
				return new Alternative(mentions);
			}
			return null;
		}

		public void CompareWithUncertainAlternatives(List<Alternative> alternatives)
		{
			betterAlternatives.Alternatives.Clear();
			worseAlternatives.Alternatives.Clear();
			foreach (var alternative in alternatives)
			{
				if (!this.Equals(alternative) &&
					alternative.Group == Group.Uncertain)
				{
					int compareResult = this.Compare(alternative);

					if (compareResult == -1)
					{
						this.AddBetter(alternative);
					}
					else if (compareResult == 1)
					{
						this.AddWorse(alternative);
					}
				}
			}
		}

		public override bool Equals(object obj)
		{
			// If Alternative is referenced to itself
			if (this == obj) { return true; }

			// We cannot compare values of different types
			// We cannot compare arrays if they have different lengths
			if (obj == null ||
				this.GetType() != obj.GetType() ||
				((Alternative)obj).mentions.Count != mentions.Count)
			{
				return false;
			}

			foreach (var objCriteriaValue in ((Alternative)obj).mentions)
			{
				// Arrays are different if array A don't containt value from array B and vice versa
				foreach (var thisCriteriaValue in this.mentions)
				{
					if (!thisCriteriaValue.Equals(objCriteriaValue))
					{
						return false;
					}
				}
			}

			// We have not found differences, so arrays are equal
			return true;
		}

		public override int GetHashCode()
		{
			// Has been generated by Visual Studio
			return -1321438047 + EqualityComparer<List<Mention>>.Default.GetHashCode(mentions);
		}

		public override string ToString()
		{
			StringBuilder str = new StringBuilder();
			mentions.ForEach(m => str.Append(m.ToString()));
			return str.ToString();
		}
	}
}
