﻿using advent_of_code_2017;

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

        var cycleDetectors = new List<CycleDetection>();
        foreach (var location in locations)
        {
            cycleDetectors.Add(new CycleDetection());
        }

        while (!IsDone(locations, cycleDetectors))
        {
            for (int ii = 0; ii < locations.Count; ii++)
            {
                var cycleDetector = cycleDetectors[ii];
                if (!cycleDetector.CycleDetected)
                {
                    var location = locations[ii];

                    var currentDirection = (int)(steps % directions.Length);
                    var direction = directions[currentDirection];

                    var currentMap = map[location];

                    cycleDetector.Record(location, currentMap[0], currentMap[1], currentDirection);

                    if (direction == 'L')
                    {
                        locations[ii] = currentMap[0];
                    }

                    else if (direction == 'R')
                    {
                        locations[ii] = currentMap[1];
                    }
                }
            }

            steps += 1;
        }

        if (cycleDetectors.All(x => x.CycleDetected))
        {
            return LCM(
                cycleDetectors
                    .Select(x => (long)x.Length)
                    .ToList());
        }

        else
        {
            return steps;
        }
    }

    private long LCM(IList<long> numbers)
    {
        return numbers.Aggregate(lcm);
    }

    private long lcm(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    private long GCD(long a, long b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

    public bool IsDone(
        IList<string> locations,
        IList<CycleDetection> cycleDetectors) =>
        locations.All(x => x.EndsWith("Z"))
        || cycleDetectors.All(x => x.CycleDetected);

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
