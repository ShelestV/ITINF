using Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace _1lbTests;

public class ClassAlternativeCollectionTests
{
    private readonly TestHelper helper = new();

    [Fact]
    public async Task Constructor_Success_Test()
    {
        var classAlternatives = await this.helper.GetClassAlternativesAsync();

        foreach (var alternative in classAlternatives)
        {
            alternative.Group.Should().Be(
                 alternative.Index == 0 ? AlternativeGroup.Good :
                 alternative.Index == classAlternatives.Count() - 1 ? AlternativeGroup.Bad :
                 AlternativeGroup.Undefined);
        }
    }

    private static void CheckMentionsValues(IEnumerable<Mention> firstMentions, IEnumerable<Mention> secondMentions)
    {
        for (var mentionIndex = 0; mentionIndex < firstMentions.Count(); mentionIndex++)
        {
            firstMentions.ElementAt(mentionIndex).Value.Should().Be(secondMentions.ElementAt(mentionIndex).Value);
        }
    }
}
