namespace Day02Problem1.Games
{
    public interface IGameFactory
    {
        IGame Create(string input);
    }
}