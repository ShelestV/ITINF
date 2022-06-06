namespace Chess.Rules;

/// <summary>
/// It implements Singleton pattern
/// </summary>
public class Game : IGame, IGameLogic
{
    private Figure[] figures;

    private Figure? lastMover;

    private static IGame? instanse;
    /// <summary>
    /// Implementation of Singleton pattern
    /// </summary>
    public static IGame Instanse => instanse ??= StartNewGame();

    private Game()
    {
        this.figures = GameInitiliazator.GetFigures();
    }

    /// <summary>
    /// Create new Game and update instanse
    /// </summary>
    /// <returns></returns>
    public static IGame StartNewGame()
    {
        instanse = new Game();
        return instanse;
    }

    public async Task<bool> IsGameOver()
    {
        var currentColor = this.lastMover!.Color == Color.White ? Color.Black :
                           this.lastMover!.Color == Color.Black ? Color.White :
                           throw new ArgumentException(Constants.IncorrectColorMessage);

        if (!await this.CanKingBeKilled(currentColor))
            return false;

        var possibleMoves = this.figures
            .Where(x => x.IsAlive && IsOpposite(x.Color, this.lastMover!.Color))
            .Select(x => ServiceFactory.GetServiceByFigure(x))
            .SelectMany(x => x.GetPossibleMoves());

        var movesCount = possibleMoves.Count();
        var gameTasks = new Task<bool>[movesCount];
        for (var i = 0; i < movesCount; i++)
        {
            var move = possibleMoves.ElementAt(i);
            gameTasks[i] = Task.Run(() => this.Clone().TryMove(move));
        }
        await Task.WhenAll(gameTasks);

        return gameTasks.All(x => !x.Result);
    }

    public async Task<bool> TryMove(string move)
    {
        move = move.ToLower();
        var (oldPosition, newPosition) = move.GetMovePositions();
        var figure = this.GetFigureByPosition(oldPosition)!;
        return this.CheckForCorrectMoveSequence(figure) && 
            await this.CanBeatOrMove(figure, newPosition);
    }

    public void Move(string move)
    {
        move = move.ToLower();
        var (oldPosition, newPosition) = move.GetMovePositions();
        var figure = this.GetFigureByPosition(oldPosition)!;
        var figureService = ServiceFactory.GetServiceByFigure(figure, this);
        MoveResult moveResult = this.IsPositionBusyWithOther(newPosition, figure.Color) ? 
            new(MoveResultE.Beaten, newPosition) : new(MoveResultE.Moved);
        this.BeatOrMove(figureService, newPosition, moveResult);
    }

    private async Task<bool> CanBeatOrMove(Figure figure, Position newPosition)
    {
        var figureService = ServiceFactory.GetServiceByFigure(figure);
        var moveResult = await figureService.CanMoveFigure(newPosition);
        if (moveResult.IsGood)
        {
            this.BeatOrMove(figureService, newPosition, moveResult);
            return true;
        }

        return false;
    }

    private void BeatOrMove(BaseService figureService, Position newPosition, MoveResult result)
    {
        if (result.Result == MoveResultE.Beaten && result.PositionOfOpposite.HasValue)
            this.KillFigureByPosition(result.PositionOfOpposite.Value);

        figureService.MoveFigure(newPosition);
        this.lastMover = figureService.Figure;
    }

    private void KillFigureByPosition(Position position)
    {
        var figure = this.GetFigureByPosition(position);
        figure!.Die();
    }

    private bool CheckForCorrectMoveSequence(Figure figure)
    {
        return (this.lastMover is null && figure.Color == Color.White) ||
            (this.lastMover is not null && this.lastMover.Color != figure.Color);
    }

    public (Rock, Rock) GetRocksByColor(Color color)
    {
        var rocks = figures.Where(x => x.Color == color && x.Piece == Piece.Rock).Select(x => (Rock)x);
        return (rocks.First(x => x.Direction == RockDirection.Left), rocks.First(x => x.Direction == RockDirection.Right));
    }

    public Figure? GetFigureByPosition(Position position)
    {
        return figures.FirstOrDefault(x => CheckPosition(x, position));
    }

    public King GetOppositeKing(Color color)
    {
        return (King)this.figures.First(x => x.Piece == Piece.King && IsOpposite(x.Color, color));
    }

    public bool CanPawnTakeOnAisle(Position position, Color color)
    {
        return this.lastMover is not null && 
            this.lastMover.Piece == Piece.Pawn &&
            this.lastMover.Position == position && 
            this.lastMover.Color != color;
    }

    public async Task<bool> CanKingBeKilled(Color currentColor)
    {
        var king = this.GetKingByColor(currentColor);

        var figures = this.figures.Where(x => IsOpposite(x.Color, currentColor));

        var figuresCount = figures.Count();
        var tasks = new Task<bool>[figuresCount];
        for (var i = 0; i < figuresCount; i++)
            tasks[i] = this.CanKillKing(figures.ElementAt(i), king.Position);

        await Task.WhenAll(tasks);
        return tasks.Any(x => x.Result);
    }

    private async Task<bool> CanKillKing(Figure figure, Position kingPosition)
    {
        if (!figure.IsAlive)
            return false;

        var service = ServiceFactory.GetServiceByFigure(figure, this);
        return await service.CanKillKing(kingPosition);
    }

    private Figure GetKingByColor(Color color)
    {
        return this.figures.First(x => x.Color == color && x.Piece == Piece.King);
    }

    public bool IsPositionFree(Position position)
    {
        return !this.IsPositionBusy(position);
    }

    public bool IsPositionBusy(Position position)
    {
        return this.figures.Any(x => x.Position == position);
    }

    public bool IsPositionBusyWithOther(Position position, Color color)
    {
        return this.figures.Any(x => IsOpposite(x.Color, color) && CheckPosition(x, position));
    }

    public bool IsPositionBusyWithSame(Position position, Color color)
    {
        return this.figures.Any(x => IsSame(x.Color, color) && CheckPosition(x, position));
    }

    private static bool CheckPosition(Figure figure, Position position)
    {
        return figure.IsAlive && figure.Position == position;
    }

    private static bool IsSame(Color first, Color second)
    {
        return first == second;
    }

    private static bool IsOpposite(Color first, Color second)
    {
        return (first == Color.White && second == Color.Black) ||
               (first == Color.Black && second == Color.White);
    }

    internal IGameLogic Clone()
    {
        return new Game
        {
            figures = this.figures.Select(x => x.Clone()).ToArray(),
            lastMover = this.lastMover?.Clone()
        };
    }
}
