using System.Numerics;

namespace advent_of_code_2023.Day16;
internal class Beam
{
    public Vector2 TravelDirection { get; private set; }
    public Vector2 Location { get; private set; }

    public Beam(
        Vector2 location,
        Vector2 travelDirection)
    {
        Location = location;
        TravelDirection = travelDirection;
    }

    public bool CanTravel(HashSet<Vector2>[][] traveledGrid)
    {
        var offset = Location + TravelDirection;
        int x = (int)offset.X;
        int y = (int)offset.Y;

        return
            0 <= y && y < traveledGrid.Length
            && 0 <= x && x < traveledGrid[y].Length
            && !traveledGrid[y][x].Contains(TravelDirection);
    }

    public void Travel(
        char[][] navigationGrid,
        HashSet<Vector2>[][] travelGrid,
        out Beam split)
    {
        split = null;

        var offset = Location + TravelDirection;
        var y = (int)offset.Y;
        var x = (int)offset.X;

        travelGrid[y][x].Add(TravelDirection);

        var symbol = navigationGrid[y][x];

        if (shouldSplit(symbol, TravelDirection))
        {
            split = new Beam(
                offset,
                getSplitDirection(symbol, TravelDirection)
            );
        }

        TravelDirection = getNewDirection(symbol, TravelDirection);
        Location = offset;
    }

    private bool shouldSplit(char symbol, Vector2 travelDirection)
    {
        return
            (symbol == '-' && travelDirection.IsYAxis())
            ||
            (symbol == '|' && travelDirection.IsXAxis());
    }

    private Vector2 getSplitDirection(char symbol, Vector2 travelDirection) =>
        getNewDirection(symbol, travelDirection).Opposite();

    private Vector2 getNewDirection(char symbol, Vector2 travelDirection)
    {
        if (symbol == '|')
        {
            if (travelDirection.IsXAxis())
            {
                return Direction.South;
            }
        }

        else if (symbol == '-')
        {
            if (travelDirection.IsYAxis())
            {
                return Direction.East;
            }
        }

        else if (symbol == '/')
        {
            if (travelDirection.IsYAxis())
            {
                return travelDirection.NextClockwise();
            }

            else
            {
                return travelDirection.NextCounterClockwise();
            }
        }

        else if (symbol == '\\')
        {
            if (travelDirection.IsYAxis())
            {
                return travelDirection.NextCounterClockwise();
            }

            else
            {
                return travelDirection.NextClockwise();
            }
        }

        return travelDirection;
    }
}
