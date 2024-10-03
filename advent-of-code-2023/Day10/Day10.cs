using System.Numerics;
using advent_of_code_2017;

namespace advent_of_code_2023.Day10;
internal class Day10 : AdventSolution
{
    private enum AnswerStrategy
    {
        Depth,
        Enclosure
    }

    private enum Direction
    {
        North,
        South,
        East,
        West
    }

    private IDictionary<Direction, string> directionToPipe = new Dictionary<Direction, string>()
    {
        { Direction.North, "|7F" },
        { Direction.South, "|LJ" },
        { Direction.East, "-J7" },
        { Direction.West, "-LF" }
    };

    private IDictionary<Direction, Vector2> directionToOffset = new Dictionary<Direction, Vector2>()
    {
        { Direction.North, new Vector2(0, -1) },
        { Direction.South, new Vector2(0, 1) },
        { Direction.West, new Vector2(-1, 0) },
        { Direction.East, new Vector2(1, 0) }
    };

    private IDictionary<Direction, Direction> opposite = new Dictionary<Direction, Direction>()
    {
        { Direction.North, Direction.South },
        { Direction.South, Direction.North },
        { Direction.West, Direction.East },
        { Direction.East, Direction.West }
    };

    protected override long part1Work(string[] input) =>
        work(input, AnswerStrategy.Depth);

    private long work(
        string[] input,
        AnswerStrategy answerStrategy)
    {
        IList<Vector2> points = new List<Vector2>() { findStart(input) };
        var grid = getFillableGrid(input);
        long steps = 1;

        while ((points = flood(input, points, grid, steps)).Any())
        {
            steps += 1;
        }

        if (answerStrategy == AnswerStrategy.Depth)
        {
            return steps - 1;
        }

        var enclosed = findEnclosed(input, grid);

        return enclosed;
    }

    private long findEnclosed(
        string[] input,
        long[][] grid)
    {
        for (int y = 0; y < grid.Length; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (!(x == 0 || y == 0 || y == grid.Length - 1 || x == grid[y].Length - 1)) continue;


            }
        }

        throw new NotImplementedException();
    }

    private IList<Vector2> flood(
        string[] input,
        IList<Vector2> points,
        long[][] grid,
        long steps)
    {
        var newPoints = new List<Vector2>();

        foreach (var point in points)
        {
            var x = (int)point.X;
            var y = (int)point.Y;

            foreach (var direction in directionToOffset.Keys)
            {
                var from = input[y][x];
                var oppositeDirection = opposite[direction];
                if (from == 'S' || directionToPipe[oppositeDirection].Contains(from))
                {
                    var offset = directionToOffset[direction];
                    var xOffset = (int)offset.X;
                    var yOffset = (int)offset.Y;

                    var xNew = x + xOffset;
                    var yNew = y + yOffset;

                    if (0 <= yNew
                        && yNew < input.Length 
                        && 0 <= xNew
                        && xNew < input[yNew].Length)
                    {
                        var nextPipe = input[yNew][xNew];

                        if (directionToPipe[direction].Contains(nextPipe))
                        {
                            if (grid[yNew][xNew] == 0)
                            {
                                newPoints.Add(new Vector2(xNew, yNew));
                                grid[yNew][xNew] = steps;
                            }
                        }
                    }
                }
            }
        }

        return newPoints;
    }

    private long[][] getFillableGrid(string[] input)
    {
        var grid = new long[input.Length][];

        for (int ii = 0; ii < input.Length; ii++)
        {
            grid[ii] = new long[input[ii].Length];
        }

        return grid;
    }

    private Vector2 findStart(string[] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S') return new Vector2(x, y);
            }
        }

        throw new ArgumentException("S not found");
    }

    protected override long part1ExampleExpected => 8;
    protected override long part1InputExpected => 6923;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
