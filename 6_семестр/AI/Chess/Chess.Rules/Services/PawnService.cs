using System.Collections.Generic;
using System.Linq;

namespace Chess.Rules;

public class PawnService : BaseService
{
    public PawnService(Pawn pawn) : base(pawn) { }

    public PawnService(Pawn pawn, IGame game) : base(pawn, game) { }

    public override async Task<MoveResult> CanMoveFigure(Position newPosition)
    {
        var (beatCheck, moveCheck) = this.CheckPositions(newPosition);

        if (!await this.IsKingInSafe(newPosition))
            return new(MoveResultE.CouldNot);

        var isBeat = beatCheck.Result == MoveResultE.Beaten;
        var isMove = moveCheck.Result == MoveResultE.Moved;

        return isBeat ? beatCheck : isMove ? moveCheck : new(MoveResultE.CouldNot);
    }

    public override async Task<bool> CanKillKing(Position kingPosition)
    {
        return await Task.Run(() => this.CheckPositions(kingPosition).Beat.Result == MoveResultE.Beaten);
    }

    private (MoveResult Beat, MoveResult Move) CheckPositions(Position newPosition)
    {
        if (!this.CheckForCorrectPosition(newPosition))
            return (new(MoveResultE.CouldNot), new(MoveResultE.CouldNot));

        var positionService = new PositionService(this.Figure.Position);

        var verticalDirection = this.Figure.Color == Color.White ? 1 : -1;
        var (horizontalDistance, verticalDistance) = this.Figure.Position.DistanceToInCoord(newPosition);

        var beatResult = this.CanBeat(positionService, newPosition, horizontalDistance, verticalDistance);

        var moveResult = this.CanMove(positionService, newPosition, verticalDirection, horizontalDistance, verticalDistance, ((Pawn)this.Figure).IsMoved);

        return (beatResult, moveResult);
    }

    private MoveResult CanBeat(PositionService positionService, Position newPosition, int horizontalDistance, int verticalDistance)
    {
        if (!IsBeat(horizontalDistance, verticalDistance))
            return new(MoveResultE.CouldNot);

        var neighboringCell = positionService.GetNeighboringCell(horizontalDistance, 0);
        var newPositionBusyWithOther = this.game.IsPositionBusyWithOther(newPosition, this.Figure.Color);
        var canTakeOnAisle = this.game.IsPositionFree(newPosition) && this.game.CanPawnTakeOnAisle(neighboringCell, this.Figure.Color);
        return newPositionBusyWithOther ? new(MoveResultE.Beaten, newPosition) :
               canTakeOnAisle ? new(MoveResultE.Beaten, neighboringCell) :
               new(MoveResultE.CouldNot);
    }

    private MoveResult CanMove(PositionService positionService, Position newPosition, int verticalDirection, int horizontalDistance, int verticalDistance, bool isMoved)
    {
        if (!IsMove(horizontalDistance, verticalDistance, isMoved))
            return new(MoveResultE.CouldNot);

        var stepOne = positionService.GetNeighboringCell(0, verticalDirection);
        if (this.game.IsPositionBusy(stepOne))
            return new(MoveResultE.CouldNot);

        if (stepOne == newPosition)
            return new(MoveResultE.Moved);

        var stepTwo = positionService.GetNeighboringCell(0, verticalDirection * 2);
        var canMove = !((Pawn)this.Figure).IsMoved && stepTwo == newPosition && !this.game.IsPositionBusy(newPosition);
        return canMove ? new(MoveResultE.Moved) : new(MoveResultE.CouldNot);
    }

    private static bool IsBeat(int horizontalDistance, int verticalDistance)
    {
        return Math.Abs(horizontalDistance) == 1 && Math.Abs(verticalDistance) == 1;
    }

    private static bool IsMove(int horizontalDistance, int verticalDistance, bool isMoved)
    {
        var absVerticalDistance = Math.Abs(verticalDistance);
        return horizontalDistance == 0 && (absVerticalDistance == 1 || (!isMoved && absVerticalDistance == 2));
    }

    public override void MoveFigure(Position newPosition)
    {
        ((Pawn)this.Figure).MakeMove();
        base.MoveFigure(newPosition);
    }

    public override IEnumerable<string> GetPossibleMoves()
    {
        var oldPosition = this.Figure.Position;
        var verticalDirection = this.Figure.Color == Color.White ? 1 : -1;

        var positionService = new PositionService(oldPosition);
        var moves = new List<string>
        {
            oldPosition.MoveTo(positionService.GetNeighboringCell(0, verticalDirection)).GetString(),
            oldPosition.MoveTo(positionService.GetNeighboringCell(0, verticalDirection * 2)).GetString(),
            oldPosition.MoveTo(positionService.GetNeighboringCell(1, verticalDirection)).GetString(),
            oldPosition.MoveTo(positionService.GetNeighboringCell(-1, verticalDirection)).GetString()
        };

        return moves;
    }
}
