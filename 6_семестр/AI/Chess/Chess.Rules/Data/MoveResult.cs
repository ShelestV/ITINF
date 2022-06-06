namespace Chess.Rules;

public enum MoveResultE
{
    Moved,
    Beaten,
    CouldNot
}

public class MoveResult
{
    public MoveResultE Result { get; }
    public Position? PositionOfOpposite { get; }

    public MoveResult(MoveResultE result, Position? position = null)
    {
        this.Result = result;
        this.PositionOfOpposite = position;
    }

    public bool IsGood => this.Result is MoveResultE.Moved or MoveResultE.Beaten;
}
