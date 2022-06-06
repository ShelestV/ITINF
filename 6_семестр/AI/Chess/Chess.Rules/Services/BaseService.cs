using System.Threading.Tasks;

namespace Chess.Rules;

public abstract class BaseService
{
    protected readonly IGame game;
    public Figure Figure { get; }

    public BaseService(Figure figure)
    {
        this.game = Game.Instanse;
        this.Figure = figure;
    }

    public BaseService(Figure figure, IGame game)
    {
        this.game = game;
        this.Figure = figure;
    }

    protected async Task<bool> IsKingInSafe()
    {
        return !(await game.CanKingBeKilled(this.Figure.Color));
    }

    protected async Task<bool> IsKingInSafe(Position newPosition) 
    {
        var game = this.game.Clone();
        game.Move((this.Figure.Position, newPosition).GetStringMove());
        return !(await game.CanKingBeKilled(this.Figure.Color));
    }

    public abstract Task<MoveResult> CanMoveFigure(Position newPosition);

    public abstract Task<bool> CanKillKing(Position kingPosition);

    protected MoveResult BeatOrMove(Position newPosition)
    {
        return this.game.IsPositionBusyWithOther(newPosition, this.Figure.Color) ?
            new(MoveResultE.Beaten, newPosition) : new(MoveResultE.Moved, null);
    }

    public virtual void MoveFigure(Position newPosition)
    {
        this.Figure.Position = newPosition;
    }

    public abstract IEnumerable<string> GetPossibleMoves();

    protected bool CheckForCorrectPosition(Position position)
    {
        return position.IsValid() && position != this.Figure.Position;
    }

    protected static bool IsDiagonalDirection(int horizontalDistance, int verticalDistance)
    {
        return Math.Abs(horizontalDistance) == Math.Abs(verticalDistance);
    }

    protected static bool IsStraightDirection(int horizontalDistance, int verticalDistance)
    {
        return horizontalDistance == 0 || verticalDistance == 0;
    }

    protected bool IsAnyByWay(IEnumerable<Position> positions)
    {
        return positions.Any(x => this.game.IsPositionBusy(x));
    }
}
