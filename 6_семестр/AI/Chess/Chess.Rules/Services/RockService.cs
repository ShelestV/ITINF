namespace Chess.Rules;

public class RockService : BaseService
{
    public RockService(Rock rock) : base(rock) { }

    public RockService(Rock rock, IGame game) : base(rock, game) { }

    public override async Task<MoveResult> CanMoveFigure(Position newPosition)
    {
        return this.CheckDirection(newPosition) && await this.IsKingInSafe(newPosition) ? 
            this.BeatOrMove(newPosition) : new(MoveResultE.CouldNot);
    }

    public override async Task<bool> CanKillKing(Position kingPosition) =>
        await Task.Run(() => this.CheckDirection(kingPosition));

    private bool CheckDirection(Position newPosition)
    {
        if (!this.CheckForCorrectPosition(newPosition))
            return false;

        var (xDistance, yDistance) = this.Figure.Position.DistanceToInCoord(newPosition);
        if (!IsStraightDirection(xDistance, yDistance))
            return false;

        var positionService = new PositionService(this.Figure.Position);
        var positions = positionService.GetCellsByDirectionTo(newPosition, xDistance, yDistance);
        return !this.IsAnyByWay(positions);
    }

    public override void MoveFigure(Position newPosition)
    {
        ((Rock)this.Figure).LockCastling();
        base.MoveFigure(newPosition);
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var positionService = new PositionService(oldPosition);

        var positions = new List<Position>();
        positions.AddRange(positionService.GetCellByDirection(0, -1));
        positions.AddRange(positionService.GetCellByDirection(1, 0));
        positions.AddRange(positionService.GetCellByDirection(0, 1));
        positions.AddRange(positionService.GetCellByDirection(-1, 0));

        var moves = new List<string>();
        foreach (var position in positions)
            moves.Add(oldPosition.MoveTo(position).GetString());

        return moves;
    }
}
