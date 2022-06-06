namespace Chess.Rules;

public class King : Figure
{
    public Position LeftCastlePosition { get; }
    public Position RightCastlePosition { get; }

    public CastlingAbility Castling { get; private set; } = CastlingAbility.Can;

    public King(Position position, Color color) : base(position, color, Piece.King) => 
        (this.LeftCastlePosition, this.RightCastlePosition) = GetCastlePositionsByColor(color);

    private static (Position, Position) GetCastlePositionsByColor(Color color)
    {
        return color == Color.White ? WhiteCastlePositions :
               color == Color.Black ? BlackCastlePositions :
               throw new ArgumentException(Constants.IncorrectColorMessage);
    }

    private static (Position, Position) WhiteCastlePositions =>
        (Constants.C1.ToPosition(), Constants.G1.ToPosition());

    private static (Position, Position) BlackCastlePositions =>
        (Constants.C8.ToPosition(), Constants.G8.ToPosition());

    public void LockCastling() =>
        this.Castling = CastlingAbility.Cannot;

    public override Figure Clone()
    {
        return new King(this.Position, this.Color) 
        { 
            IsAlive = this.IsAlive, 
            Castling = this.Castling 
        };
    }
}
