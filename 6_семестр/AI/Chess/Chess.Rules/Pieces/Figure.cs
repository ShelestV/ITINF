namespace Chess.Rules;

public abstract class Figure
{
    public Position Position { get; set; }
    public Color Color { get; }
    public Piece Piece { get; }
    public bool IsAlive { get; protected set; } = true;

    protected Figure(Position position, Color color, Piece piece)
    {
        this.Position = position;
        this.Color = color;
        this.Piece = piece;
    }

    public void Die()
    {
        this.IsAlive = false;
    }

    public abstract Figure Clone();

    public override string ToString() =>
        $"{this.Color} {this.Piece} ({this.Position})";
}