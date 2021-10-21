using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolutionTree.Algorithms;
using SolutionTree.Models;

namespace SolutionTree.Trees.SolutionTree
{
    internal class Node
    {
        private readonly string attributeName;
        private readonly object attributeValue;
        private readonly ICollection<Node> subsequence;

        private int amountOfPositiveNodes;
        private int amountOfNegativeNodes;

        public Node(string attributeName, object attributeValue = null)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
            subsequence = new List<Node>();
        }

        public void BuildTree(IEnumerable<TennisGame> games)
        {
            var tennisGames = games.ToList();
            CalculatePositivity(tennisGames);
            
            var result = tennisGames.ToList()
                .GroupBy(game => game.GetAttributeValueByName(attributeName))
                .Select(group => new
                {
                    AttributeValue = group.Key,
                    Count = group.Count(),
                    Games = group.Select(game => game),
                    HaveUncertainty = group.Select(game => game).All(game => game.PlayTennis) 
                                      || group.Select(game => game).All(game => !game.PlayTennis)
                });
			
            foreach (var group in result)
            {
                //TODO => Calculate new root attribute
                var algo = new ID3(new TennisGamesData(group.Games));
                var attributePriorities = algo.CalculateGainRatioForAllElements();
                var node = new Node(attributePriorities.GetFirst().Key, group.AttributeValue);
                
                subsequence.Add(node);
                if (group.Games.Count() > 1 && !group.HaveUncertainty)
                {
                    node.BuildTree(group.Games);
                }
                else
                {
                    node.CalculatePositivity(group.Games);
                }
            }
        }

        private void CalculatePositivity(IEnumerable<TennisGame> games)
        {
            foreach (var game in games)
            {
                if (game.PlayTennis) ++amountOfPositiveNodes;
                else ++amountOfNegativeNodes;
            }
        }

        public void ConsoleOutput()
        {
            ConsoleOutput(0);
        }
        
        private void ConsoleOutput(int numberOfTabs)
        {
            Console.WriteLine(ToString(numberOfTabs));
            foreach(var node in subsequence) node.ConsoleOutput(numberOfTabs + 1);
        }
        
        private string ToString(int numberOfTabs)
        {
            var result = new StringBuilder();
            for (int i = 0; i < numberOfTabs; ++i) result.Append('\t');
            
            result.Append(attributeValue);
            
            var haveUncertainty = amountOfPositiveNodes != 0 && amountOfNegativeNodes != 0;
            if (haveUncertainty)
            {
                result.Append(attributeValue is not null ? " : " : "").Append(attributeName)
                    .Append('(').Append(amountOfPositiveNodes).Append('/').Append(amountOfNegativeNodes).Append(')');
            }
            else
            {
                result.Append(" => ").Append(amountOfPositiveNodes != 0 ? "Positive result" : "Negative result");
            }
            
            return result.ToString();
        }
    }
}