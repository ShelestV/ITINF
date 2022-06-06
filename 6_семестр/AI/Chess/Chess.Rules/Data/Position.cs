namespace Chess.Rules;
public struct Position
{
    private const string letters = "abcdefgh";
    private const string numbers = "12345678";

    public char Letter { get; set; }
    public char Number { get; set; }

    public string StringPosition
    {
        get => new(new char[] { this.Letter, this.Number });
        set
        {
            this.Letter = value[0];
            this.Number = value[1];
        }
    }

    public Position(string position)
    {
        this.Letter = position[0];
        this.Number = position[1];
    }

    public Position(char letter, char number)
    {
        this.Letter = letter;
        this.Number = number;
    }

    public bool IsValid() => 
        letters.Contains(this.Letter) &&
        numbers.Contains(this.Number);

    public (int, int) DistanceToInCoord(Position other) =>
        (other.Letter - this.Letter, 
         other.Number - this.Number);

    public override bool Equals(object? obj) => 
        obj is Position position && 
        this.Letter == position.Letter && 
        this.Number == position.Number;

    public override int GetHashCode() => 
        HashCode.Combine(this.Letter, this.Number);

    public static bool operator ==(Position left, Position right) =>
        left.Equals(right);

    public static bool operator !=(Position left, Position right) =>
        !left.Equals(right);

    public override string ToString() =>
        this.StringPosition;
}
