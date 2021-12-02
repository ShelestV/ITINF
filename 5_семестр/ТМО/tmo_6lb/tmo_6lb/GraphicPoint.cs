using System;
using System.Drawing;

namespace tmo_6lb
{
	public struct GraphicPoint
	{
		public int MaxK;

		public int K { get; }
		public double Time { get; }

		public GraphicPoint(int k, double time, int maxK)
		{
			K = k;
			Time = time;
			MaxK = maxK;
		}

		public Point ToPoint()
		{
			return new Point((int)Math.Round(Time * 3), (K * 300) / MaxK);
		}

		public override string ToString()
		{
			return $"k = {K}; Time = {Time}";
		}
	}
}
