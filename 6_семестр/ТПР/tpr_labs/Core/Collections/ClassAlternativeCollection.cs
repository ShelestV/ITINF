using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Threading.Tasks;

namespace Core;

internal class ClassAlternativeCollection : IEnumerable<ClassAlternative>
{
    private IEnumerable<ClassAlternative> alternatives;

    public Alternative GoodCenter { get; private set; }
    public Alternative BadCenter { get; private set; }

    public ClassAlternativeCollection(IEnumerable<Alternative> alternatives)
    {
        this.alternatives = alternatives.ToClassAlternatives();

        this.UpdateAlternativeGroup(0, AlternativeGroup.Good);
        this.UpdateAlternativeGroup(this.alternatives.Count() - 1, AlternativeGroup.Bad);
    }

    private void UpdateAlternativeGroup(int index, AlternativeGroup group)
    {
        var alternative = this.alternatives.ElementAt(index);
        alternative.ChangeGroup(group);
        this.alternatives = this.alternatives.Update(index, alternative);
    }

    public void CalculateCenters()
    {
        this.CalculateGoodCenter();
        this.CalculateBadCenter();
    }

    public void CalculateGoodCenter()
    {
        this.GoodCenter = this.CalculateCenter(AlternativeGroup.Good);
    }

    public void CalculateBadCenter()
    {
        this.BadCenter = this.CalculateCenter(AlternativeGroup.Bad);
    }

    private Alternative CalculateCenter(AlternativeGroup group)
    {
        var alternatives = this.alternatives.Where(x => x.Group == group);
        ValidateAlternatives(alternatives);

        var mentionsCount = alternatives.First().Mentions.Count;
        var centerMentionsValues = new double[mentionsCount];
        centerMentionsValues = centerMentionsValues.Zero().ToArray();

        Parallel.For(0, mentionsCount, (mentionIndex) =>
        {
            foreach (var alternative in alternatives)
            {
                centerMentionsValues[mentionIndex] += alternative.Mentions[mentionIndex].Value;
            }
            centerMentionsValues[mentionIndex] /= (double)alternatives.Count();
        });

        return new Alternative
        {
            Mentions = new MentionCollection(centerMentionsValues.Select(x => new Mention { Value = x }))
        };
    }

    private static void ValidateAlternatives(IEnumerable<ClassAlternative> alternatives)
    {
        var mentionsCount = alternatives.First().Mentions.Count;
        if (!alternatives.All(x => x.Mentions.Count == mentionsCount))
            throw new ArgumentException("Different amounts of mentions");
    }

    public IEnumerator<ClassAlternative> GetEnumerator() =>
        this.alternatives.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}
