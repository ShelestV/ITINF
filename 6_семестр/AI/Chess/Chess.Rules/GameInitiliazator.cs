namespace Chess.Rules;

public static class GameInitiliazator
{
    private static readonly string[] whiteRockPositions = Constants.WhiteRockPositions;
    private static readonly string[] whiteKnightPositions = Constants.WhiteKnightPositions;
    private static readonly string[] whiteBishopPositions = Constants.WhiteBishopPositions;
    private static readonly string whiteKingPosition = Constants.WhiteKingPosition;
    private static readonly string whiteQueenPosition = Constants.WhiteQueenPosition;
    private static readonly string[] whitePawnPositions = Constants.WhitePawnPositions;

    private static readonly string[] blackRockPositions = Constants.BlackRockPositions;
    private static readonly string[] blackKnightPositions = Constants.BlackKnightPositions;
    private static readonly string[] blackBishopPositions = Constants.BlackBishopPositions;
    private static readonly string blackKingPosition = Constants.BlackKingPosition;
    private static readonly string blackQueenPosition = Constants.BlackQueenPosition;
    private static readonly string[] blackPawnPositions = Constants.BlackPawnPositions;

    public static Figure[] GetFigures()
    {
        var white = Color.White;
        var black = Color.Black;

        return new Figure[] 
        {
            new Rock  (GetPosition(Piece.Rock, white, 0), white),
            new Knight(GetPosition(Piece.Knight, white, 0), white),
            new Bishop(GetPosition(Piece.Bishop, white, 0), white),
            new Queen (GetPosition(Piece.Queen, white), white),
            new King  (GetPosition(Piece.King, white), white),
            new Bishop(GetPosition(Piece.Bishop, white, 1), white),
            new Knight(GetPosition(Piece.Knight, white, 1), white),
            new Rock  (GetPosition(Piece.Rock, white, 1), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 0), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 1), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 2), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 3), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 4), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 5), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 6), white),
            new Pawn  (GetPosition(Piece.Pawn, white, 7), white),
            new Rock  (GetPosition(Piece.Rock, black, 0), black),
            new Knight(GetPosition(Piece.Knight, black, 0), black),
            new Bishop(GetPosition(Piece.Bishop, black, 0), black),
            new Queen (GetPosition(Piece.Queen, black), black),
            new King  (GetPosition(Piece.King, black), black),
            new Bishop(GetPosition(Piece.Bishop, black, 1), black),
            new Knight(GetPosition(Piece.Knight, black, 1), black),
            new Rock  (GetPosition(Piece.Rock, black, 1), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 0), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 1), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 2), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 3), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 4), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 5), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 6), black),
            new Pawn  (GetPosition(Piece.Pawn, black, 7), black)
        };
    }

    private static Position GetPosition(Piece piece, Color color, int index = 0)
    {
        return color switch
        {
            Color.White => GetPosition(piece,
                whiteKingPosition,
                whiteQueenPosition,
                whiteRockPositions.ElementAtOrDefault(index),
                whiteKnightPositions.ElementAtOrDefault(index),
                whiteBishopPositions.ElementAtOrDefault(index),
                whitePawnPositions.ElementAtOrDefault(index)),
            Color.Black => GetPosition(piece,
                blackKingPosition,
                blackQueenPosition,
                blackRockPositions.ElementAtOrDefault(index),
                blackKnightPositions.ElementAtOrDefault(index),
                blackBishopPositions.ElementAtOrDefault(index),
                blackPawnPositions.ElementAtOrDefault(index)),
            _ => throw new ArgumentException(Constants.IncorrectColorMessage),
        };
    }

    private static Position GetPosition(Piece piece, string kingPosition, string queenPosition, string? rockPosition, string? knightPosition, string? bishopPosition, string? pawnPosition)
    {
        return piece switch
        {
            Piece.King => new(kingPosition),
            Piece.Queen => new(queenPosition),
            Piece.Rock => new(rockPosition!),
            Piece.Knight => new(knightPosition!),
            Piece.Bishop => new(bishopPosition!),
            Piece.Pawn => new(pawnPosition!),
            _ => throw new ArgumentException(Constants.IncorrectPieceMessage)
        };
    }
}
