namespace Chess.Rules;

public class QueenService : BaseService
{
    public QueenService(Queen queen) : base(queen) { }

    public QueenService(Queen queen, IGame game) : base(queen, game) { }

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
        if (!IsDirectionCorrect(xDistance, yDistance))
            return false;

        var positionService = new PositionService(this.Figure.Position);
        var positions = positionService.GetCellsByDirectionTo(newPosition, xDistance, yDistance);
        return !this.IsAnyByWay(positions);
    }

    private static bool IsDirectionCorrect(int horizontalDistance, int verticalDistance)
    {
        var absHorizontalDistance = Math.Abs(horizontalDistance);
        var absVerticalDistance = Math.Abs(verticalDistance);

        var isDiagonal = absHorizontalDistance == absVerticalDistance;
        var isStraight = absHorizontalDistance == 0 || absVerticalDistance == 0;

        return isDiagonal || isStraight;
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var positionService = new PositionService(oldPosition);

        var positions = new List<Position>();
        positions.AddRange(positionService.GetCellByDirection(0, -1));
        positions.AddRange(positionService.GetCellByDirection(1, -1));
        positions.AddRange(positionService.GetCellByDirection(1, 0));
        positions.AddRange(positionService.GetCellByDirection(1, 1));
        positions.AddRange(positionService.GetCellByDirection(0, 1));
        positions.AddRange(positionService.GetCellByDirection(-1, 1));
        positions.AddRange(positionService.GetCellByDirection(-1, 0));
        positions.AddRange(positionService.GetCellByDirection(-1, -1));

        var moves = new List<string>();
        foreach (var position in positions)
            moves.Add(oldPosition.MoveTo(position).GetString());

        return moves;
    }
}
