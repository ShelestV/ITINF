namespace Chess.Rules;

public class Knight : Figure
{
    public Knight(Position position, Color color) : base(position, color, Piece.Knight) { }

    public override Figure Clone()
    {
        return new Knight(this.Position, this.Color)
        {
            IsAlive = this.IsAlive
        };
    }
}
