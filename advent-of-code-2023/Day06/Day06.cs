namespace advent_of_code_2023.Day06;
internal class Day06 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, getAllRaces);

    private long work(
        string[] input,
        Func<string[], IList<Race>> kerningStrategy)
    {

        var races = kerningStrategy(input);
        var results = new List<long>();

        foreach (var race in races)
        {
            var winningCombinations = 0;

            for (var holdTime = 1; holdTime < race.Time; holdTime++)
            {
                var remainingTime = race.Time - holdTime;
                var distance = remainingTime * holdTime;

                if (distance > race.RecordDistance)
                {
                    winningCombinations += 1;
                }
            }

            results.Add(winningCombinations);
        }

        return results.Aggregate((long)1, (x, y) => x * y);
    }

    private IList<Race> getAllRaces(string[] input)
    {
        var races = new List<Race>();

        var times = parseLine(input[0]);
        var recordDistances = parseLine(input[1]);

        for (int ii = 0; ii < times.Count && ii < recordDistances.Count; ii++)
        {
            races.Add(new Race(
                long.Parse(times[ii]),
                long.Parse(recordDistances[ii])
                )
            );
        }

        return races;
    }

    private IList<Race> getOneRace(string[] input)
    {
        var time = getKernedLine(input[0]);
        var recordDistance = getKernedLine(input[1]);

        var race = new Race(
            long.Parse(time),
            long.Parse(recordDistance)
        );

        return new List<Race>()
        {
            race
        };
    }

    private string getKernedLine(string line)
    {
        var labelAndValues = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
        var values = labelAndValues[1];
        var singleValue = values.Replace(" ", "");
        return singleValue;
    }

    private IList<string> parseLine(string line)
    {
        var labelAndValues = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
        var valuesOneLine = labelAndValues[1];
        var values = valuesOneLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return values;
    }

    private record Race(long Time, long RecordDistance);

    protected override long part1ExampleExpected => 288;
    protected override long part1InputExpected => 74698;

    protected override long part2Work(string[] input) =>
        work(input, getOneRace);

    protected override long part2ExampleExpected => 71503;
    protected override long part2InputExpected => 27563421;
}
