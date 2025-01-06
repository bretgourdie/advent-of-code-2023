using System.Numerics;

namespace advent_of_code_2023.Day18;
internal class Moat
{
    private Vector2 position;
    private readonly IList<Vector2> points;
    public int PerimiterLength { get; private set; }

    public Moat()
    {
        position = Vector2.Zero;
        points = new List<Vector2>() { Vector2.Zero };
    }

    public void Build(IList<Instruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            Build(instruction.TheDirection, instruction.Holes);
        }
    }

    public void Build(
        Vector2 direction,
        int length)
    {
        position += direction * length;
        PerimiterLength += length;
        points.Add(position);
    }

    public long CalculateInnerArea()
    {
        double area = 0;
        
        for (int ii = 0; ii < points.Count - 1; ii++)
        {
            var subarea = shoestringArea(points[ii], points[ii + 1]);
            area += subarea;
        }

        var halfArea = area / 2;

        return (long)halfArea;
    }

    private long shoestringArea(
        Vector2 a,
        Vector2 b)
    {
        var x1 = (long)a.X;
        var y1 = (long)a.Y;

        var x2 = (long)b.X;
        var y2 = (long)b.Y;

        var x1y2 = x1 * y2;
        var y1x2 = y1 * x2;

        var diff = x1y2 - y1x2;

        return diff;
    }
}
