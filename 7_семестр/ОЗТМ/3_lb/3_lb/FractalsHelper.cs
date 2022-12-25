using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_lb;

internal class FractalsHelper
{
	private const int BorderWidth = 5;

	private const int StartX = BorderWidth;
	private const int StartY = BorderWidth;

	private static readonly Point Start = new(StartX, StartY);

	private readonly int width;
	private readonly int height;

	public FractalsHelper(Size size)
	{
		// Borders in 5 pixels
		this.width = size.Width - (BorderWidth * 2);
		this.height = size.Height - (BorderWidth * 2);
	}

	public void Draw(Graphics graphics, int iterationsCount = 1)
	{
		if (iterationsCount <= 0)
			return;

		var location = new Point(Start.X, Start.Y);
		var size = new Size(width, height);

		var resultLines = new List<Line>();
		AddTriangle(resultLines, location, size, iterationsCount);

		foreach (var line in resultLines)
			graphics.DrawLine(Line.Pen, line.First, line.Last);
	}

	private void AddTriangle(ICollection<Line> lines, Point location, Size size, int iterationsCount, int iteration = 0)
	{
		var (a, b, c) = GetTriangleVertices(location, size);
		lines.Add(a);
		lines.Add(b);
		lines.Add(c);

		iteration++;

		if (iteration == iterationsCount)
			return;

		var newSize = new Size(size.Width / 2, size.Height / 2);

		var firstTriangleLocation = new Point(location.X, location.Y + (size.Height / 2));
		var secondTriangleLocation = new Point(location.X + (size.Width / 2), location.Y + (size.Height / 2));
		var thirdTriangleLocation = new Point(location.X + (size.Width / 4), location.Y);

		AddTriangle(lines, firstTriangleLocation, newSize, iterationsCount, iteration);
		AddTriangle(lines, secondTriangleLocation, newSize, iterationsCount, iteration);
		AddTriangle(lines, thirdTriangleLocation, newSize, iterationsCount, iteration);
	}

	private static (Line, Line, Line) GetTriangleVertices(Point start, Size size)
	{
		var a = new Point(start.X, start.Y + size.Height);
		var b = new Point(start.X + (size.Width / 2), start.Y);
		var c = new Point(start.X + size.Width, start.Y + size.Height);
		return (new(a, b), new(b, c), new(a, c));
	}
}
