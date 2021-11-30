using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace tmo_5lb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

		public void DrawFunctionGraphic(GraphicPoint[] points)
		{
            graphic.CreateGraphics().DrawLines(new Pen(Color.Black, 3f), points.Select(p => p.ToPoint()).ToArray());
        }
	}
}