using System;
using System.Collections.Generic;

namespace SolutionTree.Models
{
	internal class TennisGame
	{
		public string Outlook { get; init; }
		public int Temperature { get; init; }
		public int Humidity { get; init; }
		public bool Wind { get; init; }

		public bool PlayTennis { get; init; }

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
