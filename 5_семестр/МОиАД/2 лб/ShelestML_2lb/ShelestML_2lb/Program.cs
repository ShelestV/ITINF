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

			var algo = new FindS(positiveMatrix);
			algo.CalculateHypotheses();

			foreach (var item in algo.ParticularHypotheses)
			{
				Console.Write(item + " ");
			}
			Console.WriteLine();

			  //--------------------------------------------------------//
			 //----------Candidate Elimination algorithm---------------//
			//--------------------------------------------------------//


		}
	}
}
