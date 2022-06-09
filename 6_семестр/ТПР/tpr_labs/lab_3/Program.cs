using Core;

public partial class Program
{
    public static void Main()
    {
        var useUserNames = ConsoleUIHelper.GetBooleanFromConsole("Use default names or you enter them by yourself?");
        Console.Clear();

        // ToDo: Ask for using user or default names
        // ToDo: Uncomment user input
        var criterias = ConsoleUIHelper.GetCriteriasByUserData(useUserNames);
        var alternatives = ConsoleUIHelper.GetAlternativesByUserData(criterias, useUserNames);

        // ToDo: Comment test data
        //var (criterias, alternatives) = GetCriteriasAndAlternatives();

        OutputCriteriaDescriptionOfAlternatives(alternatives, criterias);

        var firstReferenceSituation = criterias.GetFirstReferenceSituation(alternatives.Count, useUserNames);
        firstReferenceSituation.OutputWithHeader(criterias.Select(x => x.Name));

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var alternativesChains = BuildAlternativesChains(criterias, alternatives, firstReferenceSituation);
        OutputAlternativeChains(alternativesChains);

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var treeBuilder = new Core.Algos.AlternativeTreeBuilder(alternativesChains);
        var longestWaySearcher = new Core.Algos.TreeLongestWaySearcher(treeBuilder.Build());
        var longestTreeWay = longestWaySearcher.FindLongestWay();
        var longestWay = longestTreeWay.Select(x => x.Alternative).ToList();

        Console.WriteLine(string.Join(" -> ", longestWay));

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var vectorMarks = GetVectorMarks(alternatives, longestWay);
        OutputVectorMarksToConsole(alternatives, vectorMarks);

        ConsoleUIHelper.ClearConsoleAfterAnyUserAct();

        var bestAlternative = GetBestAlternative(alternatives, vectorMarks);
        Console.WriteLine("The best alternative is " + bestAlternative);
    }

