using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb_3
{
	public struct MathGraphicPoint
	{
		public static int MaxK;

		public int K { get; }
		public double P { get; }

		public MathGraphicPoint(int k, double p)
		{
			K = k;
			P = p;
		}

		public override string ToString()
		{
			return $"k = {K}; Pk = {P}";
		}
	}
}
