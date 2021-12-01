namespace tmo_5lb.Mathematics
{
	struct MathGraphicPoint
	{
		public static int MaxK;

		public int K { get; }
		public double P { get; }

		public MathGraphicPoint(int k, double p)
		{
			K = k;
			P = p;
		}

		public GraphicPoint ToGraphicPoint()
		{
			return new GraphicPoint((600 * K) / MaxK, (int)(P * 300));
		}

		public override string ToString()
		{
			return $"k = {K}; Pk = {P}";
		}
	}
}
