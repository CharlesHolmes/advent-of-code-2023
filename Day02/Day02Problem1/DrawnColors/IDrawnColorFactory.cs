namespace Day02Problem1.DrawnColors
{
    public interface IDrawnColorFactory
    {
        IDrawnColor Create(string drawnColorString);
    }
}