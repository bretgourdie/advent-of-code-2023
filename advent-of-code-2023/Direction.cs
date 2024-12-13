using System.Numerics;

namespace advent_of_code_2023;
internal static class Direction
{
    private static readonly Lazy<Vector2> _north = new Lazy<Vector2>(new Vector2(0, -1));
    public static readonly Vector2 North = _north.Value;

    private static readonly Lazy<Vector2> _south = new Lazy<Vector2>(new Vector2(0, +1));
    public static readonly Vector2 South = _south.Value;

    private static readonly Lazy<Vector2> _east = new Lazy<Vector2>(new Vector2(+1, 0));
    public static readonly Vector2 East = _east.Value;
    
    private static readonly Lazy<Vector2> _west = new Lazy<Vector2>(new Vector2(-1, 0));
    public static readonly Vector2 West = _west.Value;

    public static readonly Vector2 None = Vector2.Zero;

    private static readonly Lazy<IList<Vector2>> _clockwise = new (new List<Vector2>()
    {
        North,
        East,
        South,
        West
    });

    public static readonly IList<Vector2> Clockwise = _clockwise.Value;

    public static string ToDirectionName(this Vector2 direction)
    {
        if (direction == North) return nameof(North);
        else if (direction == South) return nameof(South);
        else if (direction == East) return nameof(East);
        else if (direction == West) return nameof(West);

        return nameof(None);
    }

    public static bool IsYAxis(this Vector2 direction) =>
        direction == North || direction == South;

    public static bool IsXAxis(this Vector2 direction) =>
        direction == East || direction == West;

    public static Vector2 Opposite(this Vector2 direction) =>
        direction.NextClockwise().NextClockwise();

    public static Vector2 NextClockwise(this Vector2 direction) =>
        nextCircularList(direction, 1);

    public static Vector2 NextCounterClockwise(this Vector2 direction) =>
        nextCircularList(direction, -1);

    private static Vector2 nextCircularList(
        Vector2 direction,
        int movingDirection)
    {
        if (direction == None) throw new ArgumentException("Cannot use None direction", nameof(direction));

        var index = Clockwise.IndexOf(direction);

        var nextIndex = ((Clockwise.Count + index) + movingDirection) % Clockwise.Count;

        return Clockwise[nextIndex];
    }
}
