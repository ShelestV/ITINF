using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic
{
	public partial class Form1 : Form
	{
		public GraphicPoint[] Points { get; set; }

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var points = lb_3.Program.Points;

			var blackPen = new Pen(Color.Black, 1f);
			var redPen = new Pen(Color.Red, 2f);
			var bluePen = new Pen(Color.Blue, 2f);

			var g = graphic.CreateGraphics();
			#region X coordinate line
			g.DrawLine(blackPen, new Point(10, 0), new Point(10, 310));
			g.DrawLine(blackPen, new Point(5, 15), new Point(10, 0));
			g.DrawLine(blackPen, new Point(15, 15), new Point(10, 0));
			#endregion
			#region Y coordinate line
			g.DrawLine(blackPen, new Point(10, 310), new Point(620, 310));
			g.DrawLine(blackPen, new Point(605, 305), new Point(620, 310));
			g.DrawLine(blackPen, new Point(605, 315), new Point(620, 310));
			#endregion
			if (points != null && points.Count > 0)
				g.DrawLines(redPen, points.Select(p => new GraphicPoint(p)).Select(p => new Point(10 + p.X, 310 - p.Y)).ToArray());
		}
	}
}
