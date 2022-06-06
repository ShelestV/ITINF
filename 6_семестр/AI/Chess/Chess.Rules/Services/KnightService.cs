namespace Chess.Rules;

public class KnightService : BaseService
{
    public KnightService(Knight knight) : base(knight) { }

    public KnightService(Knight knight, IGame game) : base(knight, game) { }

    public override async Task<MoveResult> CanMoveFigure(Position newPosition)
    {
        return this.CheckPositions(newPosition) && await this.IsKingInSafe(newPosition) ?
            this.BeatOrMove(newPosition) : new(MoveResultE.CouldNot);
    }

    public override async Task<bool> CanKillKing(Position kingPosition)
    {
        return await Task.Run(() => this.CheckPositions(kingPosition));
    }

    private bool CheckPositions(Position newPosition)
    {
        if (!this.CheckForCorrectPosition(newPosition))
            return false;

        var positionService = new PositionService(this.Figure.Position);
        var allowedPositions = new List<Position>(GetMoves(positionService));
        var validPositions = allowedPositions
            .Where(x => x.IsValid() && !this.game.IsPositionBusyWithSame(x, this.Figure.Color));

       return validPositions.Contains(newPosition);
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var moves = new List<string>();
        foreach (var position in GetMoves(new PositionService(oldPosition)))
            moves.Add(oldPosition.MoveTo(position).GetString());

        return moves;
    }

    private static IEnumerable<Position> GetMoves(PositionService positionService)
    {
        return new Position[]
        {
            positionService.GetNeighboringCell( 1,  2), 
            positionService.GetNeighboringCell( 2,  1), 
            positionService.GetNeighboringCell(-1,  2), 
            positionService.GetNeighboringCell(-2,  1), 
            positionService.GetNeighboringCell(-1, -2), 
            positionService.GetNeighboringCell(-2, -1), 
            positionService.GetNeighboringCell( 1, -2), 
            positionService.GetNeighboringCell( 2, -1), 
        };
    }
}
