using System.Drawing;

namespace tmo_3lb_with_graphic
{
	public struct GraphicPoint
	{
		public int X { get; }
		public int Y { get; }

		public GraphicPoint(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point ToPoint()
		{
			return new Point(X, Y);
		}

		public override string ToString()
		{
			return $"x = {X}; y = {Y}";
		}
	}
}
