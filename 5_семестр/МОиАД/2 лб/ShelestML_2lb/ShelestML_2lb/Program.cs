using ShelestML_2lb.Algorithms;
using ShelestML_2lb.Models;
using System;
using System.Collections.Generic;

namespace ShelestML_2lb
{
	class Program
	{
		static void Main(string[] args)
		{
			  //--------------------------------------------------------//
			 //------------------Find-S algorithm----------------------//
			//--------------------------------------------------------//

			var positiveMatrix = new PositiveMatrix(new List<string> { "Соперник", "Играем", "Лидеры", "Дождь" });

			positiveMatrix.Add(new List<string> { "Выше", "Дома", "На месте", "Нет" });
			positiveMatrix.Add(new List<string> { "Выше", "Дома", "Пропускают", "Нет" });
			positiveMatrix.Add(new List<string> { "Ниже", "Дома", "Пропускают", "Нет" });

			var findS = new FindS(positiveMatrix);
			findS.CalculateHypotheses();

			Console.WriteLine("Find-S:");
			foreach (var item in findS.ParticularHypotheses)
			{
				Console.Write(item + " ");
			}
			Console.WriteLine("\n");

			  //--------------------------------------------------------//
			 //----------Candidate Elimination algorithm---------------//
			//--------------------------------------------------------//

			var matrix = new Matrix(new List<string> { "Соперник", "Играем", "Лидеры", "Дождь" });
			matrix.Add(new List<string> { "Выше", "Дома", "На месте", "Нет" }, true);
			matrix.Add(new List<string> { "Ниже", "В гостях", "Пропускают", "Нет" }, false);
			matrix.Add(new List<string> { "Ниже", "Дома", "Пропускают", "Да" }, false);
			matrix.Add(new List<string> { "Выше", "Дома", "Пропускают", "Нет" }, true);

			var candidateElimination = new CandidateElimination(matrix);
			candidateElimination.CalculateHypotheses();

			Console.WriteLine("Candidate elimination:");
			foreach (var item in candidateElimination.ParticularHypotheses)
			{
				Console.Write(item + " ");
			}
			Console.WriteLine("\n");
			foreach (var hypothesis in candidateElimination.GeneralHypotheses)
			{
				foreach (var item in hypothesis)
				{
					Console.Write(item + " ");
				}
				Console.WriteLine();
			}
		}
	}
}
