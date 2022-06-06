namespace Chess.Rules;

internal class ServiceFactory
{
    public static BaseService GetServiceByFigure(Figure figure)
    {
        return figure.Piece switch
        {
            Piece.King => new KingService((King)figure),
            Piece.Queen => new QueenService((Queen)figure),
            Piece.Rock => new RockService((Rock)figure),
            Piece.Knight => new KnightService((Knight)figure),
            Piece.Bishop => new BishopService((Bishop)figure),
            Piece.Pawn => new PawnService((Pawn)figure),
            _ => throw new ArgumentException(Constants.IncorrectPieceMessage)
        };
    }

    public static BaseService GetServiceByFigure(Figure figure, IGame game)
    {
        return figure.Piece switch
        {
            Piece.King => new KingService((King)figure, game),
            Piece.Queen => new QueenService((Queen)figure, game),
            Piece.Rock => new RockService((Rock)figure, game),
            Piece.Knight => new KnightService((Knight)figure, game),
            Piece.Bishop => new BishopService((Bishop)figure, game),
            Piece.Pawn => new PawnService((Pawn)figure, game),
            _ => throw new ArgumentException(Constants.IncorrectPieceMessage)
        };
    }
}
