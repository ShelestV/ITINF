namespace Core;

public interface IAlternativesClassificable
{
    IClassAlternativeCollection Collection { get; }
    bool CanDoIteration { get; }
    ClassAlternative DoIteration();
    void UpdateAlternativesGroup(ClassAlternative altWithMaxInfo, AlternativeGroup group);
}
