using System.Linq;
using System.Collections.Generic;

namespace Core;

public static class AlternativesExtension
{
    public static IEnumerable<ClassAlternative> ToClassAlternatives(this IEnumerable<Alternative> alternatives)
    {
        var classAlternatives = new ClassAlternative[alternatives.Count()];

        var alternativeIndex = 0;
        foreach (var alternative in alternatives)
        {
            classAlternatives[alternativeIndex] = alternative.ToClassAlternative();
            ++alternativeIndex;
        }

        return classAlternatives;
    }
}
