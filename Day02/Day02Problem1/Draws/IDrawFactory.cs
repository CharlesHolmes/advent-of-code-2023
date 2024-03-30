namespace Day02Problem1.Draws
{
    public interface IDrawFactory
    {
        IDraw Create(string input);
    }
}