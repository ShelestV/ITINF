using System.Collections.Generic;
using System.Text;

namespace _2_lb_TPR
{
    class Criteria
    {
        private string name;
        private List<Mention> mentions;

        public string Name { get => name; set => name = value; }
        public List<Mention> Mentions { get => mentions; }
        public int NumberOfMentions
        {
            get => mentions.Count;
            set
            {
                for (int i = 1; i <= value; ++i)
                {
                    this.mentions.Add(new Mention(this, i, value));
                }
            }
        }

        public Criteria(string name, int numberOfCriteriaValues)
        {
            this.name = name;
            this.mentions = new List<Mention>();
            for (int i = 1; i <= numberOfCriteriaValues; ++i)
            {
                this.mentions.Add(new Mention(this, i, numberOfCriteriaValues));
            }
        }

        public Criteria(string name)
        {
            this.name = name;
            this.mentions = new List<Mention>();
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            mentions.ForEach(m => str.Append(m));
            return str.ToString();
        }
    }
}
