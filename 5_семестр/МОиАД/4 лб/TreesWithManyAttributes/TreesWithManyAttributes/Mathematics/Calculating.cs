using System;
using System.Collections.Generic;
using System.Linq;

namespace TreesWithManyAttributes.Mathematics
{
    public class Calculating
    {
        public static IEnumerable<double> CalculateRapids(IEnumerable<bool> classes, IEnumerable<int> attributeValues)
        {
            var classesList = classes.ToList();
            var attributeValuesList = attributeValues.ToList();
            
            if (classesList.Count != attributeValuesList.Count)
                throw new ArgumentException();

            var rapids = new List<double>();
            var previousSign = classesList[0];
            int index = 0;
            for (var i = 1; i < classesList.Count; ++i)
            {
                if (previousSign == classesList[i]) continue;

                var rapid = (attributeValuesList[i] + attributeValuesList[i - 1]) / 2.0;
                Console.WriteLine($"{++index} rapid: {rapid}");
                rapids.Add(rapid);
                previousSign = classesList[i];
            }

            return rapids;
        }

        private static double CalculateEntropy(IEnumerable<ValueClass> classesValues, string entropyString)
        {
            if (classesValues == null)
                throw new ArgumentException();

            var classesValuesList = classesValues.ToList();

            var numberOfPositiveValues = 0;
            var numberOfNegativeValues = 0;
            foreach (var classValue in classesValuesList)
            {
                if (classValue.Class)
                    ++numberOfPositiveValues;
                else 
                    ++numberOfNegativeValues;
            }

            var particularAmounts = new[] {numberOfPositiveValues, numberOfNegativeValues};
            
            
            if (numberOfNegativeValues == 0 || numberOfPositiveValues == 0)
            {
                var number = numberOfNegativeValues == 0 ? numberOfPositiveValues : numberOfNegativeValues;
                Console.WriteLine($"Entropy({entropyString}) = - ({number} / {classesValuesList.Count}) * log2({number} / {classesValuesList.Count}) = 0");
                return 0;
            }
            
            Console.Write($"Entropy({entropyString}) =");
            foreach (var amount in particularAmounts)
                Console.Write(
                    $" - ({amount} / {classesValuesList.Count}) * log2({amount} / {classesValuesList.Count})");
            
            var result = -particularAmounts.Select(particularAmount => particularAmount / (double) classesValuesList.Count)
                .Select(quantity => quantity * Math.Log2(quantity)).Sum();
            Console.WriteLine($" = {Math.Round(1000 * result) / 1000}");
            return result;
        }
        
        public static double CalculateGainByRapid(IEnumerable<bool> classes, IEnumerable<int> attributesValues, double rapid, string attributeName)
        {
            if (classes == null || attributesValues == null || 
                classes.Count() != attributesValues.Count())
                throw new ArgumentException();

            var classesValuesList = GetValueClassEnumerable(classes, attributesValues);
            
            var listOfGreaterThenRapid = GetValuesByComparingWithValue(classesValuesList, rapid, true);
            var listOfLessThenRapid = GetValuesByComparingWithValue(classesValuesList, rapid, false);

            Console.WriteLine($"Gain(D, {attributeName}={rapid}) = Entropy(D) - ((|D > {rapid}| / |D|) * Entropy(D < {rapid}) + (|D < {rapid}| / |D|) * Entropy(D < {rapid}))");
            
            var result = CalculateEntropy(classesValuesList, "D") -
                         (listOfGreaterThenRapid.Count() / (double)classes.Count()) * CalculateEntropy(listOfGreaterThenRapid, $"D > {rapid}") -
                         (listOfLessThenRapid.Count() / (double)classes.Count()) * CalculateEntropy(listOfLessThenRapid, $"D < {rapid}");

            Console.WriteLine($"Gain(D, {attributeName}={rapid}) = {Math.Round(1000 * result) / 1000}");
            return result;
        }

        private static IEnumerable<ValueClass> GetValuesByComparingWithValue(IEnumerable<ValueClass> values, double value, bool isGreater)
        {
            return values.Where(listValue => listValue.Value > value == isGreater);
        }

        private static IEnumerable<ValueClass> GetValueClassEnumerable(IEnumerable<bool> classes, IEnumerable<int> attributesValues)
        {
            if (classes == null || attributesValues == null ||
                classes.Count() != attributesValues.Count())
                throw new ArgumentException();
            
            var classesValuesList = new List<ValueClass>();
            for (var i = 0; i < classes.Count(); i++)
                classesValuesList.Add(new ValueClass(attributesValues.ToList()[i], classes.ToList()[i]));

            return classesValuesList;
        }

        private class ValueClass
        {
            public int Value { get; }
            public bool Class { get; }

            public ValueClass(int value, bool _class)
            {
                Value = value;
                Class = _class;
            }
        }
    }
}