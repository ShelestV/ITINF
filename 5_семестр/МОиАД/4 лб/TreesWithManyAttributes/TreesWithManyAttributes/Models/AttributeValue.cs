using System;

namespace TreesWithManyAttributes.Models
{
    public class AttributeValue
    {
        private readonly int value;
        private bool _class;
        
        public object Value => isDefined ? _class : value;
        private bool isDefined;

        public AttributeValue(int value)
        {
            isDefined = false;
            this.value = value;
        }
        
        public void Define(bool _class)
        {
            isDefined = true;
            this._class = _class;
        }

        public int Compare(AttributeValue attributeValue)
        {
            if (attributeValue.isDefined || this.isDefined)
                throw new Exception();

            return value - attributeValue.value;
        }
        
        public override string ToString()
        {
            return !isDefined ? value.ToString() : _class ? "+" : "-";
        }
    }
}