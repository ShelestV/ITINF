using BayesTheorema.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BayesTheorema.Collections
{
	public class ModelsData : IEnumerable
	{
		private Model[] models;

		public ModelsData(params Model[] models)
		{
			this.models = models;
		}

		public int Count => models.Length;

		/// <summary>
		/// Make new ModelsData from <c>this</c> without written attributes
		/// </summary>
		/// <param name="names">useless attributes</param>
		/// <returns></returns>
		public ModelsData CutAttributes(params string[] names)
		{
			ModelsData data = new ModelsData(models);
			foreach (var name in names)
				data = CutAttriburte(data, name);
			return data;
		}

		private ModelsData CutAttriburte(ModelsData data, string name)
		{
			var internalModels = new List<Model>();
			foreach (var dataModel in data.models)
			{
				var cutModel = GetCutModel(data, dataModel, name);
				bool isContinue = false;
				foreach (var internalModel in internalModels)
				{
					if (internalModel.ContainsAllProperties(cutModel.Properties))
						isContinue = true;
				}
				if (!isContinue) internalModels.Add(cutModel);
			}
			return new ModelsData(internalModels.Distinct().ToArray());
		}

		private Model GetCutModel(ModelsData data, Model model, string name)
		{
			var properties = model.Properties.Where(prop => !prop.Name.Equals(name)).ToArray();
			var probability = 0.0;
			var newModel = new Model(probability, properties);

			foreach (var dataModel in data.models)
			{
				if (dataModel.ContainsAllProperties(properties))
					probability += dataModel.Probability;
			}

			newModel.Probability = probability;
			return newModel;
		}

		public double CalculateProbabylityFor(BoolProperty mainProperty, BoolProperty supportProperty)
		{
			var probOfPosResOfMainProp = models.Where(m => m.Properties.Where(p => p.Name.Equals(mainProperty.Name) &&  p.Value).Count() > 0).Select(m => m.Probability).Sum();
			var probOfNegResOfMainProp = models.Where(m => m.Properties.Where(p => p.Name.Equals(mainProperty.Name) && !p.Value).Count() > 0).Select(m => m.Probability).Sum();

			var probOfSupportProp = models.Select(m => m.Probability * (m.Properties.Any(p => p.Name.Equals(mainProperty.Name) && p.Value) ? probOfPosResOfMainProp : probOfNegResOfMainProp)).Sum();

			var modelProbability = models.Where(m => m.ContainsAllProperties(mainProperty, supportProperty)).First().Probability;

			return (modelProbability * (mainProperty.Value ? probOfPosResOfMainProp : probOfNegResOfMainProp)) / probOfSupportProp;
		}

		public Model this[params BoolProperty[] properties]
		{
			get
			{
				return models.Where(m => m.ContainsAllProperties(properties)).FirstOrDefault();
			}
		}

		public IEnumerator GetEnumerator()
		{
			foreach (var model in models)
				yield return model;
		}
	}
}
