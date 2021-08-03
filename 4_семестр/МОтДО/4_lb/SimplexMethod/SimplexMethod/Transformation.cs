using System.Collections.Generic;

namespace SimplexMethod
{
	class Transformation
	{
		public static List<Expression> CopyExpressions(List<Expression> expressions)
		{
			var result = new List<Expression>();
			expressions.ForEach(e => result.Add(new Expression(e)));
			return result;
		}

		public static void ToCanonicView(List<Expression> expressions)
		{
			expressions.ForEach(ex => ToCanonicView(ex));
		}

		private static void ToCanonicView(Expression expression)
		{
			if (expression.Sign == Sign.EqualOrGreater)
			{
				expression.MultiplyOnMinusOne();
				expression.Sign = Sign.EqualOrLess;
			}
		}

		public static void ToEquations(List<Expression> expressions)
		{
			for (int i = 0; i < expressions.Count; ++i)
			{
				if (expressions[i].Sign != Sign.Equal)
				{
					if (expressions[i].Sign == Sign.EqualOrGreater)
					{
						ToCanonicView(expressions[i]);
					}
					AddBasis(expressions, i); // If sign is equal or less
				}
			}
		}

		private static void AddBasis(List<Expression> expressions, int indexOfBasisExpression)
		{
			// First ID of undefined = 1, not 0
			// So last ID = number of undefineds
			var lastID = expressions[0].NumberOfUndefineds;
			var lastIndex = lastID - 1;

			for (int i = 0; i < expressions.Count; ++i)
			{
				double coeficient = (i == indexOfBasisExpression) ? 1 : 0;
				expressions[i].Undefineds.Add(new Undefined(lastIndex, coeficient));
			}
		}
	}
}
