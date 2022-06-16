using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace _2_lb_TPR
{
	class Program
	{
		private static ReadOnlyCollection<int> iterations;

		private static ListOfAlternatives alternatives;
		private static List<Criteria> criterias;
		private static int EnterIntValue(string nameOfValue)
		{
			int result;
			string input;
			bool isFirstTry = true;

			do
			{
				if (!isFirstTry) { Console.WriteLine("Try again!"); }

				Console.WriteLine("Enter amount of " + nameOfValue + ": ");
				input = Console.ReadLine();

				isFirstTry = false;

			} while (!Rules.isCorrectValue(input, out result));

			return result;
		}

		private static string GetGroup(Alternative alternative)
		{
			string group = ((int)alternative.Group).ToString();
			if (alternative.Group == Group.Uncertain)
			{
				group = "1,2";
			}
			return group;
		}

		private static double GetDistanceToGoodCenter(Alternative alternative, ref double D)
		{
			var goodCenter = alternatives.CalculateGoodCenter(alternatives.GoodAlternatives);

			alternative.DistanceToGoodCenter = alternative.CalculateDistanceTo(goodCenter);
			if (alternative.DistanceToGoodCenter > D)
			{
				D = alternative.DistanceToGoodCenter;
			}

			return alternative.DistanceToGoodCenter;
		}

		private static double GetDistanceToBadCenter(Alternative alternative, ref double D)
		{
			var badCenter = alternatives.CalculateBadCenter(alternatives.BadAlternatives);

			alternative.DistanceToBadCenter = alternative.CalculateDistanceTo(badCenter);
			if (alternative.DistanceToBadCenter > D)
			{
				D = alternative.DistanceToBadCenter;
			}

			return alternative.DistanceToBadCenter;
		}

		private static double GetProximityToGoodCenter(Alternative alternative, double D)
		{
			if (alternative.Group == Group.Good) { return 1; }
			if (alternative.Group == Group.Bad) { return 0; }

			double d1 = alternative.DistanceToGoodCenter;
			double d2 = alternative.DistanceToBadCenter;

			alternative.ProximityToGoodCenter = (D - d1) / (D - d1 + D - d2);
			return alternative.ProximityToGoodCenter;
		}

		private static double GetProximityToBadCenter(Alternative alternative, double D)
		{
			if (alternative.Group == Group.Good) { return 0; }
			if (alternative.Group == Group.Bad) { return 1; }

			double d1 = alternative.DistanceToGoodCenter;
			double d2 = alternative.DistanceToBadCenter;

			alternative.ProximityToBadCenter = (D - d2) / (D - d1 + D - d2);
			return alternative.ProximityToBadCenter;
		}

		private static double GetInformativenessOfGood(Alternative alternative)
		{
			alternative.InformativenessOfGood = alternative.ProximityToGoodCenter * alternative.GetCountOfBetterAlternatives(alternatives);
			return alternative.InformativenessOfGood;
		}

		private static double GetInformativenessOfBad(Alternative alternative)
		{
			alternative.InformativenessOfBad = alternative.ProximityToBadCenter * alternative.GetCountOfWorseAlternatives(alternatives);
			return alternative.InformativenessOfBad;
		}

		private static double Round(double value)
		{
			return Math.Round(value * 1000) / 1000;
		}

		private static List<List<string>> GetIterationTable(int numberOfRows, int numberOfColumns, out Alternative maxInformativenessAlternative)
		{
			var table = new string[numberOfRows][];
			for (int i = 0; i < table.Length; ++i)
			{
				table[i] = new string[numberOfColumns];
			}

			double D = 0;
			for (int j = 0; j < numberOfColumns; ++j)
			{
				if (j == 0) { table[0][j] = "№"; }
				if (1 <= j && j <= criterias.Count) { table[0][j] = criterias[j - 1].Name; }
				if (j == criterias.Count + 1) { table[0][j] = "G"; }
				if (j == criterias.Count + 2) { table[0][j] = "d1"; }
				if (j == criterias.Count + 3) { table[0][j] = "d2"; }
				if (j == criterias.Count + 4) { table[0][j] = "p1"; }
				if (j == criterias.Count + 5) { table[0][j] = "p2"; }
				if (j == criterias.Count + 6) { table[0][j] = "g1"; }
				if (j == criterias.Count + 7) { table[0][j] = "g2"; }
				if (j == criterias.Count + 8) { table[0][j] = "F1"; }
				if (j == criterias.Count + 9) { table[0][j] = "F2"; }
				if (j == criterias.Count + 10) { table[0][j] = "F"; }

				for (int i = 1; i < numberOfRows; ++i)
				{
					if (j == 0) { table[i][j] = i.ToString(); }                                                                                                              // Number of row
					else if (0 < j && j <= criterias.Count) { table[i][j] = alternatives.Alternatives[i - 1].Mentions[j - 1].Name; }                                         // Mention
					else if (j == criterias.Count + 1) { table[i][j] = GetGroup(alternatives.Alternatives[i - 1]); }                                                         // Group G
					else if (j == criterias.Count + 2) { table[i][j] = Round(GetDistanceToGoodCenter(alternatives.Alternatives[i - 1], ref D)).ToString(); }                 // Distance to good center d1
					else if (j == criterias.Count + 3) { table[i][j] = Round(GetDistanceToBadCenter(alternatives.Alternatives[i - 1], ref D)).ToString(); }                  // Distance to bad center d2
					else if (j == criterias.Count + 4) { table[i][j] = Round(GetProximityToGoodCenter(alternatives.Alternatives[i - 1], D)).ToString(); }                    // Proximity to good center p1
					else if (j == criterias.Count + 5) { table[i][j] = Round(GetProximityToBadCenter(alternatives.Alternatives[i - 1], D)).ToString(); }                     // Proximity to bad center p2
					else if (j == criterias.Count + 6) { table[i][j] = alternatives.Alternatives[i - 1].GetCountOfBetterAlternatives(alternatives).ToString(); }             // Number of better alternatives then this
					else if (j == criterias.Count + 7) { table[i][j] = alternatives.Alternatives[i - 1].GetCountOfWorseAlternatives(alternatives).ToString(); }              // Number of worse alternatives then this
					else if (j == criterias.Count + 8) { table[i][j] = Round(GetInformativenessOfGood(alternatives.Alternatives[i - 1])).ToString(); }                       // Informativeness of good
					else if (j == criterias.Count + 9) { table[i][j] = Round(GetInformativenessOfBad(alternatives.Alternatives[i - 1])).ToString(); }                        // Informativeness of bad
					else if (j == criterias.Count + 10) { table[i][j] = Round(alternatives.Alternatives[i - 1].Informativeness).ToString(); }                                // Informativeness
				}
			}

			maxInformativenessAlternative = alternatives.GetUncertainAlternativeWithMaxInformativenessOrNull();

			var result = new List<List<string>>();
			for (int i = 0; i < numberOfRows; ++i)
			{
				var interList = new List<string>();
				for (int j = 0; j < numberOfColumns; ++j)
				{
					interList.Add(table[i][j]);
				}
				result.Add(interList);
			}

			return result;
		}

		private static void Iteration(ref int indexOfIteration)
		{
			int numberOfColumns = criterias.Count + 11; // 11 is number of calculating columns and numeric row
			int numberOfRows = alternatives.Alternatives.Count + 1; // 1 is a header

			Alternative maxInformativenessAlternative;
			var tabel = GetIterationTable(numberOfRows, numberOfColumns, out maxInformativenessAlternative);

			if (maxInformativenessAlternative != null)
			{
				maxInformativenessAlternative.Group = (Group)iterations[indexOfIteration];
				if (iterations[indexOfIteration] == 1)
				{
					foreach (var alternative in maxInformativenessAlternative.GetBetterAlternatives(alternatives))
					{
						if (alternative.Group == Group.Uncertain)
						{
							alternative.Group = Group.Good;
						}
					}
				}
				else if (iterations[indexOfIteration] == 2)
				{
					foreach (var alternative in maxInformativenessAlternative.GetWorseAlternatives(alternatives))
					{
						if (alternative.Group == Group.Uncertain)
						{
							alternative.Group = Group.Bad;
						}
					}
				}
			}

			Console.WriteLine("\n" + (indexOfIteration + 1).ToString() + " iteration\n");
			foreach (var row in tabel)
			{
				foreach (var str in row)
				{
					Console.Write(str + "\t");
				}
				Console.WriteLine();
			}

			++indexOfIteration;
		}

		static void Main(string[] args)
		{
			iterations = new ReadOnlyCollection<int>(new List<int> { 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });

			criterias = new List<Criteria>();

			int numberOfCriterias = EnterIntValue("criterias");

			// Input number of criterias
			for (int i = 0; i < numberOfCriterias; ++i)
			{
				criterias.Add(new Criteria("K" + (i + 1).ToString()));
			}

			// For every criteria input number of mentions
			for (int i = 0; i < numberOfCriterias; ++i)
			{
				criterias[i].NumberOfMentions = EnterIntValue("mentions of " + (i + 1).ToString() + " criteria");
			}
			alternatives = new ListOfAlternatives(criterias);

			int indexOfIteration = 0;

			while (alternatives.HasAnyElementUncertainGroup())
			{
				Iteration(ref indexOfIteration);
			}

			Iteration(ref indexOfIteration);
		}
	}
}