    private static (IList<Criteria> Criterias, IList<Alternative> Alternatives) GetCriteriasAndAlternatives()
    {
        var baseMentionList = new List<Mention>
        {
            new() { Name = "k1", Value = 1 },
            new() { Name = "k2", Value = 2 },
            new() { Name = "k3", Value = 3 },
            //new() { Name = "k4", Value = 4 }
        };

        var criterias = new List<Criteria>
        {
            new() { Index = 0, Name = "K1", Mentions = new(baseMentionList) },
            new() { Index = 1, Name = "K2", Mentions = new(baseMentionList) },
            new() { Index = 2, Name = "K3", Mentions = new(baseMentionList) },
            //new() { Index = 3, Name = "K4", Mentions = new(baseMentionList) }
        };

        var alternatives = new List<Alternative>
        {
            new() 
            {
                Index = 0, 
                Name = "A1", 
                Mentions = new(new Mention[] 
                { 
                    new() { Name = Mention.GetAlternativeName(baseMentionList[0], criterias[0]), Value = baseMentionList[0].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[1], criterias[1]), Value = baseMentionList[1].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[2], criterias[2]), Value = baseMentionList[2].Value }
                })
            },
            new() 
            { 
                Index = 1, 
                Name = "A2", 
                Mentions = new(new Mention[] 
                {
                    new() { Name = Mention.GetAlternativeName(baseMentionList[1], criterias[0]), Value = baseMentionList[1].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[2], criterias[1]), Value = baseMentionList[2].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[0], criterias[2]), Value = baseMentionList[0].Value }
                })
            },
            new() 
            { 
                Index = 2, 
                Name = "A3", 
                Mentions = new(new Mention[] 
                {
                    new() { Name = Mention.GetAlternativeName(baseMentionList[2], criterias[0]), Value = baseMentionList[2].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[0], criterias[1]), Value = baseMentionList[0].Value },
                    new() { Name = Mention.GetAlternativeName(baseMentionList[1], criterias[2]), Value = baseMentionList[1].Value }
                })
            },

            //new() { Index = 0, Name = "A1", Mentions = new(new Mention[] { baseMentionList[0], baseMentionList[1], baseMentionList[2], baseMentionList[3] })},
            //new() { Index = 1, Name = "A2", Mentions = new(new Mention[] { baseMentionList[1], baseMentionList[2], baseMentionList[3], baseMentionList[0] })},
            //new() { Index = 2, Name = "A3", Mentions = new(new Mention[] { baseMentionList[2], baseMentionList[3], baseMentionList[0], baseMentionList[1] })},
            //new() { Index = 3, Name = "A4", Mentions = new(new Mention[] { baseMentionList[3], baseMentionList[0], baseMentionList[1], baseMentionList[2] })}
        };

        return (criterias, alternatives);
    }

    private static void OutputCriteriaDescriptionOfAlternatives(IEnumerable<Alternative> alternatives, IEnumerable<Criteria> criterias)
    {
        Console.Write("Alter");
        foreach (var criteria in criterias)
            Console.Write($"\t{ criteria.Name }");
        Console.WriteLine();

        foreach (var alternative in alternatives)
            Console.WriteLine($"{ alternative.Name }\t{ string.Join('\t', alternative.Mentions.ToValues()) }");
        Console.WriteLine();
    }

    private static Alternative[][] BuildAlternativesChains(IList<Criteria> criterias, IList<Alternative> alternatives, Alternative[,] firstReferenceSituation)
    {
        var comparator = new ConsoleAlternativesComparator();
        var chainsCount = criterias.Count * (criterias.Count - 1) / 2;
        var alternativesChains = new Alternative[chainsCount][];
        var chainIndex = 0;
        for (var leftIndex = 0; leftIndex < alternatives.Count - 1; leftIndex++)
        {
            for (var rightIndex = leftIndex + 1; rightIndex < alternatives.Count; ++rightIndex)
            {
                Console.WriteLine($"Compare criterias: \n{criterias[leftIndex]}\n{criterias[rightIndex]}\n");

                var leftAlternatives = firstReferenceSituation.GetColumn(leftIndex);
                var rightAlternatives = firstReferenceSituation.GetColumn(rightIndex);
                alternativesChains[chainIndex++] = comparator.Compare(leftAlternatives, rightAlternatives).ToArray();

                Console.Clear();
            }
        }

        return alternativesChains;
    }

    private static void OutputAlternativeChains(Alternative[][] alternativesChains)
    {
        foreach (var chain in alternativesChains)
            Console.WriteLine(string.Join(" -> ", chain));
    }

    private static int[][] GetVectorMarks(IList<Alternative> alternatives, IList<Alternative> longestWay)
    {
        var vectorMarks = new int[alternatives.Count][];
        var vectorMarksIndex = 0;
        foreach (var alternative in alternatives)
        {
            vectorMarks[vectorMarksIndex] = new int[alternative.Mentions.Count];
            for (var mentionIndex = 0; mentionIndex < alternative.Mentions.Count; mentionIndex++)
            {
                var alternativeFromLongestWay = longestWay.First(x => x.Mentions[mentionIndex].Equals(alternative.Mentions[mentionIndex]));
                var alternativeFromLongestWayIndex = longestWay.IndexOf(alternativeFromLongestWay);
                vectorMarks[vectorMarksIndex][mentionIndex] = alternativeFromLongestWayIndex + 1;
            }
            vectorMarksIndex++;
        }
        return vectorMarks;
    }

    private static void OutputVectorMarksToConsole(IList<Alternative> alternatives, int[][] vectorMarks)
    {
        for (var index = 0; index < alternatives.Count; index++)
        {
            Console.Write($"{alternatives[index].Name}\t");
            Console.Write($"{string.Join("", alternatives[index].Mentions.ToValues())}\t");
            Console.Write($"{string.Join("", vectorMarks[index])}\t");
            Array.Sort(vectorMarks[index]);
            Console.Write($"{string.Join("", vectorMarks[index])}\t");
            Console.WriteLine();
        }
    }

    private static Alternative GetBestAlternative(IList<Alternative> alternatives, int[][] vectorMarks)
    {
        var intVectorMarks = vectorMarks.Select(x => int.Parse(string.Join("", x)));
        var minVectorMark = intVectorMarks.Min();
        var indexOfBestAlternative = intVectorMarks.ToList().IndexOf(minVectorMark);
        return alternatives[indexOfBestAlternative];
    }
}