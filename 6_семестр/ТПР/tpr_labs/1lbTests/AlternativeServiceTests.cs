using Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace _1lbTests;

public class AlternativeServiceTests
{
    [Fact]
    public async Task GetBetterAlternatives_Test()
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
                new Mention { Name = "k2", Value = 0.75 },
                new Mention { Name = "k3", Value = 0.5  },
                new Mention { Name = "k4", Value = 0.25 })
            }
        };
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

    [Fact]
    public async Task GetWorseAlternatives_Test()
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
                new Mention { Name = "k2", Value = 0.75 },
                new Mention { Name = "k3", Value = 0.5  },
                new Mention { Name = "k4", Value = 0.25 })
            }
        };
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

    [Fact]
    public async Task GetIncomparableAlternatives_Test()
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
                new Mention { Name = "k2", Value = 0.75 },
                new Mention { Name = "k3", Value = 0.5  },
                new Mention { Name = "k4", Value = 0.25 })
            }
        };
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

    [Fact]
    public async Task GetTheBestAlternative_Test()
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
                new Mention { Name = "k2", Value = 0.75 },
                new Mention { Name = "k3", Value = 0.5  },
                new Mention { Name = "k4", Value = 0.25 })
            }
        };
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetBest();
        var expected = alternatives.ElementAt(0);
        actual.Should().Be(expected);
    }

    [Fact]
    public async Task GetWorstAlternative_Test()
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
                new Mention { Name = "k2", Value = 0.75 },
                new Mention { Name = "k3", Value = 0.5  },
                new Mention { Name = "k4", Value = 0.25 })
            }
        };
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        IAlternativesDivider alternativeService = new AlternativeService(alternatives);
        var actual = alternativeService.GetWorst();
        var expected = alternatives.ElementAt(^1);
        actual.Should().Be(expected);
    }
}
