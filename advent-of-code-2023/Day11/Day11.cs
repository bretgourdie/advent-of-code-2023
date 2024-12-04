using System.Numerics;

namespace advent_of_code_2023.Day11;
internal class Day11 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, 2);
    
    private long work(
        string[] input,
        long expansionSize)
    {
        var rowExpansions = getRowExpansions(input);
        var columnExpansions = getColumnExpansions(input);

        var shortestPaths = getShortestPaths(getGalaxyCoords(input), rowExpansions, columnExpansions, expansionSize - 1);

        return shortestPaths.Sum();
    }

    private IList<int> getRowExpansions(string[] input)
    {
        var rowsToInsert = new List<int>();
        for (int ii = 0; ii < input.Length; ii++)
        {
            if (input[ii].All(x => x == '.'))
            {
                rowsToInsert.Add(ii);
            }
        }

        return rowsToInsert;
    }

    private IList<int> getColumnExpansions(string[] input)
    {
        var columnsToInsert = new List<int>();
        for (int ii = 0; ii < input[0].Length; ii++)
        {
            bool shouldAdd = true;
            for (int jj = 0; jj < input.Length && shouldAdd; jj++)
            {
                shouldAdd &= input[jj][ii] == '.';
            }

            if (shouldAdd)
            {
                columnsToInsert.Add(ii);
            }
        }

        return columnsToInsert;
    }

    private IEnumerable<long> getShortestPaths(
        IList<Vector2> coords,
        IList<int> rowExpansions,
        IList<int> columnExpansions,
        long spaceToAdd)
    {
        var pairs = generatePairs(coords);
        var dict = new Dictionary<string, long>();
        foreach (var pair in pairs)
        {
            dict[pair] = long.MaxValue;
        }

        for (int ii = 0; ii < coords.Count; ii++)
        {
            var source = coords[ii];

            for (int jj = 0; jj < coords.Count; jj++)
            {
                if (ii == jj) continue;

                var target = coords[jj];

                var rowsBetween = between(source.Y, target.Y, rowExpansions);
                var columnsBetween = between(source.X, target.X, columnExpansions);

                var xDistance = (long)Math.Abs(target.X - source.X);
                var yDistance = (long)Math.Abs(target.Y - source.Y);
                var rowsSeparating = rowsBetween * spaceToAdd;
                var columnsSeparating = columnsBetween * spaceToAdd;

                var distance = xDistance + yDistance + rowsSeparating + columnsSeparating;

                var pair = getPair(source, target);
                dict[pair] = Math.Min(dict[pair], distance);
            }
        }

        return dict.Values;
    }

    private int between(float fSource, float fTarget, IList<int> expansions)
    {
        var source = (int)fSource;
        var target = (int)fTarget;

        return expansions
            .Count(x => Math.Min(source, target) < x
                        && x < Math.Max(source, target));
    }

    private ISet<string> generatePairs(IList<Vector2> coords)
    {
        var set = new HashSet<string>();

        for (int ii = 0; ii < coords.Count; ii++)
        {
            for (int jj = ii + 1; jj < coords.Count; jj++)
            {
                var joined = getPair(coords[ii], coords[jj]);
                set.Add(joined);
            }
        }

        return set;
    }

    private string getPair(Vector2 first, Vector2 second)
    {
        var pair = new Vector2[2];
        pair[0] = first;
        pair[1] = second;

        var ordered = pair
            .OrderBy(point => point.X)
            .ThenBy(point => point.Y)
            .Select(point => $"({point.X},{point.Y}");

        var joined = String.Join(",", ordered);

        return joined;
    }

    private IList<Vector2> getGalaxyCoords(IList<string> grid)
    {
        var list = new List<Vector2>();

        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == '#')
                {
                    list.Add(
                        new Vector2(x, y));
                }
            }
        }

        return list;
    }

    protected override long part1ExampleExpected => 374;
    protected override long part1InputExpected => 9795148;

    protected override long part2Work(string[] input) =>
        work(input, 1_000_000);

    protected override long part2ExampleExpected => 82000210;

    protected override long part2InputExpected => 650672493820;
}
