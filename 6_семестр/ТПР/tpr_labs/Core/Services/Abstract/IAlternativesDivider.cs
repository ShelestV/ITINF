using System.Collections.Generic;

namespace Core;

public interface IAlternativesDivider
{
    IAlternative GetBest();
    IAlternative GetWorst();
    IEnumerable<IAlternative> GetBetterAlternatives(IAlternative comparableAlternative);
    IEnumerable<IAlternative> GetWorseAlternatives(IAlternative comparableAlternatives);
    IEnumerable<IAlternative> GetIncomparableAlternatives(IAlternative comparableAlternative);
}
