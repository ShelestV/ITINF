using System;

namespace WinesML.Models
{
	public class WineType
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public override bool Equals(object obj)
		{
			return obj is WineType type &&
				   Id == type.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}
	}
}
