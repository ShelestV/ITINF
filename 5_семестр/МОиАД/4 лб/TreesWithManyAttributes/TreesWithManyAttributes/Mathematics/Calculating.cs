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
            for (var i = 1; i < classesList.Count; ++i)
            {
                if (previousSign == classesList[i]) continue;
                
                rapids.Add((attributeValuesList[i] + attributeValuesList[i - 1]) / 2.0);
                previousSign = classesList[i];
            }

            return rapids;
        }

        private static double CalculateEntropy(IEnumerable<ValueClass> classesValues)
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

            if (numberOfNegativeValues == 0 || numberOfPositiveValues == 0)
                return 0;
            
            return -(new [] { numberOfPositiveValues, numberOfNegativeValues }
                .Select(particularAmount => particularAmount / (double) classesValuesList.Count)
                .Select(quantity => quantity * Math.Log2(quantity)).Sum());
        }
        
        public static double CalculateGainByRapid(IEnumerable<bool> classes, IEnumerable<int> attributesValues, double rapid)
        {
            if (classes == null || attributesValues == null || 
                classes.Count() != attributesValues.Count())
                throw new ArgumentException();

            var classesValuesList = GetValueClassEnumerable(classes, attributesValues);
            
            var listOfGreaterThenRapid = GetValuesByComparingWithValue(classesValuesList, rapid, true);
            var listOfLessThenRapid = GetValuesByComparingWithValue(classesValuesList, rapid, false);

            return CalculateEntropy(classesValuesList) -
                   (listOfGreaterThenRapid.Count() / (double)classes.Count()) * CalculateEntropy(listOfGreaterThenRapid) -
                   (listOfLessThenRapid.Count() / (double)classes.Count()) * CalculateEntropy(listOfLessThenRapid);
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