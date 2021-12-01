using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace tmo_5lb
{
    public partial class Form1 : Form
    {
        private GraphicPoint[] points;
        public GraphicPoint[] Points { set => points = value; }

        public Form1()
        {
            InitializeComponent();
        }

		private void button1_Click(object sender, EventArgs e)
		{
            var blackPen = new Pen(Color.Black, 1f);
            var redPen = new Pen(Color.Red, 2f);

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
			g.DrawLines(redPen, points.Select(p => p.ToPoint()).Select(p => new Point(10 + p.X, 310 - p.Y)).ToArray());
        }
	}
}