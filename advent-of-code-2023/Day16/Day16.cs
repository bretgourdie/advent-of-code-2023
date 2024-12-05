using System.Numerics;
using System.Text;

namespace advent_of_code_2023.Day16;
internal class Day16 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        shootBeam(input.To2DChar(), new Vector2(-1, 0), Direction.East);

    private long shootBeam(
        char[][] navigationGrid,
        Vector2 start,
        Vector2 direction)
    {
        var travelGrid = getTravelGrid(navigationGrid);

        var beams = new List<Beam>() { new Beam(start, direction) };

        while (beams.Any(x => x.CanTravel(travelGrid)))
        {
            var newBeams = new List<Beam>();

            foreach (var beam in beams)
            {
                if (beam.CanTravel(travelGrid))
                {
                    beam.Travel(navigationGrid, travelGrid, out Beam split);

                    if (split != null)
                    {
                        newBeams.Add(split);
                    }
                }
            }

            newBeams.ForEach(x => beams.Add(x));
        }

        return calculateEnergized(travelGrid);
    }

    private long calculateEnergized(HashSet<Vector2>[][] travelGrid)
    {
        long count = 0;

        for (int y = 0; y < travelGrid.Length; y++)
        {
            for (int x = 0; x < travelGrid[y].Length; x++)
            {
                if (travelGrid[y][x].Any())
                {
                    count += 1;
                }
            }
        }

        return count;
    }

    private HashSet<Vector2>[][] getTravelGrid(char[][] navigationGrid)
    {
        var travelGrid = new HashSet<Vector2>[navigationGrid.Length][];

        for (int ii = 0; ii < navigationGrid.Length; ii++)
        {
            travelGrid[ii] = new HashSet<Vector2>[navigationGrid[ii].Length];

            for (int jj = 0; jj < navigationGrid[ii].Length; jj++)
            {
                travelGrid[ii][jj] = new HashSet<Vector2>();
            }
        }

        return travelGrid;
    }

    protected override long part1ExampleExpected => 46;
    protected override long part1InputExpected => 7562;
    protected override long part2Work(string[] input)
    {
        var navigationGrid = input.To2DChar();

        long max = long.MinValue;

        for (int y = 0; y < navigationGrid.Length; y++)
        {
            foreach (var direction in Direction.Clockwise)
            {
                var leftResult = shootBeam(
                    navigationGrid,
                    new Vector2(-1, y),
                    direction);


                max = Math.Max(max, leftResult);

                var rightResult = shootBeam(
                    navigationGrid,
                    new Vector2(navigationGrid.Length, y),
                    direction);

                max = Math.Max(max, rightResult);
            }
        }

        for (int x = 0; x < navigationGrid[0].Length; x++)
        {
            foreach (var direction in Direction.Clockwise)
            {
                var upResult = shootBeam(
                    navigationGrid,
                    new Vector2(x, -1),
                    direction);

                max = Math.Max(max, upResult);

                var downResult = shootBeam(
                    navigationGrid,
                    new Vector2(x, navigationGrid[0].Length),
                    direction);

                max = Math.Max(max, downResult);
            }
        }

        return max;
    }

    protected override long part2ExampleExpected => 51;
    protected override long part2InputExpected => 7793;
}
