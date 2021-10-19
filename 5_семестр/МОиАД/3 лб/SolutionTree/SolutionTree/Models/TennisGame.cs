using System;
using System.Collections.Generic;

namespace SolutionTree.Models
{
	class TennisGame
	{
		public string Outlook { get; set; }
		public int Temperature { get; set; }
		public int Humidity { get; set; }
		public bool Wind { get; set; }

		public bool PlayTennis { get; set; }

		public object GetAttributeValueByName(string attributeName)
		{
			switch (attributeName)
			{
				case nameof(Outlook):
					return Outlook;
				case nameof(Temperature):
					return Temperature;
				case nameof(Humidity):
					return Humidity;
				case nameof(Wind):
					return Wind;
				case nameof(PlayTennis):
					return PlayTennis;
				default:
					throw new ArgumentException();
			}
		}
		
		
		public static IEnumerable<string> GetAttributeNames()
		{
			return new List<string>
			{
				nameof(Outlook), 
				nameof(Temperature), 
				nameof(Humidity), 
				nameof(Wind)
			};
		}
	}
}
