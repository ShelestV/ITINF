using System;
using SolutionTree.Models;
using System.Collections.Generic;
using System.Linq;
using SolutionTree.Servers;

namespace SolutionTree.Algorithms
{
	internal class ID3
	{
		private readonly TennisGamesDataService gamesService;

		public ID3(TennisGamesData games)
		{
			gamesService = new TennisGamesDataService(games);
		}

		public Trees.SolutionTree GetSolutionTree()
		{
			var attributeGainRatios = new Dictionary<string, double>();
			foreach (var attributeName in gamesService.GetAttributeNames())
			{
				attributeGainRatios.Add(attributeName, CalculateGainRatio(attributeName));
			}

			

			/*int i = 0;
			foreach (var attributeName in gamesService.GetAttributeNames())
			{
				Console.WriteLine(attributeName + " : " + attributeGainRatios[i]);
				++i;
			}*/
			
			return null;
		}
private double CalculateGainRatio(string attributeName)
		{
			double gain = CalculateInformationGain(attributeName);
			double split = CalculateSplitInfo(attributeName);
			
			return gain / split;
		}
		
		private double CalculateSplitInfo(string attributeName)
		{
			double result = 0.0;
			var attributeValues = gamesService.GetAttributeValues(attributeName).Distinct();
			foreach (var value in attributeValues)
			{
				double valueRows = gamesService.GetTotalAmountOfRowsForAttribute(attributeName, value);
				double totalRows = gamesService.GetAmountOfRows();

				result -= (valueRows / totalRows) * Math.Log2(valueRows / totalRows);
			}

			return result;
		}
		
		private double CalculateInformationGain(string attributeName)
		{
			double result = CalculateGeneralEntropy();
			var attributeValues = gamesService.GetAttributeValues(attributeName).Distinct();
			
			foreach (var value in attributeValues)
			{
				double valueRows = gamesService.GetTotalAmountOfRowsForAttribute(attributeName, value);
				double totalRows = gamesService.GetAmountOfRows();
				
				var calculateResult = CalculateAttributeEntropy(attributeName, value);

				result -= (valueRows / totalRows) * calculateResult;
			}

			return result;
		}

		private double CalculateGeneralEntropy()
		{
			double positiveCoefficient = 
				gamesService.GetAmountOfPositiveRows() / (double)gamesService.GetAmountOfRows();
			double negativeCoefficient = 
				gamesService.GetAmountOfNegativeRows() / (double)gamesService.GetAmountOfRows();
			
			return -(positiveCoefficient * Math.Log2(positiveCoefficient)) +
			       -(negativeCoefficient * Math.Log2(negativeCoefficient));
		}
		
		private double CalculateAttributeEntropy(string attributeName, object attributeValue)
		{
			double positiveRows = gamesService.GetAmountOfPositiveRowsForAttribute(attributeName, attributeValue);
			double negativeRows = gamesService.GetAmountOfNegativeRowsForAttribute(attributeName, attributeValue);
			double totalRows = gamesService.GetTotalAmountOfRowsForAttribute(attributeName, attributeValue);
			
			double positiveCoefficient = positiveRows / totalRows;
			double negativeCoefficient = negativeRows / totalRows;

			if (positiveCoefficient == 0 || negativeCoefficient == 0)
			{
				return 0.0;
			}
			
			return -(positiveCoefficient * Math.Log2(positiveCoefficient)) +
			       -(negativeCoefficient * Math.Log2(negativeCoefficient));
		}
	}
}
