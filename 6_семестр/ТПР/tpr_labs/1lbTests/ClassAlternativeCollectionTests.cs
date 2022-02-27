using Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace _1lbTests;

public class ClassAlternativeCollectionTests
{
    private async Task<IEnumerable<Alternative>> GetAlternativesAsync()
    {
        var criterias = new List<Criteria>
        {
            new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 1 },
                new Mention { Name = "k2", Value = 0.6 },
                new Mention { Name = "k3", Value = 0.3 })
            },
            new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 1 },
                new Mention { Name = "k2", Value = 0.6 },
                new Mention { Name = "k3", Value = 0.3 })
            }
        };
        var service = new CriteriaService(criterias);
        return await service.GetAllTheoryAlternativesAsync();
    }

    private async Task<ClassAlternativeCollection> GetClassAlternativesAsync()
    {
        var alternatives = await this.GetAlternativesAsync();
        return new ClassAlternativeCollection(alternatives);
    }

    [Fact]
    public async Task Constructor_Success_Test()
    {
        var classAlternatives = await this.GetClassAlternativesAsync();

        foreach (var alternative in classAlternatives)
        {
            alternative.Group.Should().Be(
                 alternative.Index == 0 ? AlternativeGroup.Good :
                 alternative.Index == classAlternatives.Count() - 1 ? AlternativeGroup.Bad :
                 AlternativeGroup.Undefined);
        }
    }

    [Fact]
    public async Task CenterCalculating_Success_Test()
    {
        var classAlternatives = await this.GetClassAlternativesAsync();

        classAlternatives.CalculateCenters();
        this.CheckMentionsValues(classAlternatives.GoodCenter.Mentions, classAlternatives.First().Mentions);
        this.CheckMentionsValues(classAlternatives.BadCenter.Mentions, classAlternatives.Last().Mentions);
    }

    private void CheckMentionsValues(IEnumerable<Mention> firstMentions, IEnumerable<Mention> secondMentions)
    {
        for (var mentionIndex = 0; mentionIndex < firstMentions.Count(); mentionIndex++)
        {
            firstMentions.ElementAt(mentionIndex).Value.Should().Be(secondMentions.ElementAt(mentionIndex).Value);
        }
    }
}
