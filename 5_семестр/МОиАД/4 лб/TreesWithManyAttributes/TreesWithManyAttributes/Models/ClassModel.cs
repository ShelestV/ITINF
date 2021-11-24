using System;
using System.Collections.Generic;
using System.Linq;

namespace TreesWithManyAttributes.Models
{
    public class ClassModel
    {
        public bool Class { get; init; }
        public IEnumerable<string> AttributeNames { get; set; }
        public IEnumerable<AttributeValue> AttributeValues { get; init; }

        public AttributeValue this[string attributeName]
        {
            get
            {
                if (string.IsNullOrEmpty(attributeName))
                    throw new ArgumentException();
                
                for (var i = 0; i < AttributeNames.Count(); ++i)
                {
                    if (attributeName.Equals(AttributeNames.ToList()[i]))
                        return AttributeValues.ToList()[i];
                }

                throw new ArgumentException();
            }
        }
    }
}