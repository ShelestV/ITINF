using System.Collections.Generic;

namespace Core;

public interface IClassAlternativeCollection : IEnumerable<ClassAlternative>
{
    void UpdateAlternativeGroup(int index, AlternativeGroup group);
    void CalculateDistancesToGoodCenter();
    void CalculateDistancesToBadCenter();
    void CalculateProximitiesToGoodCenter();
    void CalculateProximitiesToBadCenter();
    void CalculateNumberOfBetterAlternatives();
    void CalculateNumberOfWorseAlternatives();
    void CalculateGoodInformativenesses();
    void CalculateBadInformativenesses();
}
