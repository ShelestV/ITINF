using System;
using System.ComponentModel;
using WinesML.Models.Abstract;

namespace WinesML.Models
{
	public class WineType : BaseModel
	{
		private string name;

		public int Id { get; set; }
		public string Name 
		{
			get => name;
			set
			{
				name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public bool EqualsById(int id)
		{
			return Id == id;
		}

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
