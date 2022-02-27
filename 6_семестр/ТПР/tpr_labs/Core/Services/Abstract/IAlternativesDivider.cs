using System.Collections.Generic;

namespace Core;

public interface IAlternativesDivider
{
    Alternative GetBest();
    Alternative GetWorst();
    IEnumerable<Alternative> GetBetterAlternatives(Alternative comparableAlternative);
    IEnumerable<Alternative> GetWorseAlternatives(Alternative comparableAlternatives);
    IEnumerable<Alternative> GetIncomparableAlternatives(Alternative comparableAlternative);
}
