using System.Collections.Generic;
using System.Linq;

namespace TransportProblem.Models
{
	enum Direction
	{ 
		Stop,
		Up,
		Right,
		Down,
		Left
	}

	class PotentialMethod
	{
		private static Tarrif endAndStartLoopTarrif;

		public static void OptimisePlan(Tarrifs tarrifs, U[] u, V[] v)
		{
			int row = -1;
			int column = -1;

			int max = -1;

			for (int i = 0; i < tarrifs.NumberOfRows; ++i)
			{
				for (int j = 0; j < tarrifs.NumberOfColumns; ++j)
				{
					if (tarrifs[i, j].HasProduct)
						continue;

					int tarrifScore = u[i].Number + v[j].Number - tarrifs[i, j].Cost;

					if (tarrifScore > 0 &&
						((row == -1 && column == -1) || tarrifScore > max))
					{
						row = i;
						column = j;
						max = tarrifScore;
					}
				}
			}

			if (row != -1 && column != -1)
			{
				endAndStartLoopTarrif = tarrifs[row, column];

				var loop = GetLoop(tarrifs, Direction.Stop, row, column, new List<Tarrif>() { endAndStartLoopTarrif });
				loop.RemoveAt(loop.Count - 1);

				int min = loop[1].QuantityOfProduct;
				for (int i = 3; i < loop.Count; i += 2)
				{
					if (loop[i].QuantityOfProduct < min)
						min = loop[i].QuantityOfProduct;
				}

				for (int i = 0; i < loop.Count; i += 2)
				{
					if ((min == 0 && !loop[i].HasProduct) ||
						(min == 0 && loop[i].HasProduct && loop[i].QuantityOfProduct == 0))
					{
						loop[i].SetImaginary();
					}
					else
					{
						loop[i].QuantityOfProduct += min;
					}
					loop[i + 1].QuantityOfProduct -= min;
				}
			}
		}

		private static List<Tarrif> GetLoop(Tarrifs tarrifs, Direction whereCameFrom, int row, int column, List<Tarrif> loop)
		{
			int originalRow = row;
			int originalColumn = column;

			if (whereCameFrom != Direction.Down && whereCameFrom != Direction.Up)
			{
				var ups = GetUpNext(tarrifs, row, column);
				foreach (var up in ups)
				{
					var upLoop = new List<Tarrif>();
					upLoop.AddRange(loop);
					upLoop.Add(up);

					if (!IsContained(loop, up) && !up.Equals(endAndStartLoopTarrif))
						upLoop = GetLoop(tarrifs, Direction.Up, up.Row, up.Column, upLoop);

					if (IsContainedElements(upLoop) && upLoop.Last().Equals(endAndStartLoopTarrif))
						return upLoop;
				}

				row = originalRow;
				column = originalColumn;

				var downs = GetDownNext(tarrifs, row, column);
				foreach (var down in downs)
				{
					var downLoop = new List<Tarrif>();
					downLoop.AddRange(loop);
					downLoop.Add(down);

					if (!IsContained(loop, down) && !down.Equals(endAndStartLoopTarrif))
						downLoop = GetLoop(tarrifs, Direction.Down, down.Row, down.Column, downLoop);

					if (IsContainedElements(downLoop) && downLoop.Last().Equals(endAndStartLoopTarrif))
						return downLoop; 
				}
			}

			if (whereCameFrom != Direction.Left && whereCameFrom != Direction.Right)
			{
				var rights = GetRightNext(tarrifs, row, column);
				foreach (var right in rights)
				{
					var rightLoop = new List<Tarrif>();
					rightLoop.AddRange(loop);
					rightLoop.Add(right);

					if (!IsContained(loop, right) && !right.Equals(endAndStartLoopTarrif))
						rightLoop = GetLoop(tarrifs, Direction.Right, right.Row, right.Column, rightLoop);

					if (IsContainedElements(rightLoop) && rightLoop.Last().Equals(endAndStartLoopTarrif))
						return rightLoop;
				}

				row = originalRow;
				column = originalColumn;

				var lefts = GetLeftNext(tarrifs, row, column);
				foreach (var left in lefts)
				{
					var leftLoop = new List<Tarrif>();
					leftLoop.AddRange(loop);
					leftLoop.Add(left);

					if (!IsContained(loop, left) && !left.Equals(endAndStartLoopTarrif))
						leftLoop = GetLoop(tarrifs, Direction.Left, left.Row, left.Column, leftLoop);

					if (IsContainedElements(leftLoop) && leftLoop.Last().Equals(endAndStartLoopTarrif))
						return leftLoop;
				}
			}

			return new List<Tarrif>();
		}

		private static List<Tarrif> GetUpNext(Tarrifs tarrifs, int row, int column)
		{
			for (int i = row - 1; i > -1; --i)
			{
				if (endAndStartLoopTarrif.Equals(tarrifs[i, column]))
					return new List<Tarrif> { tarrifs[i, column] };
			}

			var result = new List<Tarrif>();
			for (int i = row - 1; i > -1; --i)
			{
				if (tarrifs[i, column].HasProduct)
					result.Add(tarrifs[i, column]);
			}
			return result;
		}

		private static List<Tarrif> GetRightNext(Tarrifs tarrifs, int row, int column)
		{
			for (int j = column + 1; j < tarrifs.NumberOfColumns; ++j)
			{
				if (endAndStartLoopTarrif.Equals(tarrifs[row, j]))
					return new List<Tarrif> { tarrifs[row, j] };
			}

			var result = new List<Tarrif>();
			for (int j = column + 1; j < tarrifs.NumberOfColumns; ++j)
			{
				if (tarrifs[row, j].HasProduct)
					result.Add(tarrifs[row, j]);
			}
			return result;
		}

		private static List<Tarrif> GetDownNext(Tarrifs tarrifs, int row, int column)
		{
			for (int i = row + 1; i < tarrifs.NumberOfRows; ++i)
			{
				if (endAndStartLoopTarrif.Equals(tarrifs[i, column]))
					return new List<Tarrif> { tarrifs[i, column] };
			}

			var result = new List<Tarrif>();
			for (int i = row + 1; i < tarrifs.NumberOfRows; ++i)
			{
				if (tarrifs[i, column].HasProduct)
					result.Add(tarrifs[i, column]);
			}
			return result;
		}


		private static List<Tarrif> GetLeftNext(Tarrifs tarrifs, int row, int column)
		{
			for (int j = column - 1; j > -1; --j)
			{
				if (endAndStartLoopTarrif.Equals(tarrifs[row, j]))
					return new List<Tarrif> { tarrifs[row, j] };
			}

			var result = new List<Tarrif>();
			for (int j = column - 1; j > -1; --j)
			{
				if (tarrifs[row, j].HasProduct)
					result.Add(tarrifs[row, j]);
			}
			return result;
		}

		private static bool IsContained(List<Tarrif> loop, Tarrif element)
		{
			foreach (var tarrif in loop)
			{
				if (element.Equals(tarrif))
					return true;
			}
			return false;
		}

		private static bool IsContainedElements(List<Tarrif> tarrifs)
		{
			return tarrifs.Count > 0;
		}
	}
}
