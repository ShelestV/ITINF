using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core;

public class MentionCollection : IEnumerable<Mention>
{
    private readonly IEnumerable<Mention> mentions;

    public int Count => this.mentions.Count();

    public MentionCollection() => 
        this.mentions = new List<Mention>();

    public MentionCollection(IEnumerable<Mention> mentions) => 
        this.mentions = mentions;

    public MentionCollection(params Mention[] mentions) => 
        this.mentions = mentions;

    public Mention this[int index]
    {
        get => this.mentions.ElementAt(index);
        set => this.mentions.Insert(index, value);
    }

    public IEnumerator<Mention> GetEnumerator()
    {
        foreach (var mention in this.mentions)
        {
            yield return mention;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public IEnumerable<Mention> GetData() => this.mentions;
}
