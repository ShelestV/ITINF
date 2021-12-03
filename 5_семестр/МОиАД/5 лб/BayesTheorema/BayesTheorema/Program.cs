using BayesTheorema.Collections;
using BayesTheorema.Models;
using System;

namespace BayesTheorema
{
	class Program
	{
		static void Main(string[] args)
		{
			#region Fill data
			string toothache = "Toothache";
			string @catch = "Catch";
			string cavity = "Cavity";

			var data = new ModelsData(
				new Model(0.108, new BoolProperty(toothache, true), new BoolProperty(@catch, true), new BoolProperty(cavity, true)),
				new Model(0.016, new BoolProperty(toothache, true), new BoolProperty(@catch, true), new BoolProperty(cavity, false)),
				new Model(0.012, new BoolProperty(toothache, true), new BoolProperty(@catch, false), new BoolProperty(cavity, true)),
				new Model(0.064, new BoolProperty(toothache, true), new BoolProperty(@catch, false), new BoolProperty(cavity, false)),
				new Model(0.072, new BoolProperty(toothache, false), new BoolProperty(@catch, true), new BoolProperty(cavity, true)),
				new Model(0.144, new BoolProperty(toothache, false), new BoolProperty(@catch, true), new BoolProperty(cavity, false)),
				new Model(0.008, new BoolProperty(toothache, false), new BoolProperty(@catch, false), new BoolProperty(cavity, true)),
				new Model(0.576, new BoolProperty(toothache, false), new BoolProperty(@catch, false), new BoolProperty(cavity, false))
			);
			#endregion

			data = data.CutAttributes(@catch);

			var result = data.CalculateProbabylityFor(new BoolProperty(cavity, false), new BoolProperty(toothache, true));

			Console.WriteLine($"P({new BoolProperty(cavity, false)}|{new BoolProperty(toothache, true)}) = {result}");
		}
	}
}
