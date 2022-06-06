using System.Threading.Tasks;

namespace Chess.Rules;

internal interface IGameLogic
{
    (Rock, Rock) GetRocksByColor(Color color);
    Figure? GetFigureByPosition(Position position);
    King GetOppositeKing(Color color);

    bool IsPositionFree(Position position);
    bool IsPositionBusy(Position position);
    bool IsPositionBusyWithOther(Position position, Color color);
    bool IsPositionBusyWithSame(Position position, Color color);

    bool CanPawnTakeOnAisle(Position position, Color color);
    Task<bool> CanKingBeKilled(Color currentColor);

    IGameLogic Clone();
}
