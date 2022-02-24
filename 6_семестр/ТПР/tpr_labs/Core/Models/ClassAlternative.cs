namespace Core;

internal struct ClassAlternative : IClassAlternative
{
    public int Index { get; set; }
    public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public MentionCollection Mentions { get => throw new System.NotImplementedException(); init => throw new System.NotImplementedException(); }

    public AlternativeGroup Group { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double DistanceToGoodCenter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double DistanceToBadCenter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double ProximityToGoodCenter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double ProximityToBadCenter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double InformativenessOfGood { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public double InformativenessOfBad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public double Informativeness => throw new System.NotImplementedException();
}
