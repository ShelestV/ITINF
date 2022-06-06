namespace Chess.Rules;

public class Queen : Figure
{
    public Queen(Position position, Color color) : base(position, color, Piece.Queen) { }

    public override Figure Clone()
    {
        return new Queen(this.Position, this.Color)
        {
            IsAlive = this.IsAlive
        };
    }
}
