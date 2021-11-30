using System;
using System.Linq;
using TreesWithManyAttributes.Algorithms;
using TreesWithManyAttributes.Mathematics;
using TreesWithManyAttributes.Models;

namespace TreesWithManyAttributes
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialize data (contains both first and second variant and methods example)
            /* First variant 
            var data = new Data(new[] {"Temperature"});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(24) }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(28) }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(12) }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(18) }});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(20) }});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(22) }});
            */
            /* Second variant */
            var data = new Data(new[] {"A1", "A2"});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(7),  new AttributeValue(15)  }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(24), new AttributeValue(32)  }});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(8),  new AttributeValue(135) }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(75), new AttributeValue(56)  }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(38), new AttributeValue(24)  }});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(12), new AttributeValue(62)  }});
            /**/
            /* Methoda example
            var data = new Data(new[] {"A1", "A2"});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(2),  new AttributeValue(51) }});
            data.Add(new ClassModel { Class = true, AttributeValues = new [] { new AttributeValue(25), new AttributeValue(53) }});
            data.Add(new ClassModel { Class = false,  AttributeValues = new [] { new AttributeValue(11), new AttributeValue(50) }});
            data.Add(new ClassModel { Class = true, AttributeValues = new [] { new AttributeValue(4),  new AttributeValue(52) }});
            data.Add(new ClassModel { Class = false, AttributeValues = new [] { new AttributeValue(19), new AttributeValue(54) }});
            data.Add(new ClassModel { Class = true,  AttributeValues = new [] { new AttributeValue(13), new AttributeValue(55) }});
            */
            #endregion
            Console.WriteLine(data.ToString());
            Console.WriteLine();
            
            for (var i = 0; i < data.AttributesCount; ++i)
            {
                var attributeName = data.GetAttributeNameByIndex(i);
                Sorting.SortDataByAttribute(data, attributeName);
                Console.WriteLine("After sort");
                Console.WriteLine(data.ToString());
                Console.WriteLine();
                var classes = data.GetClasses().ToList();
                var attributeValues = data.GetValuesByAttributes(attributeName).ToList();

                var rapids = Calculating.CalculateRapids(classes, attributeValues).ToList();
                var gains = rapids
                    .Select(rapid => Calculating.CalculateGainByRapid(classes, attributeValues, rapid, attributeName))
                    .ToList();
                var maxGain = gains.Max();
                var indexOfMaxGain = gains.IndexOf(maxGain);
                
                Console.WriteLine($"{indexOfMaxGain + 1} rapid has max gain = {Math.Round(maxGain * 1000) / 1000}");
                
                data.DefineAttribute(attributeName, rapids[indexOfMaxGain]);

                Console.WriteLine(data.ToString());
                Console.WriteLine();
            }
        }
    }
}
