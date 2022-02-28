using Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace _1lbTests;

public class AlternativeServiceTests
{
    private static IEnumerable<IEnumerable<object>> GetData()
    {
        return new List<object[]>
        {
            new object[]
            {
                new List<Criteria>
                {
                    new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 1 },
                        new Mention { Name = "k2", Value = 2 },
                        new Mention { Name = "k3", Value = 3 })
                    },
                    new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 1 },
                        new Mention { Name = "k2", Value = 2 },
                        new Mention { Name = "k3", Value = 3 },
                        new Mention { Name = "k4", Value = 4 })
                    }
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task GetBetterAlternatives_Test(IEnumerable<Criteria> criterias)
    {
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetBetterAlternatives(alternatives.ElementAt(6));
        var expected = new List<Alternative> 
        { 
            alternatives.ElementAt(0), 
            alternatives.ElementAt(1), 
            alternatives.ElementAt(2),
            alternatives.ElementAt(4),
            alternatives.ElementAt(5),
        };
        actual.Count().Should().Be(expected.Count);
        foreach (var alternative in expected)
        {
            actual.Any(x => x.Equals(alternative)).Should().BeTrue();
        }
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task GetWorseAlternatives_Test(IEnumerable<Criteria> criterias)
    {
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetWorseAlternatives(alternatives.ElementAt(6));
        var expected = new List<Alternative>
        {
            alternatives.ElementAt(7),
            alternatives.ElementAt(10),
            alternatives.ElementAt(11),
        };
        actual.Count().Should().Be(expected.Count);
        foreach (var alternative in expected)
        {
            actual.Any(x => x.Equals(alternative)).Should().BeTrue();
        }
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task GetIncomparableAlternatives_Test(IEnumerable<Criteria> criterias)
    {
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetIncomparableAlternatives(alternatives.ElementAt(6));
        var expected = new List<Alternative>
        {
            alternatives.ElementAt(3),
            alternatives.ElementAt(8),
            alternatives.ElementAt(9),
        };
        actual.Count().Should().Be(expected.Count);
        foreach (var alternative in expected)
        {
            actual.Any(x => x.Equals(alternative)).Should().BeTrue();
        }
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task GetTheBestAlternative_Test(IEnumerable<Criteria> criterias)
    {
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetBest();
        var expected = alternatives.ElementAt(0);
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task GetWorstAlternative_Test(IEnumerable<Criteria> criterias)
    {
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetWorst();
        var expected = alternatives.ElementAt(^1);
        actual.Should().Be(expected);
    }
}
