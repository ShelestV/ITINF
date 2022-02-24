using System;

namespace Core;

public struct Alternative : IAlternative
{
    public int Index { get; set; }
    public string Name { get; set; }
    public MentionCollection Mentions { get; init; }

    public override bool Equals(object? obj) => 
        obj is Alternative alternative && 
        this.Name.Equals(alternative.Name);

    public override int GetHashCode() => 
        HashCode.Combine(this.Name);

    public static bool operator ==(Alternative left, Alternative right) =>
        left.Equals(right);

    public static bool operator !=(Alternative left, Alternative right) =>
        !left.Equals(right);

    public override string? ToString() => 
        this.Name;
}
