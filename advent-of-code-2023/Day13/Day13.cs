namespace advent_of_code_2023.Day13;
internal class Day13 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, Grid.Smudging.No);

    private long work(
        string[] input,
        Grid.Smudging smudingStrategy)
    {
        var grids = getGrids(input);
        long total = 0;

        foreach (var grid in grids)
        {
            var result = grid.FindSymmetry(smudingStrategy);
            total += result;
        }

        return total;
    }

    private IList<Grid> getGrids(string[] input)
    {
        var listOfGrids = new List<Grid>();

        var gridBuildup = new List<string>();

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                listOfGrids.Add(
                    new Grid(
                        gridBuildup.ToArray()
                    )
                );
                gridBuildup = new List<string>();
            }

            else
            {
                gridBuildup.Add(line);
            }
        }

        listOfGrids.Add(new Grid(gridBuildup.ToArray()));

        return listOfGrids;
    }

    protected override long part1ExampleExpected => 405;
    protected override long part1InputExpected => 29165;

    protected override long part2Work(string[] input) =>
        work(input, Grid.Smudging.Yes);

    protected override long part2ExampleExpected => 400;
    protected override long part2InputExpected => 32192;
}
