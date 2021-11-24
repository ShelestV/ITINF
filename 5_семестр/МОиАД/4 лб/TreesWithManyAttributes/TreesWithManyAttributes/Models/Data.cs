using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreesWithManyAttributes.Models
{
    public class Data
    {
        private readonly IEnumerable<string> attributeNames;
        private ICollection<ClassModel> rows;

        public ClassModel this[int index] 
        {
            get => rows.ToList()[index];
            set => rows.ToList()[index] = value;
        }

        public int Count => rows.Count;
        public int AttributesCount => attributeNames.Count();
        
        public Data(IEnumerable<string> attributeNames)
        {
            this.attributeNames = attributeNames;
            rows = new List<ClassModel>();
        }

        public void Add(ClassModel model)
        {
            if (model is not null && model.AttributeValues.Count() == attributeNames.Count())
            {
                model.AttributeNames = attributeNames.ToList();
                rows.Add(model);
            }
        }

        public string GetAttributeNameByIndex(int index) => attributeNames.ToList()[index];

        public IEnumerable<bool> GetClasses()
        {
            return rows.Select(row => row.Class);
        }

        public IEnumerable<int> GetValuesByAttributes(string attribute)
        {
            return rows.Select(row => (int)row[attribute].Value);
        }

        public void DefineAttribute(string attribute, double rapid)
        {
            foreach (var row in rows)
                row[attribute].Define((int) row[attribute].Value < rapid);
        }

        public void Swipe(int firstIndex, int secondIndex)
        {
            var copy = rows.ToList();
            (copy[firstIndex], copy[secondIndex]) = (copy[secondIndex], copy[firstIndex]);
            rows = copy;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Class\t");
            foreach (var name in attributeNames)
                sb.Append(name).Append('\t');

            foreach (var row in rows)
            {
                sb.Append('\n').Append(row.Class ? "+" : "-").Append('\t');
                foreach (var attribute in row.AttributeValues)
                    sb.Append(attribute).Append('\t');
            }

            return sb.ToString();
        }
    }
}