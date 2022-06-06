namespace Chess.Rules;

public class KingService : BaseService
{
    public KingService(King king) : base(king) { }

    public KingService(King king, IGame game) : base(king, game) { }

    public override async Task<MoveResult> CanMoveFigure(Position newPosition)
    {
        return await this.CheckPositions(newPosition) && await this.IsKingInSafe(newPosition) ?
            this.BeatOrMove(newPosition) : new(MoveResultE.CouldNot);
    }

    public override async Task<bool> CanKillKing(Position kingPosition)
    {
        return await Task.Run(() => false);
    }

    private async Task<bool> CheckPositions(Position newPosition)
    {
        if (!this.CheckForCorrectPosition(newPosition))
            return false;

        var positionService = new PositionService(this.Figure.Position);
        var allowedPositions = new List<Position>(await this.GetMoves(positionService));
        var oppositeKing = this.game.GetOppositeKing(this.Figure.Color);
        return allowedPositions.Where(x => this.IsValidPosition(x)).Contains(newPosition);
    }

    private async Task<IEnumerable<Position>> GetMoves(PositionService positionService)
    {
        var positions = GetSimpleMoves(positionService).ToList();

        if (this.CanCastle() && await this.IsKingInSafe())
            positions.AddRange(this.GetCastlingMoves(positionService));
        return positions;
    }

    private bool CanCastle()
    {
        var king = (King)this.Figure;
        return king.Castling == CastlingAbility.Can;
    }

    private IEnumerable<Position> GetCastlingMoves(PositionService positionService)
    {
        var positions = new List<Position>();
        var (leftRock, rightRock) = this.game.GetRocksByColor(this.Figure.Color);

        var (leftXDistance, leftYDistance) = positionService.Position.DistanceToInCoord(leftRock.Position);
        var (rightXDistance, rightYDistance) = positionService.Position.DistanceToInCoord(rightRock.Position);

        var leftPositions = positionService.GetCellsByDirectionTo(leftRock.Position, leftXDistance, leftYDistance);
        var rightPositions = positionService.GetCellsByDirectionTo(rightRock.Position, rightXDistance, rightYDistance);

        if (leftRock.Castling == CastlingAbility.Can && !this.IsAnyByWay(leftPositions))
        {
            positions.Add(positionService.GetNeighboringCell(-2, 0));
        }
        if (rightRock.Castling == CastlingAbility.Can && !this.IsAnyByWay(rightPositions))
        {
            positions.Add(positionService.GetNeighboringCell(2, 0));
        }
        return positions;
    }

    private bool IsValidPosition(Position position)
    {
        var oppositeKing = this.game.GetOppositeKing(this.Figure.Color);
        var positionService = new PositionService(oppositeKing.Position);
        var oppositeKingPosibleMoves = GetSimpleMoves(positionService);
        return position.IsValid() && !this.game.IsPositionBusyWithSame(position, this.Figure.Color) &&
            !oppositeKingPosibleMoves.Contains(position);
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var positionService = new PositionService(oldPosition);

        var positions = new List<Position>();
        positions.AddRange(GetSimpleMoves(positionService));
        positions.AddRange(this.GetCastlingMoves(positionService));

        var moves = new List<string>();
        foreach (var position in positions)
            moves.Add(oldPosition.MoveTo(position).GetString());

        return moves;
    }

    private static IEnumerable<Position> GetSimpleMoves(PositionService positionService)
    {
        return new Position[]
        {
            positionService.GetNeighboringCell( 0, -1), // Up
            positionService.GetNeighboringCell( 1, -1), // UpRight
            positionService.GetNeighboringCell( 1,  0), // Right
            positionService.GetNeighboringCell( 1,  1), // DownRight
            positionService.GetNeighboringCell( 0,  1), // Down
            positionService.GetNeighboringCell(-1,  1), // DownLeft
            positionService.GetNeighboringCell(-1,  0), // Left
            positionService.GetNeighboringCell(-1, -1), // UpLeft
        };
    }

    public override void MoveFigure(Position newPosition)
    {
        var (leftRock, rightRock) = this.game.GetRocksByColor(this.Figure.Color);
        var king = (King)this.Figure;

        if (newPosition == king.LeftCastlePosition)
        {
            Castle(leftRock, rightRock);
        }
        if (newPosition == king.RightCastlePosition)
        {
            Castle(rightRock, leftRock);
        }

        king.LockCastling();
        base.MoveFigure(newPosition);
    }

    private static void Castle(Rock castlingRock, Rock stayingRock)
    {
        var rockService = ServiceFactory.GetServiceByFigure(castlingRock);
        rockService.MoveFigure(castlingRock.CastlePosition);
        stayingRock.LockCastling();
    }
}
