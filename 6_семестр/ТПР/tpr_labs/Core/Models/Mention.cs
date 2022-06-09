using System;

namespace Core;

public struct Mention : IComparable<Mention>
{
    public string Name { get; set; }
    public double Value { get; set; }

    public static string GetAlternativeName(Mention mention, Criteria criteria) => 
        mention.Name[..1] + (criteria.Index + 1) + mention.Name[1..];

    public override bool Equals(object? obj) => 
        obj is Mention mention && 
        this.Name == mention.Name;

    public override int GetHashCode() => 
        HashCode.Combine(this.Name);

    public static bool operator ==(Mention left, Mention right) => 
        left.Equals(right);

    public static bool operator !=(Mention left, Mention right) =>
        !left.Equals(right);

    public int CompareTo(Mention other)
    {
        if (this == other)
            return 0;

        if (this.Value > other.Value)
            return -1;

        if (this.Value < other.Value)
            return 1;

        return 0;
    }

    public override string? ToString() =>
        this.Name;

    public Mention Copy()
    {
        return new()
        {
            Name = this.Name,
            Value = this.Value
        };
    }
}
