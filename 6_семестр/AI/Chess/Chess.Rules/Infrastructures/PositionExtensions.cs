namespace Chess.Rules;

public static class PositionExtensions
{
    public static (Position Old, Position New) MoveTo(this Position oldPosition, Position newPosition)
    {
        return (oldPosition, newPosition);
    }

    public static string GetString(this (Position Old, Position New) move)
    {
        return move.Old.ToString() + move.New.ToString();
    }

    public static string GetStringMove(this (Position Old, Position New) positions) =>
        positions.Old.ToString() + positions.New.ToString();
}
