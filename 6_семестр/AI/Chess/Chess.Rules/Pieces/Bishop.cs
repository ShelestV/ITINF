namespace Chess.Rules;

public class Bishop : Figure
{
    public Bishop(Position position, Color color) : base(position, color, Piece.Bishop) { }

    public override Figure Clone()
    {
        return new Bishop(this.Position, this.Color)
        {
            IsAlive = this.IsAlive
        };
    }
}
