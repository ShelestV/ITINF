namespace BayesTheorema.Models
{
	public record BoolProperty
	{
		public string Name { get; init; }
		public bool Value { get; init; }

		public BoolProperty(string name, bool value)
		{
			Name = name;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Name}({(Value ? "yes" : "no")})";
		}
	}
}
