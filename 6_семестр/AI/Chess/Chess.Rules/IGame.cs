namespace Chess.Rules;
public interface IGame
{
    Task<bool> IsGameOver();

    Task<bool> TryMove(string move);
    void Move(string move);
}
