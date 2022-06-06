using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Chess.Rules.Tests.Services;

public class PositionServiceTests
{
    private static IEnumerable<IEnumerable<object>> GetNeighboringCells()
    {
        return new List<object[]>
        {
            new object[] { new Position("d4"), -1,  1, new Position("c5") },
            new object[] { new Position("d4"),  2,  2, new Position("f6") },
            new object[] { new Position("d4"),  5,  5, new Position("i9") },
            new object[] { new Position("d4"), -3,  5, new Position("a9") },
            new object[] { new Position("d4"),  2, -3, new Position("f1") },
            new object[] { new Position("d4"),  4,  4, new Position("h8") },
        };
    }

    [Theory]
    [MemberData(nameof(GetNeighboringCells))]
    public void GetNeighboringCells_Success_Test(Position initialPosition, int horizontalDeviation, int verticalDeviation, Position neighboringPosition)
    {
        var positionService = new PositionService(initialPosition);
        var actualPosition = positionService.GetNeighboringCell(horizontalDeviation, verticalDeviation);
        actualPosition.Should().Be(neighboringPosition);
    }
}
