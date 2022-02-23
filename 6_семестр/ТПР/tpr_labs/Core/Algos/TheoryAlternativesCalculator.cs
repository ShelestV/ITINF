using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Algos;

internal class TheoryAlternativesCalculator
{
    private readonly IEnumerable<Criteria> criterias;

    private readonly Alternative[] alternatives;
    private readonly Mention[,] mentions;

    private readonly int alternativesCount;

    public TheoryAlternativesCalculator(IEnumerable<Criteria> criterias)
    {
        this.criterias = criterias;
        this.alternativesCount = this.criterias.Select(x => x.Mentions.Count).Multiply();
        this.mentions = new Mention[this.alternativesCount, this.criterias.Count()];
        this.alternatives = new Alternative[this.alternativesCount];
    }

    public async Task<IEnumerable<Alternative>> GetAlternativesAsync()
    {
        var firstCriteria = this.criterias.First();
        await this.FillMentionsOrCreateAlternative(firstCriteria);
        return this.alternatives;
    }

    private async Task FillMentionsOrCreateAlternative(Criteria criteria)
    {
        var isNextCriteriaExisting = this.criterias.Any(x => x.Index == criteria.Index + 1);
        if (isNextCriteriaExisting)
        {
            await this.SetCriteriaMentions(criteria);
            var nextCriteria = this.criterias.ElementAt(criteria.Index + 1);
            await this.FillMentionsOrCreateAlternative(nextCriteria);
        }
        else
        {
            await this.SetCriteriaMentions(criteria);
            this.SetAlternatives();
        }
    }

    private async Task SetCriteriaMentions(Criteria criteria)
    {
        var tasks = new Task[criteria.Mentions.Count];
        for (var i = 0; i < criteria.Mentions.Count; i++)
        {
            var index = i;
            tasks[index] = Task.Run(() => this.SetMentions(criteria, index));
        }
        await Task.WhenAll(tasks);
    }

    private void SetAlternatives()
    {
        Parallel.For(0, alternativesCount, i =>
        {
            var index = i;
            this.SetAlternative(index);
        });
    }

    private void SetAlternative(int index)
    {
        var mentions = this.mentions.GetRow(index);
        this.alternatives[index] = new()
        {
            Index = index,
            Name = string.Join(',', mentions.Select(x => x.Name)),
            Mentions = new MentionCollection(mentions)
        };
    }

    private void SetMentions(Criteria criteria, int mentionIndex)
    {
        var mentionDuplicatesCount = this.alternativesCount;
        for (var i = 0; i <= criteria.Index; i++)
            mentionDuplicatesCount /= criterias.ElementAt(i).Mentions.Count;

        Parallel.For(0, this.mentions.GetRowsCount(), i => 
        {
            var index = i;
            this.InitMentions(criteria, mentionDuplicatesCount, mentionIndex, index);
        });
    }

    private void InitMentions(Criteria criteria, int mentionDuplicatesCount, int mentionIndex, int index)
    {
        if (ShouldInitialize(criteria, mentionDuplicatesCount, mentionIndex, index))
        {
            this.mentions[index, criteria.Index] = new()
            {
                Name = Mention.GetAlternativeName(criteria.Mentions[mentionIndex], criteria),
                Value = criteria.Mentions[mentionIndex].Value,
            };
        }
    }

    private static bool ShouldInitialize(Criteria criteria, int duplicatesCount, int mentionIndex, int alternativeIndex)
    {
        var altIndexWithoutDup = (int)Math.Ceiling((double)(alternativeIndex / duplicatesCount));
        return altIndexWithoutDup % criteria.Mentions.Count == mentionIndex;
    }
}
