namespace Chess.Rules;

public class BishopService : BaseService
{
    public BishopService(Bishop bishop) : base(bishop) { }

    public BishopService(Bishop bishop, IGame game) : base(bishop, game) { }

    public override async Task<MoveResult> CanMoveFigure(Position newPosition)
    {
        return this.CheckDirection(newPosition) && await this.IsKingInSafe(newPosition) ? 
            this.BeatOrMove(newPosition) : new(MoveResultE.CouldNot);
    }

    public override async Task<bool> CanKillKing(Position kingPosition)
    {
        return await Task.Run(() => this.CheckDirection(kingPosition));
    }

    private bool CheckDirection(Position newPosition)
    {
        if (!this.CheckForCorrectPosition(newPosition))
            return false;

        var (xDistance, yDistance) = this.Figure.Position.DistanceToInCoord(newPosition);
        if (!IsDiagonalDirection(xDistance, yDistance))
            return false;

        var positionService = new PositionService(this.Figure.Position);
        var positions = positionService.GetCellsByDirectionTo(newPosition, xDistance, yDistance);
        return !this.IsAnyByWay(positions);
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var positionService = new PositionService(oldPosition);

        var positions = new List<Position>();
        positions.AddRange(positionService.GetCellByDirection(-1, -1));
        positions.AddRange(positionService.GetCellByDirection(-1, 1));
        positions.AddRange(positionService.GetCellByDirection(1, -1));
        positions.AddRange(positionService.GetCellByDirection(1, 1));

        var moves = new List<string>();
        foreach (var position in positions)
            moves.Add(oldPosition.MoveTo(position).GetString());

        return moves;
    }
}
