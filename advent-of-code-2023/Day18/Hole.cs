using System.Numerics;

namespace advent_of_code_2023.Day18;
internal struct Hole
{
    public int X;
    public int Y;
    public string Color;

    public Hole(Vector2 position, string color) : this(
        (int)position.X,
        (int)position.Y,
        color) { }

    public Hole (int x, int y, string color)
    {
        X = x;
        Y = y;
        Color = color;
    }
}
