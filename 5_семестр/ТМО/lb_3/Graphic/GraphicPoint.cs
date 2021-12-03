using System.Drawing;

namespace Graphic
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

		public GraphicPoint(lb_3.MathGraphicPoint point)
		{
			X = (600 * point.K) / lb_3.MathGraphicPoint.MaxK;
			Y = (int)(point.P * 300);
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
