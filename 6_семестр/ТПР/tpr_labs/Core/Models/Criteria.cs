using System;
using System.Collections.Generic;

namespace Core;

public struct Criteria : IModel
{
    public int Index { get; set; }
    public string Name { get; set; }
    public MentionCollection Mentions { get; set; }

    public override bool Equals(object? obj) => 
        obj is Criteria criteria && 
        this.Name == criteria.Name;

    public override int GetHashCode() => 
        HashCode.Combine(this.Name);

    public static bool operator ==(Criteria left, Criteria right) => 
        left.Equals(right);

    public static bool operator !=(Criteria left, Criteria right) => 
        !left.Equals(right);

    public override string ToString() =>
        this.Name + $" {{ { string.Join(", ", this.Mentions) } }}";
}
