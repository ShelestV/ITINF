using System;

namespace Core;

public struct Alternative : IModel, IAlternative
{
    public int Index { get; set; }
    public string Name { get; set; }
    public MentionCollection Mentions { get; init; }

    public override bool Equals(object? obj) => 
        obj is Alternative alternative && 
        this.Name.Equals(alternative.Name);

    public override int GetHashCode() => 
        HashCode.Combine(this.Name);

    public override string? ToString() => 
        this.Name + $" {{ {string.Join(", ", this.Mentions)} }}";

    public ClassAlternative ToClassAlternative()
    {
        return new ClassAlternative
        {
            Index = this.Index,
            Name = this.Name,
            Mentions = this.Mentions,
            Group = AlternativeGroup.Undefined
        };
    }

    public Alternative Copy()
    {
        return new Alternative
        {
            Index = this.Index,
            Name = this.Name,
            Mentions = this.Mentions.Copy()
        };
    }
}
