using System.Numerics;

namespace advent_of_code_2023;
internal class Direction
{
    private static readonly Lazy<Vector2> _north = new Lazy<Vector2>(new Vector2(0, -1));
    public static readonly Vector2 North = _north.Value;

    private static readonly Lazy<Vector2> _south = new Lazy<Vector2>(new Vector2(0, +1));
    public static readonly Vector2 South = _south.Value;

    private static readonly Lazy<Vector2> _east = new Lazy<Vector2>(new Vector2(+1, 0));
    public static readonly Vector2 East = _east.Value;
    
    private static readonly Lazy<Vector2> _west = new Lazy<Vector2>(new Vector2(-1, 0));
    public static readonly Vector2 West = _west.Value;
}
