using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplexMethod
{
	class Simplex
	{
		private ObjectiveFunction originalFunction;
		private ObjectiveFunction operateFunction;

		private List<Expression> originalExpressions;
		private List<Expression> operateExpressions;

		private Undefined[] basises;

		private int indexOfMaxColumn;
		private int indexOfMinRow;

		public ObjectiveFunction F { get => originalFunction; }
		public List<Expression> Equations { get => originalExpressions; }

		public Simplex(List<Expression> expressions, ObjectiveFunction F)
		{
			originalExpressions = Transformation.CopyExpressions(expressions);
			operateExpressions = Transformation.CopyExpressions(expressions);

			this.originalFunction = new ObjectiveFunction(F);
			this.operateFunction = new ObjectiveFunction(F);
			basises = new Undefined[expressions.Count];

			indexOfMaxColumn = -1;
			indexOfMinRow = -1;
		}

		public void Operate()
		{
			if (!Rules.IsAllEquations(operateExpressions))
			{
				Transformation.ToCanonicView(operateExpressions);
				Transformation.ToEquations(operateExpressions);
			}

			// First iteration is Jordan exceptions
			var jordanExceprion = new JordanGauss(operateExpressions);
			jordanExceprion.Operate();
			operateExpressions = jordanExceprion.GetResult();

			SetBasises();
			CalculateNewObjectiveFunction();
			indexOfMaxColumn = GetIndexOfMaxColumn();
			indexOfMinRow = GetIndexOfMinRow();

			// Other iterations are simplex transformations
			while (!IsOptimalPlan())
			{
				SimplexTransformations();

				SetBasises();
				CalculateNewObjectiveFunction();
				indexOfMaxColumn = GetIndexOfMaxColumn();
				indexOfMinRow = GetIndexOfMinRow();
			}
		}

		private void SetBasises()
		{
			for (int j = 0; j < operateFunction.NumberOfUndefineds; ++j)
			{
				double countOfNotZeroes = 0;
				int indexOfNotZero = -1;
				for (int i = 0; i < operateExpressions.Count; ++i)
				{
					if (operateExpressions[i].Undefineds[j].Coeficient != 0)
					{
						indexOfNotZero = i;
						++countOfNotZeroes;
					}
				}
				if (countOfNotZeroes == 1)
				{
					basises[indexOfNotZero] = new Undefined(j, 1);
				}
			}
		}

		private void CalculateNewObjectiveFunction()
		{
			for (int j = 0; j < operateFunction.NumberOfUndefineds; ++j)
			{
				double x = 0;
				for (int i = 0; i < operateExpressions.Count; ++i)
				{
					x += operateExpressions[i].Undefineds[j].Coeficient * originalFunction.Undefineds[basises[i].Index].Coeficient;
				}
				operateFunction.Undefineds[j].Coeficient = x - originalFunction.Undefineds[j].Coeficient;
			}

			double free = 0;
			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				free += operateExpressions[i].Free.Coeficient * originalFunction.Undefineds[basises[i].Index].Coeficient;
			}
			operateFunction.Free.Coeficient = free - originalFunction.Free.Coeficient;
		}

		private int GetIndexOfMaxColumn()
		{
			double max;
			if (originalFunction.Objective == Objective.Min)
			{
				try
				{
					max = operateFunction.Undefineds.Where(x => x.Coeficient > 0).Select(x => Math.Abs(x.Coeficient)).Max();
				}
				catch (InvalidOperationException)
				{
					return -1;
				}
				return operateFunction.Undefineds.FindIndex(x => x.Coeficient > 0 && Math.Abs(x.Coeficient) == max);
			}
			try
			{
				max = operateFunction.Undefineds.Where(x => x.Coeficient < 0).Select(x => Math.Abs(x.Coeficient)).Max();
			}
			catch (InvalidOperationException)
			{
				return -1;
			} 
			return operateFunction.Undefineds.FindIndex(x => x.Coeficient < 0 && Math.Abs(x.Coeficient) == max);
		}

		private int GetIndexOfMinRow()
		{
			if (indexOfMaxColumn != -1)
			{
				List<double> Q = new List<double>();
				foreach (var expression in operateExpressions)
				{
					Q.Add(expression.Free.Coeficient / expression.Undefineds[indexOfMaxColumn].Coeficient);
				}

				double min;
				try
				{
					min = Q.Where(q => q > 0).Min();
				}
				catch (InvalidOperationException)
				{
					return -1;
				}

				return Q.FindIndex(q => q > 0 && q == min);
			}
			return -1;
		}

		private bool IsOptimalPlan()
		{
			return operateFunction.IsOptimal();
		}

		private void SimplexTransformations()
		{
			var previous = Transformation.CopyExpressions(operateExpressions);
			var solvedElement = previous[indexOfMinRow].Undefineds[indexOfMaxColumn];

			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				if (i == indexOfMinRow)
				{
					for (int j = 0; j < operateExpressions[i].NumberOfUndefineds; ++j)
					{
						operateExpressions[i].Undefineds[j].Coeficient = 
							previous[i].Undefineds[j].Coeficient / solvedElement.Coeficient;
					}
					operateExpressions[i].Free.Coeficient =
						previous[i].Free.Coeficient / solvedElement.Coeficient;
				}
				else
				{
					double c = -(previous[i].Undefineds[indexOfMaxColumn].Coeficient / solvedElement.Coeficient);

					for (int j = 0; j < operateExpressions[i].NumberOfUndefineds; ++j)
					{
						operateExpressions[i].Undefineds[j].Coeficient = 
							previous[i].Undefineds[j].Coeficient + previous[indexOfMinRow].Undefineds[j].Coeficient * c;
					}
					operateExpressions[i].Free.Coeficient =
						previous[i].Free.Coeficient + previous[indexOfMinRow].Free.Coeficient * c;
				}
			}
		}

		public string GetSolution()
		{
			var solutions = new List<double>();
			for (int j = 0; j < originalFunction.NumberOfUndefineds; ++j)
			{
				if (IsBasis(j))
				{
					for (int i = 0; i < originalExpressions.Count; ++i)
					{
						if (operateExpressions[i].Undefineds[j].Coeficient != 0)
						{
							solutions.Add(operateExpressions[i].Free.Coeficient);
						}
					}
				}
				else
				{
					solutions.Add(0);
				}
			}

			double F = operateFunction.Free.Coeficient;

			StringBuilder sb = new StringBuilder();
			sb.Append("F : ");
			for (int i = 0; i < solutions.Count; ++i)
			{
				sb.Append("(").Append(originalFunction.Undefineds[i].Coeficient).Append(")*(").Append(solutions[i]).Append(")").Append((i == solutions.Count - 1) ? " = " : " + ");
			}
			sb.Append(F).Append(Environment.NewLine);

			for (int i = 0; i < solutions.Count; ++i)
			{
				sb.Append("x").Append(i + 1).Append(" = ").Append(solutions[i]).Append(Environment.NewLine);
			}
			sb.Append("F = ").Append(F);
			return sb.ToString();
			
		}

		public bool IsBasis(int index)
		{
			foreach (var basis in basises)
			{
				if (index == basis.Index)
				{
					return true;
				}
			}
			return false;
		}

		public List<List<string>> GetLastIteration()
		{
			var result = new List<List<string>>();

			var header = new List<string>();
			header.Add("Basis");
			foreach (var x in operateExpressions[0].Undefineds)
			{
				header.Add(x.ToStringWithoutCoeficient());
			}
			header.Add("b");
			result.Add(header);

			for (int i = 0; i < operateExpressions.Count; ++i)
			{
				var inter = new List<string>();
				inter.Add(basises[i].ToStringWithoutCoeficient());
				for (int j = 0; j < operateExpressions[i].NumberOfUndefineds; ++j)
				{
					inter.Add((Math.Round(operateExpressions[i].Undefineds[j].Coeficient * 1000) / 1000).ToString());
				}
				inter.Add((Math.Round(operateExpressions[i].Free.Coeficient * 1000) / 1000).ToString());
				result.Add(inter);
			}

			var function = new List<string>();
			function.Add("");
			foreach (var x in operateFunction.Undefineds)
			{
				function.Add((Math.Round(x.Coeficient * 1000) / 1000).ToString());
			}
			function.Add((Math.Round(operateFunction.Free.Coeficient * 1000) / 1000).ToString());
			result.Add(function);

			return result;
		}
	}
}
