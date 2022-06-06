namespace Chess.Rules;

public static class StringExtensions
{
    public static Position ToPosition(this string position)
    {
        return new(position);
    }

    public static (Position, Position) GetMovePositions(this string move)
    {
        return (new(move.Substring(0, 2)), new(move.Substring(2, 2)));
    }
}
