using System.Text;

namespace BayesTheorema.Models
{
	public record Model
	{
		public BoolProperty[] Properties { get; init; }
		public double Probability { get; set; }

		public Model(double probability, params BoolProperty[] properties)
		{
			Probability = probability;
			Properties = properties;
		}

		public bool ContainsAllProperties(params BoolProperty[] properties)
		{
			var count = 0;
			foreach (var prop in Properties)
			{
				foreach (var paramProp in properties)
				{
					if (prop.Name.Equals(paramProp.Name) && prop.Value == paramProp.Value)
					{
						count++;
						break;
					}
				}
			}

			return count == properties.Length;
		}

		public override string ToString()
		{
			var str = new StringBuilder();

			for (int i = 0; i < Properties.Length; ++i)
				str.Append(Properties[i]).Append(i < Properties.Length - 1 ? "|" : "");

			return str.ToString();
		}
	}
}
