namespace Chess.Rules;

internal static class Constants
{
    public const string IncorrectColorMessage = "Incorrect color";
    public const string IncorrectPositionMessage = "Incorrect position";
    public const string IncorrectPieceMessage = "Incorrect piece";
    public const string IncorrectDirectionMessage = "Incorrect direction";

    public static string[] WhiteRockPositions => new string[] { A1, H1 };
    public static string[] WhiteKnightPositions => new string[] { B1, G1 };
    public static string[] WhiteBishopPositions => new string[] { C1, F1 };
    public static string WhiteKingPosition => E1;
    public static string WhiteQueenPosition => D1;
    public static string[] WhitePawnPositions => new string[] { A2, B2, C2, D2, E2, F2, G2, H2 };

    public static string[] BlackRockPositions => new string[] { A8, H8 };
    public static string[] BlackKnightPositions => new string[] { B8, G8 };
    public static string[] BlackBishopPositions => new string[] { C8, F8 };
    public static string BlackKingPosition => E8;
    public static string BlackQueenPosition => D8;
    public static string[] BlackPawnPositions => new string[] { A7, B7, C7, D7, E7, F7, G7, H7 };

    #region Positions
    public const string A1 = "a1";
    public const string A2 = "a2";
    public const string A3 = "a3";
    public const string A4 = "a4";
    public const string A5 = "a5";
    public const string A6 = "a6";
    public const string A7 = "a7";
    public const string A8 = "a8";
    public const string B1 = "b1";
    public const string B2 = "b2";
    public const string B3 = "b3";
    public const string B4 = "b4";
    public const string B5 = "b5";
    public const string B6 = "b6";
    public const string B7 = "b7";
    public const string B8 = "b8";
    public const string C1 = "c1";
    public const string C2 = "c2";
    public const string C3 = "c3";
    public const string C4 = "c4";
    public const string C5 = "c5";
    public const string C6 = "c6";
    public const string C7 = "c7";
    public const string C8 = "c8";
    public const string D1 = "d1";
    public const string D2 = "d2";
    public const string D3 = "d3";
    public const string D4 = "d4";
    public const string D5 = "d5";
    public const string D6 = "d6";
    public const string D7 = "d7";
    public const string D8 = "d8";
    public const string E1 = "e1";
    public const string E2 = "e2";
    public const string E3 = "e3";
    public const string E4 = "e4";
    public const string E5 = "e5";
    public const string E6 = "e6";
    public const string E7 = "e7";
    public const string E8 = "e8";
    public const string F1 = "f1";
    public const string F2 = "f2";
    public const string F3 = "f3";
    public const string F4 = "f4";
    public const string F5 = "f5";
    public const string F6 = "f6";
    public const string F7 = "f7";
    public const string F8 = "f8";
    public const string G1 = "g1";
    public const string G2 = "g2";
    public const string G3 = "g3";
    public const string G4 = "g4";
    public const string G5 = "g5";
    public const string G6 = "g6";
    public const string G7 = "g7";
    public const string G8 = "g8";
    public const string H1 = "h1";
    public const string H2 = "h2";
    public const string H3 = "h3";
    public const string H4 = "h4";
    public const string H5 = "h5";
    public const string H6 = "h6";
    public const string H7 = "h7";
    public const string H8 = "h8";
    #endregion
}
