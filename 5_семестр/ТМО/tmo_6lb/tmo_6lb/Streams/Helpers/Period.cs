namespace tmo_6lb.Streams.Helpers
{
	struct Period
	{
		public int Number { get; }
		public double StartTime { get; }
		public double EndTime { get; }

		public Period(double start, double end, int number)
		{
			StartTime = start;
			EndTime = end;
			Number = number;
		}
	}
}
