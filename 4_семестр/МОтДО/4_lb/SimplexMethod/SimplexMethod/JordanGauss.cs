using System;
using System.Collections.Generic;
using System.Text;

namespace SimplexMethod
{
	class JordanGauss
	{
		private List<Expression> originalExpressions;
		private List<Expression> operateExpressions;

		public List<Expression> Equations { get => originalExpressions; }
		
		public JordanGauss(List<Expression> expressions)
		{
			originalExpressions = Transformation.CopyExpressions(expressions);
			operateExpressions = Transformation.CopyExpressions(expressions);
		}

		public void Operate()
		{
			if (!Rules.IsAllEquations(operateExpressions))
			{
				Transformation.ToCanonicView(operateExpressions);
				Transformation.ToEquations(operateExpressions);
			}

			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				if (operateExpressions[i].Undefineds[i].Coeficient != 0)
				{
					CalculateBySolvedElement(new Undefined(operateExpressions[i].Undefineds[i]));
				}
			}

		}

		private void CalculateBySolvedElement(Undefined solvedElement)
		{
			var previous = Transformation.CopyExpressions(operateExpressions);

			SetNullsToSolvedElementColumn(solvedElement);
			CalculateEquationWithSolvedElement(solvedElement);
			CalculateOtherElementsBySolvedElement(previous, solvedElement);
		}

		private void SetNullsToSolvedElementColumn(Undefined solvedElement)
		{
			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				if (i != solvedElement.Index)
				{
					operateExpressions[i].ChangeCoeficientToElementByIndex(0, solvedElement.Index);
				}
			}
		}

		private void CalculateEquationWithSolvedElement(Undefined solvedElement)
		{
			var solveEquation = operateExpressions[solvedElement.Index];
			double deleteElement = solvedElement.Coeficient;
			solveEquation.DeleteOnElement(deleteElement);
		}

		private void CalculateOtherElementsBySolvedElement(List<Expression> previous, Undefined solvedElement)
		{
			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				if (i == solvedElement.Index)
				{
					continue;
				}

				for (int j = 0; j < operateExpressions[i].Undefineds.Count; ++j)
				{
					if (j == solvedElement.Index)
					{
						continue;
					}

					operateExpressions[i].Undefineds[j].Coeficient = NewOtherElementCoeficient(solvedElement, previous, i, j);
				}
				operateExpressions[i].Free.Coeficient = NewFreeElement(solvedElement, previous, i);
			}	

		}

		private double NewOtherElementCoeficient(Undefined solvedElement, List<Expression> previous, int row, int column)
		{
			double a = previous[solvedElement.Index].Undefineds[column].Coeficient;
			double b = previous[row].Undefineds[solvedElement.Index].Coeficient;

			double xRS = previous[solvedElement.Index].Undefineds[solvedElement.Index].Coeficient;

			return previous[row].Undefineds[column].Coeficient - ((a * b) / xRS);
		}

		private double NewFreeElement(Undefined solvedElement, List<Expression> previous, int row)
		{
			double a = previous[solvedElement.Index].Free.Coeficient;
			double b = previous[row].Undefineds[solvedElement.Index].Coeficient;

			double xRS = previous[solvedElement.Index].Undefineds[solvedElement.Index].Coeficient;

			return previous[row].Free.Coeficient - ((a * b) / xRS);
		}

		public List<Expression> GetResult()
		{
			return Transformation.CopyExpressions(operateExpressions);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			operateExpressions.ForEach(e => sb.Append(e.ToString()).Append(Environment.NewLine));
			return sb.ToString();
		}
	}
}
