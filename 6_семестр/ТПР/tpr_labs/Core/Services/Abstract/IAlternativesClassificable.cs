namespace Core;

public interface IAlternativesClassificable
{
    IClassAlternativeCollection Collection { get; }
    bool CanDoIteration { get; }
    Task<ClassAlternative> DoIterationAsync();
    Task DoLastIterationAsync();
    void UpdateAlternativesGroup(ClassAlternative altWithMaxInfo, AlternativeGroup group);
}
