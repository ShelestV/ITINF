using Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1lbTests;

internal class TestHelper
{
    public async Task<IEnumerable<Alternative>> GetAlternativesAsync()
    {
        var criterias = new List<Criteria>
        {
            new Criteria { Index = 0, Name = "K1", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 1 },
                new Mention { Name = "k2", Value = 2 },
                new Mention { Name = "k3", Value = 3 })
            },
            new Criteria { Index = 1, Name = "K2", Mentions = new MentionCollection(
                new Mention { Name = "k1", Value = 1 },
                new Mention { Name = "k2", Value = 2 },
                new Mention { Name = "k3", Value = 3 })
            }
        };
        var service = new CriteriaService(criterias);
        return await service.GetAllTheoryAlternativesAsync();
    }

    public async Task<ClassAlternativeCollection> GetClassAlternativesAsync()
    {
        var alternatives = await this.GetAlternativesAsync();
        return new ClassAlternativeCollection(alternatives);
    }
}
