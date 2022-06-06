using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Chess.Rules.Tests;

public class GameTests
{
    private IGame game = Game.StartNewGame();

    public record Move(string Action, bool MoveResult);

    private static IEnumerable<IEnumerable<object>> GetMoves()
    {
        return new List<object[]>
        {
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("d7d5", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d7d5", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("c7c5", true),
                    new("d4c5", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1e3", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1d2", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1d2", true),
                    new("b4d2", true),
                    new("e1d2", true),
                    new("d8g5", true),
                    new("d4d5", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1d2", true),
                    new("b4d2", true),
                    new("e1d2", true),
                    new("d8g5", true),
                    new("d2e1", true)
                }
            }, 
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1d2", true),
                    new("b4d2", true),
                    new("e1d2", true),
                    new("d8g5", true),
                    new("d2e1", true),
                    new("g5e3", true),
                    new("f2e3", true)
                }
            }, 
            new object[]
            {
                new Move[]
                {
                    new("d2d4", true),
                    new("e7e5", true),
                    new("e2e4", true),
                    new("f8b4", true),
                    new("c1d2", true),
                    new("b4d2", true),
                    new("e1d2", true),
                    new("d8g5", true),
                    new("d2e1", true),
                    new("g5e3", true),
                    new("f2e3", true),
                    new("e5d4", true),
                    new("e3d4", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("e1g1", false)
                }
            }, 
            new object[]
            {
                new Move[]
                {
                    new("e2e4", true),
                    new("e7e5", true),
                    new("f1c4", true),
                    new("d7d6", true),
                    new("e1g1", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("e2e4", true),
                    new("e7e5", true),
                    new("g1f3", true),
                    new("d7d6", true),
                    new("e1g1", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("e2e4", true),
                    new("e7e5", true),
                    new("g1f3", true),
                    new("d7d6", true),
                    new("f1c4", true),
                    new("c8d7", true),
                    new("e1g1", true)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("h2h4", true),
                    new("e7e5", true),
                    new("g2g3", true),
                    new("d7d5", true),
                    new("g1f3", true),
                    new("c7c5", true),
                    new("f1g2", true),
                    new("c8f5", true),
                    new("h1h2", true),
                    new("b8c6", true),
                    new("h2h1", true),
                    new("g8f6", true),
                    new("e1g1", false)
                }
            },
            new object[]
            {
                new Move[]
                {
                    new("e2e4", true),
                    new("e7e5", true),
                    new("e1e2", true),
                    new("e8e7", true),
                    new("e2e3", true),
                    new("e7e6", true),
                    new("d2d3", true),
                    new("f7f5", true),
                    new("e4f5", true),
                    new("e6f5", true),
                    new("e3e4", false)
                }
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetMoves))]
    public async Task MoveWithoutCheck_Test(IEnumerable<Move> moves)
    {
        this.StartNewGame();
        foreach (var move in moves)
            await this.MakeMove(move);
    }

    [Fact]
    public async Task Castling_CheckRockPosition_E2E4_E7E5_G1F3_D7D6_F1C4_C8D7_E1G1_Success_Test()
    {
        this.StartNewGame();
        await this.MakeMove("e2e4", true);
        await this.MakeMove("e7e5", true);
        await this.MakeMove("g1f3", true);
        await this.MakeMove("d7d6", true);
        await this.MakeMove("f1c4", true);
        await this.MakeMove("c8d7", true);
        await this.MakeMove("e1g1", true);

        var f = this.game.GetFigureByPosition("g1".ToPosition());
        f.Should().NotBeNull("Get figure by position g1 is not null");
        f.Should().BeOfType<King>("Figure has type of king");
        var king = (f! as King)!;
        king.Color.Should().Be(Color.White, "Color of king is white");
        king.Castling.Should().Be(CastlingAbility.Cannot, "King can't castle twice");
        king.IsAlive.Should().BeTrue("King is alive");

        var figure = this.game.GetFigureByPosition("f1".ToPosition());
        figure.Should().NotBeNull("Get figure by position f1 is not null");
        figure.Should().BeOfType<Rock>("Figure has type of rock");
        var rock = (figure! as Rock)!;
        rock.Color.Should().Be(Color.White, "Color of rock is white");
        rock.Direction.Should().Be(RockDirection.Right, "It's right rock");
        rock.Castling.Should().Be(CastlingAbility.Cannot, "Rock can't castle twice");
        rock.IsAlive.Should().BeTrue("Rock is alive");

        var figure1 = this.game.GetFigureByPosition("a1".ToPosition());
        figure1.Should().NotBeNull("Get figure by position f1 is not null");
        figure1.Should().BeOfType<Rock>("Figure has type of rock");
        var rock1 = (figure1! as Rock)!;
        rock1.Color.Should().Be(Color.White, "Color of rock is white");
        rock1.Direction.Should().Be(RockDirection.Left, "It's left rock");
        rock1.Castling.Should().Be(CastlingAbility.Cannot, "Rock can't castle twice");
        rock1.IsAlive.Should().BeTrue("Rock is alive");
    }

    [Fact]
    public async Task Pawn_KillOnAisle_E2E4_D7D5_E4E5_D5E4_Success_Test()
    {
        this.StartNewGame();
        await this.MakeMove("e2e4", true);
        await this.MakeMove("d7d5", true);
        await this.MakeMove("e4e5", true);
        await this.MakeMove("d5e4", true);

        var figure = this.game.GetFigureByPosition("e5".ToPosition());
        figure.Should().BeNull();
    }

    [Fact]
    public async Task GameOver_Test()
    {
        this.StartNewGame();
        await this.MakeMove("e2e4", true, false);
        await this.MakeMove("e7e5", true, false);
        await this.MakeMove("f1c4", true, false);
        await this.MakeMove("f8e7", true, false);
        await this.MakeMove("d1f3", true, false);
        await this.MakeMove("e7d6", true, false);
        await this.MakeMove("f3f7", true, true);
    }

    private void StartNewGame()
    {
        this.game = Game.StartNewGame();
    }

    private async Task MakeMove(Move move)
    {
        await this.MakeMove(move.Action, move.MoveResult);
    }

    private async Task MakeMove(string move, bool canMove, bool isGameOver)
    {
        await this.MakeMove(move, canMove);
        var gameOverResult = await this.game.IsGameOver();
        var shouldBeOver = isGameOver ? "" : "not ";
        gameOverResult.Should().Be(isGameOver, $"{ move }. Game should be { shouldBeOver } over");
    }

    private async Task MakeMove(string move, bool canMove)
    {
        var moveResult = await this.game.TryMove(move);
        moveResult.Should().Be(canMove, $"Move { move }, result = { canMove }");
    }
}
