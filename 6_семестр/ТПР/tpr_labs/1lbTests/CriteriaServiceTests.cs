using Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using System;
using Core.Algos;
using System.IO;

namespace _1lbTests;
public class CriteriaServiceTests
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
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 }),
                    },
                    new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 })
                    }
                }
            },
            new object[]
            {
                new List<Criteria>
                {
                    new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 })
                    },
                    new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 })
                    },
                    new Criteria { Index = 2, Name = "K3", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 })
                    },
                    new Criteria { Index = 3, Name = "K4", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 })
                    },
                    new Criteria { Index = 4, Name = "K5", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 })
                    },
                    new Criteria { Index = 5, Name = "K6", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 })
                    },
                    new Criteria { Index = 6, Name = "K7", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 })
                    },
                    new Criteria { Index = 7, Name = "K8", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    }
                }
            }
        };
    }

    [Fact]
    public async Task GetAllAlternativesAsync_Test()
    {
        var criterias = new List<Criteria>
                {
                    new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 2, Name = "K3", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 3, Name = "K4", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 4, Name = "K5", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 5, Name = "K6", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 6, Name = "K7", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    },
                    new Criteria { Index = 7, Name = "K8", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    }
                };
        var criteriaService = new CriteriaService(criterias);
        var alternatives = await criteriaService.GetAllTheoryAlternativesAsync();
        var alternativesCount = 1;
        foreach (var criteria in criterias)
        {
            alternativesCount *= criteria.Mentions.Count;
        }
        alternatives.Count().Should().Be(alternativesCount);
        var alternativeNames = alternatives.Select(x => x.Name);
        //foreach (var alternativeName in alternativeNames)
        //    await File.WriteAllLinesAsync("WriteLines.txt", alternativeNames);
    }

    [Fact]
    public void GetAllAlternatives_Test()
    {
        var criterias = new List<Criteria>
                {
                    new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 })
                    },
                    new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 })
                    },
                    new Criteria { Index = 2, Name = "K3", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 })
                    },
                    new Criteria { Index = 3, Name = "K4", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 })
                    },
                    new Criteria { Index = 4, Name = "K5", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 })
                    },
                    new Criteria { Index = 5, Name = "K6", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 })
                    },
                    new Criteria { Index = 6, Name = "K7", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 })
                    },
                    new Criteria { Index = 7, Name = "K8", Mentions = new MentionCollection(
                        new Mention { Name = "k1", Value = 0.1 },
                        new Mention { Name = "k2", Value = 0.2 },
                        new Mention { Name = "k3", Value = 0.3 },
                        new Mention { Name = "k4", Value = 0.4 },
                        new Mention { Name = "k5", Value = 0.5 },
                        new Mention { Name = "k6", Value = 0.6 },
                        new Mention { Name = "k7", Value = 0.7 },
                        new Mention { Name = "k8", Value = 0.8 })
                    }
                };
        var criteriaService = new CriteriaService(criterias);
        //var alternatives = criteriaService.GetAllAlternatives();
        var alternativesCount = 1;
        foreach (var criteria in criterias)
        {
            alternativesCount *= criteria.Mentions.Count;
        }
        //lternatives.Count().Should().Be(alternativesCount);
    }

    [Fact]
    public async Task Test()
    {
        var criterias = new List<Criteria>
        {
            new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 0.1 },
                new Mention { Name = "k2", Value = 0.2 },
                new Mention { Name = "k3", Value = 0.3 })
            },
            new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 0.1 },
                new Mention { Name = "k2", Value = 0.2 },
                new Mention { Name = "k3", Value = 0.3 },
                new Mention { Name = "k4", Value = 0.4 })
            },
            new Criteria { Index = 2, Name = "K3", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 0.1 },
                new Mention { Name = "k2", Value = 0.2 })
            }
        };
        var service = new CriteriaService(criterias);
        var alternatives = await service.GetAllTheoryAlternativesAsync();

        #region Expected initialization
        var expected = new Alternative[24];
        expected[0] = new Alternative
        {
            Index = 0,
            Name = "k11,k21,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k21", Value = 0.2 },
                new Mention { Name = "k31", Value = 0.3 })
        };
        expected[1] = new Alternative
        {
            Index = 1,
            Name = "k11,k21,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k21", Value = 0.1 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[2] = new Alternative
        {
            Index = 2,
            Name = "k11,k22,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[3] = new Alternative
        {
            Index = 3,
            Name = "k11,k22,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[4] = new Alternative
        {
            Index = 4,
            Name = "k11,k23,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[5] = new Alternative
        {
            Index = 5,
            Name = "k11,k23,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[6] = new Alternative
        {
            Index = 6,
            Name = "k11,k24,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[7] = new Alternative
        {
            Index = 7,
            Name = "k11,k24,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[8] = new Alternative
        {
            Index = 8,
            Name = "k12,k21,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k21", Value = 0.1 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[9] = new Alternative
        {
            Index = 9,
            Name = "k12,k21,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k21", Value = 0.1 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[10] = new Alternative
        {
            Index = 10,
            Name = "k12,k22,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[11] = new Alternative
        {
            Index = 11,
            Name = "k12,k22,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[12] = new Alternative
        {
            Index = 12,
            Name = "k12,k23,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[13] = new Alternative
        {
            Index = 13,
            Name = "k12,k23,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[14] = new Alternative
        {
            Index = 14,
            Name = "k12,k24,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[15] = new Alternative
        {
            Index = 15,
            Name = "k12,k24,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k12", Value = 0.2 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[16] = new Alternative
        {
            Index = 16,
            Name = "k13,k21,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k21", Value = 0.1 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[17] = new Alternative
        {
            Index = 17,
            Name = "k13,k21,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k11", Value = 0.1 },
                new Mention { Name = "k21", Value = 0.1 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[18] = new Alternative
        {
            Index = 18,
            Name = "k13,k22,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[19] = new Alternative
        {
            Index = 19,
            Name = "k13,k22,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k22", Value = 0.2 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[20] = new Alternative
        {
            Index = 20,
            Name = "k13,k23,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[21] = new Alternative
        {
            Index = 21,
            Name = "k13,k23,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k23", Value = 0.3 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        expected[22] = new Alternative
        {
            Index = 22,
            Name = "k13,k24,k31",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k31", Value = 0.1 })
        };
        expected[23] = new Alternative
        {
            Index = 0,
            Name = "k13,k24,k32",
            Mentions = new MentionCollection(
                new Mention { Name = "k13", Value = 0.3 },
                new Mention { Name = "k24", Value = 0.4 },
                new Mention { Name = "k32", Value = 0.2 })
        };
        #endregion

        for (var i = 0; i < alternatives.Count(); i++)
        {
            alternatives.ElementAt(i).Should().Be(expected[i]);
        }
    }
}