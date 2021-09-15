using System;
using System.ComponentModel;

namespace WinesML.Models
{
	public class WineType : INotifyPropertyChanged
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
