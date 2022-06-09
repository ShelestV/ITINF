using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core;

public class MentionCollection : IEnumerable<Mention>
{
    private readonly IList<Mention> mentions;

    public int Count => this.mentions.Count;

    private MentionCollection() => 
        this.mentions = new List<Mention>();

    public MentionCollection(IEnumerable<Mention> mentions) => 
        this.mentions = mentions.ToList();

    public MentionCollection(params Mention[] mentions) => 
        this.mentions = mentions;

    public Mention this[int index]
    {
        get => this.mentions.ElementAt(index);
        set => this.mentions[index] = value;
    }

    public IEnumerator<Mention> GetEnumerator()
    {
        foreach (var mention in this.mentions)
        {
            yield return mention;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public MentionCollection Copy()
    {
        var mentions = new MentionCollection();
        foreach (var mention in this.mentions)
            mentions.Add(mention.Copy());
        return mentions;
    }

    private void Add(Mention mention)
    {
        this.mentions.Add(mention);
    }
}
