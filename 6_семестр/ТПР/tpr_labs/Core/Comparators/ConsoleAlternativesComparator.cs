using System;
using System.Linq;
using System.Collections.Generic;

namespace Core;

internal sealed class ConsoleAlternativesComparator
{
    private Alternative[] x = Array.Empty<Alternative>();
    private Alternative[] y = Array.Empty<Alternative>();

    private int xIndex = 1;
    private int yIndex = 1;

    private Alternative[] resultAlternatives = Array.Empty<Alternative>();
    private int resAltIndex = 0;

    public IEnumerable<Alternative> Compare(IEnumerable<Alternative> x, IEnumerable<Alternative> y)
    {
        this.SetDefaultValues(x, y);

        do
        {
            this.SetAlternativeToResult(this.ChooseAlternative());
        } while (this.CanChooseAlternatives());

        return this.FormResultAlternatives();
    }

    private void SetDefaultValues(IEnumerable<Alternative> x, IEnumerable<Alternative> y)
    {
        // First alternatives are equaled, (1..1) 
        this.xIndex = 1;
        this.yIndex = 1;

        this.x = x.ToArray();
        this.y = y.ToArray();

        this.resultAlternatives = new Alternative[this.x.Length + this.y.Length - 1]; // -1 is important, because first alternative in both of cases is equaled another
        this.resultAlternatives[0] = this.x[0];
        this.resAltIndex = 1;
    }

    private int ChooseAlternative()
    {
        var choiceAlternatives = new Alternative[] { this.x[this.xIndex], this.y[this.yIndex] };
        var message = $"Choose better alternative: \n{ string.Join("\n", choiceAlternatives.NumerateFrom1()) }\n";
        return ConsoleUIHelper.GetDiapazonNumberFromConsole(message, 1, 2);
    }

    private void SetAlternativeToResult(int choice)
    {
        switch (choice)
        {
            case 1:
                SetAlternativeToResult(this.x, ref this.xIndex, this.resultAlternatives, ref this.resAltIndex);
               break;
            case 2:
                SetAlternativeToResult(this.y, ref this.yIndex, this.resultAlternatives, ref this.resAltIndex);
                break;
        }
    }
    
    private static void SetAlternativeToResult(Alternative[] alternatives, ref int index, Alternative[] resultAlternatives, ref int resultIndex)
    {
        resultAlternatives[resultIndex++] = alternatives[index++];
    }

    private bool CanChooseAlternatives()
    {
        return this.xIndex < this.x.Length && this.yIndex < this.y.Length;
    }

    private IEnumerable<Alternative> FormResultAlternatives()
    {
        var (alternatives, index) = GetRemainedAlternativeEnumerable();
        this.WriteAlternativesFromIndexToResult(alternatives, index);
        return this.resultAlternatives;
    }

    private (IEnumerable<Alternative>, int) GetRemainedAlternativeEnumerable()
    {
        return this.xIndex < this.x.Length ? (this.x, this.xIndex) :
               this.yIndex < this.y.Length ? (this.y, this.yIndex) :
               throw new Exception("Something went wrong");
    }

    private void WriteAlternativesFromIndexToResult(IEnumerable<Alternative> alternatives, int index)
    {
        var alternativesCount = alternatives.Count();
        while (index < alternativesCount)
        {
            this.resultAlternatives[this.resAltIndex++] = alternatives.ElementAt(index++);
        }
    }
}
