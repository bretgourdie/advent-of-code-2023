using advent_of_code_2017;

namespace advent_of_code_2023.Day08;
internal class Day08 : AdventSolution
{
    protected new static string[] example1Filename() => new[] { "example1" };
    protected new static string[] example2Filename() => new[] { "example2" };

    protected override long part1Work(string[] input) =>
        work(input, new Living());

    private long work(
        string[] input,
        INavigationStrategy navigation)
    {
        var directions = input.First();

        var map = getMap(input);

        var locations = navigation.GetStart(map);
        long steps = 0;

        while (!navigation.IsDone(locations))
        {
            for (int ii = 0; ii < locations.Count; ii++)
            {
                var location = locations[ii];

                var currentDirection = (int)(steps % directions.Length);
                var direction = directions[currentDirection];

                var currentMap = map[location];

                if (direction == 'L')
                {
                    locations[ii] = currentMap[0];
                }

                else if (direction == 'R')
                {
                    locations[ii] = currentMap[1];
                }
            }

            steps += 1;
        }

        return steps;
    }

    private IDictionary<string, IList<string>> getMap(string[] input)
    {
        var map = new Dictionary<string, IList<string>>();

        for (int ii = 2; ii < input.Length; ii++)
        {
            var line = input[ii];

            var keyAndValues = line.Split(" = ");
            var values = keyAndValues[1]
                .Replace("(", "")
                .Replace(")", "")
                .Split(", ");

            map[keyAndValues[0]] = values;
        }

        return map;
    }

    protected override long part1ExampleExpected => 6;
    protected override long part1InputExpected => 15989;

    protected override long part2Work(string[] input) =>
        work(input, new Ghost());

    protected override long part2ExampleExpected => 6;
    protected override long part2InputExpected => -1;
}
