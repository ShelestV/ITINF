using Core.Algos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core;

public class CriteriaService : IAllTheoryAlternativesGetable
{
    private readonly IEnumerable<Criteria> criterias;

    public CriteriaService(IEnumerable<Criteria> criterias) =>
        this.criterias = criterias;

    public int TheoryAlternativesCount => this.criterias.Select(x => x.Mentions.Count).Multiply();

    public async Task<IEnumerable<Alternative>> GetAllTheoryAlternativesAsync()
    {
        var calculator = new TheoryAlternativesCalculator(this.criterias);
        return await calculator.GetAlternativesAsync();
    }
}
