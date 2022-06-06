namespace Chess.Rules;

public class Pawn : Figure
{
    public bool IsMoved { get; private set; } = false;

    public Pawn(Position position, Color color) : base(position, color, Piece.Pawn) { }

    public void MakeMove()
    {
        this.IsMoved = true;
    }

    public override Figure Clone()
    {
        return new Pawn(this.Position, this.Color)
        {
            IsAlive = this.IsAlive,
            IsMoved = this.IsMoved
        };
    }
}
