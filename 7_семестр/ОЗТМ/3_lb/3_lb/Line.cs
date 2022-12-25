namespace _3_lb;

internal struct Line
{
	public static Pen Pen => new(Color.Black, 3f);

	public Point First { get; }
	public Point Last { get; }

	public Line(Point first, Point last)
	{
		First = first;
		Last = last;
	}
}
