namespace Chess.Rules;

public class PositionService
{
    public Position Position { get; }

    public PositionService(Position position) =>
        this.Position = position;

    public IEnumerable<Position> GetCellByDirection(int horizontalDirection, int verticalDirection)
    {
        var count = 1;
        var position = this.GetNeighboringCell(horizontalDirection, verticalDirection);
        while (position.IsValid())
        {
            yield return position;
            count++;
            position = this.GetNeighboringCell(horizontalDirection * count, verticalDirection * count);
        }
        
    }

    public IEnumerable<Position> GetCellsByDirectionTo(Position to, int horizontalDeviation, int verticalDeviation)
    {
        // We need only direction: -1, 0 or 1
        var v = verticalDeviation == 0 ? 0 : verticalDeviation / Math.Abs(verticalDeviation);
        var h = horizontalDeviation == 0 ? 0 : horizontalDeviation / Math.Abs(horizontalDeviation);

        var count = horizontalDeviation == 0 ? Math.Abs(verticalDeviation) : Math.Abs(horizontalDeviation);
        for (var i = 1; i < count; i++)
            yield return this.GetNeighboringCell(h * i, v * i);
    }

    public Position GetNeighboringCell(int horizontalDeviation, int verticalDeviation)
    {
        var letterInt = this.Position.Letter + horizontalDeviation;
        var numberInt = this.Position.Number + verticalDeviation;

        return new Position((char)letterInt, (char)numberInt);
    }
}
