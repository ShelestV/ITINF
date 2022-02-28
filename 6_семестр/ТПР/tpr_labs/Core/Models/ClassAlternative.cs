namespace Core;

public struct ClassAlternative : IAlternative
{
    public int Index { get; set; }
    public string Name { get; set; }
    public MentionCollection Mentions { get; init; }

    public AlternativeGroup Group { get; set; }
    public double DistanceToGoodCenter { get; set; }
    public double DistanceToBadCenter { get; set; }
    public double ProximityToGoodCenter { get; set; }
    public double ProximityToBadCenter { get; set; }
    public int NumberOfBetter { get; set; }
    public int NumberOfWorse { get; set; }
    public double InformativenessOfGood { get; set; }
    public double InformativenessOfBad { get; set; }

    public double Informativeness => this.InformativenessOfGood + this.InformativenessOfBad;

    public void ChangeGroup(AlternativeGroup group) =>
        this.Group = group;

    public override string ToString() =>
        $"{this.Name}: {this.Group}; " +
        $"d1 = {this.DistanceToGoodCenter}, d2 = {this.DistanceToBadCenter}; " +
        $"p1 = {this.ProximityToGoodCenter}, p2 = {this.ProximityToBadCenter}; " +
        $"g1 = {this.NumberOfBetter}, g2 = {this.NumberOfWorse}; " +
        $"F1 = {this.InformativenessOfGood}, F2 = {this.InformativenessOfBad}, F = {this.Informativeness}";
}
