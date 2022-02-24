namespace Core;

internal interface IClassAlternative : IAlternative
{
	public AlternativeGroup Group { get; set; }
	public double DistanceToGoodCenter { get; set; }
	public double DistanceToBadCenter { get; set; }
	public double ProximityToGoodCenter { get; set; }
	public double ProximityToBadCenter { get; set; }
	public double InformativenessOfGood { get; set; }
	public double InformativenessOfBad { get; set; }
	public double Informativeness { get; }
}
