using System.Threading.Tasks;
using Xunit;
using Core;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace _1lbTests;
public class _2_lb_Tests
{
    private readonly TestHelper helper = new();

    [Fact]
    public async Task Classificate_Success_Test()
    {
        var alternatives = await helper.GetAlternativesAsync();
        var service = new ClassAlternativeService(alternatives);

        // Assert
        service.CanDoIteration.Should().BeTrue();

        // Act
        // First iteration
        var alternative = service.DoIteration();

        // Assert
        CheckIterationData(service.Collection, new List<object[]>
        {
            new object[] { AlternativeGroup.Good,      0.0, 4.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 1.0, 3.0, 0.75, 0.25, 0, 4, 0.0, 1.0, 1.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 1, 1, 0.5, 0.5, 1.0 },
            new object[] { AlternativeGroup.Undefined, 1.0, 3.0, 0.75, 0.25, 0, 4, 0.0, 1.0, 1.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 2, 2, 1.0, 1.0, 2.0 },
            new object[] { AlternativeGroup.Undefined, 3.0, 1.0, 0.25, 0.75, 4, 0, 1.0, 0.0, 1.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 1, 1, 0.5, 0.5, 1.0 },
            new object[] { AlternativeGroup.Undefined, 3.0, 1.0, 0.25, 0.75, 4, 0, 1.0, 0.0, 1.0 },
            new object[] { AlternativeGroup.Bad,       4.0, 0.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 }
        });
        service.CanDoIteration.Should().BeTrue();

        // Act
        service.UpdateAlternativesGroup(alternative, AlternativeGroup.Good);
        // Second iteration
        alternative = service.DoIteration();

        // Assert
        CheckIterationData(service.Collection, new List<object[]>
        {
            new object[] { AlternativeGroup.Good,      1.0, 4.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 3.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 0, 1, 0.0, 0.5, 0.5 },
            new object[] { AlternativeGroup.Good,      1.0, 3.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 2.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 1.0,  0.4,  0.6, 1, 0, 0.4, 0.0, 0.4 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 0, 1, 0.0, 0.5, 0.5 },
            new object[] { AlternativeGroup.Undefined, 2.0, 1.0,  0.4,  0.6, 1, 0, 0.4, 0.0, 0.4 },
            new object[] { AlternativeGroup.Bad,       3.0, 0.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 }
        });
        service.CanDoIteration.Should().BeTrue();

        // Act
        service.UpdateAlternativesGroup(alternative, AlternativeGroup.Bad);
        // Third iteration
        alternative = service.DoIteration();

        // Assert
        CheckIterationData(service.Collection, new List<object[]>
        {
            new object[] { AlternativeGroup.Good,      1.0, 3.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 2.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       2.0, 1.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 2.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 1.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       2.0, 0.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 3.0,  1.0,  0.0, 0, 1, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.0,  0.5,  0.5, 1, 0, 0.5, 0.0, 0.5 },
            new object[] { AlternativeGroup.Bad,       3.0, 1.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 }
        });
        service.CanDoIteration.Should().BeTrue();

        // Act
        service.UpdateAlternativesGroup(alternative, AlternativeGroup.Bad);
        // Fourth iteration
        alternative = service.DoIteration();

        // Assert
        CheckIterationData(service.Collection, new List<object[]>
        {
            new object[] { AlternativeGroup.Good,      1.0, 3.0,   1.0,   0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 2.0,   1.0,   0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       2.0, 1.5,   0.0,   1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 2.0,   1.0,   0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good,      1.0, 1.0,   1.0,   0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       2.0, 0.5,   0.0,   1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Undefined, 2.0, 2.5, 0.667, 0.333, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       2.0, 1.5,   0.0,   1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,       3.0, 1.0,   0.0,   1.0, 0, 0, 0.0, 0.0, 0.0 }
        });
        service.CanDoIteration.Should().BeTrue();

        // Act
        service.UpdateAlternativesGroup(alternative, AlternativeGroup.Good);
        // Fifth iteration
        service.CanDoIteration.Should().BeFalse();
        service.DoLastIteration();

        // Assert
        CheckIterationData(service.Collection, new List<object[]>
        {
            new object[] { AlternativeGroup.Good, 1.2, 3.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good, 1.4, 2.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,  2.4, 1.5,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good, 0.6, 2.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good, 0.8, 1.0,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,  1.8, 0.5,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Good, 1.6, 2.5,  1.0,  0.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,  1.8, 1.5,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 },
            new object[] { AlternativeGroup.Bad,  2.8, 1.0,  0.0,  1.0, 0, 0, 0.0, 0.0, 0.0 }
        });
    }

    private void CheckIterationData(IClassAlternativeCollection collection, IEnumerable<IEnumerable<object>> expectedData)
    {
        var index = 0;
        foreach (var item in collection)
        {
            var rowData = expectedData.GetRow(index);
            item.Group.Should().Be((AlternativeGroup)rowData.ElementAt(0));
            item.DistanceToGoodCenter.Round(3).Should().Be(((double)rowData.ElementAt(1)).Round(3));
            item.DistanceToBadCenter.Round(3).Should().Be(((double)rowData.ElementAt(2)).Round(3));
            item.ProximityToGoodCenter.Round(3).Should().Be(((double)rowData.ElementAt(3)).Round(3));
            item.ProximityToBadCenter.Round(3).Should().Be(((double)rowData.ElementAt(4)).Round(3));
            item.NumberOfBetter.Should().Be((int)rowData.ElementAt(5));
            item.NumberOfWorse.Should().Be((int)rowData.ElementAt(6));
            item.InformativenessOfGood.Round(3).Should().Be(((double)rowData.ElementAt(7)).Round(3));
            item.InformativenessOfBad.Round(3).Should().Be(((double)rowData.ElementAt(8)).Round(3));
            item.Informativeness.Round(3).Should().Be(((double)rowData.ElementAt(9)).Round(3));
            index++;
        }
    }
}
