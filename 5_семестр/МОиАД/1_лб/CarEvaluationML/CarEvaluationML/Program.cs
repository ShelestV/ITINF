﻿using System;

namespace CarEvaluationML
{
	class Program
	{
		static void Main(string[] args)
		{
			var ui = new UI();
			ui.Start();

			while (ui.IsWorking)
			{
				ui.Communicate();
			}
		}
	}
}
