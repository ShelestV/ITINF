using BayesTheorema.Collections;
using BayesTheorema.Models;
using System;
using Xunit;

namespace BayesTheorema.Tests
{
	public class ModelDataTests
	{
		[Fact]
		public void CutAttributesTest()
		{
			ModelsData data = new ModelsData(
				new Model(0.108, new BoolProperty("Toothache", true), new BoolProperty("Catch", true), new BoolProperty("Cavity", true)),
				new Model(0.016, new BoolProperty("Toothache", true), new BoolProperty("Catch", true), new BoolProperty("Cavity", false)),
				new Model(0.012, new BoolProperty("Toothache", true), new BoolProperty("Catch", false), new BoolProperty("Cavity", true)),
				new Model(0.064, new BoolProperty("Toothache", true), new BoolProperty("Catch", false), new BoolProperty("Cavity", false)),
				new Model(0.072, new BoolProperty("Toothache", false), new BoolProperty("Catch", true), new BoolProperty("Cavity", true)),
				new Model(0.144, new BoolProperty("Toothache", false), new BoolProperty("Catch", true), new BoolProperty("Cavity", false)),
				new Model(0.008, new BoolProperty("Toothache", false), new BoolProperty("Catch", false), new BoolProperty("Cavity", true)),
				new Model(0.576, new BoolProperty("Toothache", false), new BoolProperty("Catch", false), new BoolProperty("Cavity", false))
			);

			var expected = new ModelsData(
				new Model(0.12, new BoolProperty("Toothache", true), new BoolProperty("Cavity", true)),
				new Model(0.08, new BoolProperty("Toothache", true), new BoolProperty("Cavity", false)),
				new Model(0.08, new BoolProperty("Toothache", false), new BoolProperty("Cavity", true)),
				new Model(0.72, new BoolProperty("Toothache", false), new BoolProperty("Cavity", false))
			);

			var actual = data.CutAttributes("Catch");

			Assert.Equal(expected.Count, actual.Count);
		}

		[Fact]
		public void CalculateProbabilityForTest()
		{
			var data = new ModelsData(
				new Model(0.12, new BoolProperty("Toothache", true), new BoolProperty("Cavity", true)),
				new Model(0.08, new BoolProperty("Toothache", true), new BoolProperty("Cavity", false)),
				new Model(0.08, new BoolProperty("Toothache", false), new BoolProperty("Cavity", true)),
				new Model(0.72, new BoolProperty("Toothache", false), new BoolProperty("Cavity", false))
			);

			var actual = data.CalculateProbabylityFor(new BoolProperty("Cavity", false), new BoolProperty("Toothache", true));

			Assert.Equal(0.0941, Math.Round(actual * 10000) / 10000);
		}
	}
}
