using System;

namespace Core;

internal static class ClassAlternativeExtension
{
    public static double DistanceTo(this ClassAlternative from, ClassAlternative to)
    {
        var result = 0.0;
        for (var mentionIndex = 0; mentionIndex < from.Mentions.Count; mentionIndex++)
        {
            result += Math.Abs(from.Mentions[mentionIndex].Value - to.Mentions[mentionIndex].Value);
        }
        return result;
    }
}
