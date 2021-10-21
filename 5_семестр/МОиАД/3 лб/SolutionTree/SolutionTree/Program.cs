using SolutionTree.Algorithms;
using SolutionTree.Models;

namespace SolutionTree
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var data = new TennisGamesData();
			#region Adding data
			data.Games.Add(new TennisGame { Outlook = "Sunny",    Temperature = 29, Humidity = 85, Wind = false, PlayTennis = false });
			data.Games.Add(new TennisGame { Outlook = "Sunny",    Temperature = 27, Humidity = 90, Wind = true,  PlayTennis = false });
			data.Games.Add(new TennisGame { Outlook = "Overcast", Temperature = 28, Humidity = 78, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Rain",     Temperature = 21, Humidity = 96, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Rain",     Temperature = 20, Humidity = 80, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Rain",     Temperature = 18, Humidity = 70, Wind = true,  PlayTennis = false });
			data.Games.Add(new TennisGame { Outlook = "Overcast", Temperature = 18, Humidity = 65, Wind = true,  PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Sunny",    Temperature = 22, Humidity = 95, Wind = false, PlayTennis = false });
			data.Games.Add(new TennisGame { Outlook = "Sunny",    Temperature = 21, Humidity = 70, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Rain",     Temperature = 24, Humidity = 80, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Sunny",    Temperature = 24, Humidity = 70, Wind = true,  PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Overcast", Temperature = 22, Humidity = 90, Wind = true,  PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Overcast", Temperature = 27, Humidity = 75, Wind = false, PlayTennis = true  });
			data.Games.Add(new TennisGame { Outlook = "Rain",     Temperature = 22, Humidity = 80, Wind = true,  PlayTennis = false });
			#endregion
			
			var algo = new ID3(data);
			algo.GetSolutionTree();
		}
	}
}
