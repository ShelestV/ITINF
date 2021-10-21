using System.Collections.Generic;
using System.Linq;
using SolutionTree.Models;

namespace SolutionTree.Trees.SolutionTree
{
    internal class Node
    {
        public string AttributeName { get; }
        private Node Previous { get; }
        public object AttributeValue { get; }
        public ICollection<Node> Subsequence { get; }
        
        public int AmountOfPositiveNodes { get; private set; }
        public int AmountOfNegativeNodes { get; private set; }

        public Node(string attributeName, Node previous = null, object attributeValue = null)
        {
            AttributeName = attributeName;
            Previous = previous;
            AttributeValue = attributeValue;
            Subsequence = new List<Node>();
        }

        public void BuildTree(IEnumerable<TennisGame> games)
        {
            var result = games.ToList()
                .GroupBy(game => game.GetAttributeValueByName(AttributeName))
                .Select(group => new
                {
                    AttributeValue = group.Key,
                    Count = group.Count(),
                    Games = group.Select(game => game)
                });
			
            foreach (var group in result)
            {
                var node = new Node(AttributeName, this, group.AttributeValue);
                Subsequence.Add(node);
                node.CalculatePositivity(group.Games);
                //TODO => Calculate new root attribute
                node.BuildTree(group.Games);
            }
        }

        private void CalculatePositivity(IEnumerable<TennisGame> games)
        {
            foreach (var game in games)
            {
                if (game.PlayTennis) ++AmountOfPositiveNodes;
                else ++AmountOfNegativeNodes;
            }
        }
    }
}