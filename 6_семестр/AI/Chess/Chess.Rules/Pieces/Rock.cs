namespace Chess.Rules;

public class Rock : Figure
{
    public Position CastlePosition { get; }

    public RockDirection Direction { get; init; }
    public CastlingAbility Castling { get; private set; } = CastlingAbility.Can;

    public Rock(Position position, Color color) : base(position, color, Piece.Rock)
    {
        this.Direction = position.Letter <= 'd' ? RockDirection.Left : RockDirection.Right;
        this.CastlePosition = GetCastlePositionByColor(color, this.Direction);
    }

    private static Position GetCastlePositionByColor(Color color, RockDirection direction)
    {
        return color == Color.White ? GetWhiteCastlePositionByDirection(direction) :
               color == Color.Black ? GetBlackCastlePositionByDirection(direction) :
               throw new ArgumentException(Constants.IncorrectColorMessage);
    }

    private static Position GetWhiteCastlePositionByDirection(RockDirection direction)
    {
        return direction == RockDirection.Left  ? WhiteLeftCastlePosition  :
               direction == RockDirection.Right ? WhiteRightCastlePosition :
               throw new ArgumentException(Constants.IncorrectDirectionMessage);
    }
        

    private static Position GetBlackCastlePositionByDirection(RockDirection direction)
    {
        return direction == RockDirection.Left  ? BlackLeftCastlePosition  :
               direction == RockDirection.Right ? BlackRightCastlePosition :
               throw new ArgumentException(Constants.IncorrectDirectionMessage);
    }

    private static Position WhiteLeftCastlePosition  => Constants.D1.ToPosition();
    private static Position WhiteRightCastlePosition => Constants.F1.ToPosition();
    private static Position BlackLeftCastlePosition  => Constants.D8.ToPosition();
    private static Position BlackRightCastlePosition => Constants.F8.ToPosition();

    public void LockCastling() =>
        this.Castling = CastlingAbility.Cannot;

    public override Figure Clone()
    {
        return new Rock(this.Position, this.Color)
        {
            IsAlive = this.IsAlive,
            Castling = this.Castling,
            Direction = this.Direction
        };
    }
}
