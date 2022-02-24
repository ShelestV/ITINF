using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core;

public interface IAllTheoryAlternativesGetable
{
    int TheoryAlternativesCount { get; }
    Task<IEnumerable<IAlternative>> GetAllTheoryAlternativesAsync();
}
