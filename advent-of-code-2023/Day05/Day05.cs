using advent_of_code_2017;

namespace advent_of_code_2023.Day05;
internal class Day05 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, seedsAsPoints);

    private long work(
        string[] input,
        Func<IEnumerable<long>, IEnumerable<long>> seedParseFunction)
    {
        var seeds = parseSeeds(input, seedParseFunction);
        var rangeMaps = parseMaps(input);
        var minLocation = long.MaxValue;

        foreach (var seed in seeds)
        {
            var value = seed;
            var map = "seed";

            while (map != "location")
            {
                var rangeMap = rangeMaps[map];
                value = rangeMap.GetDestination(value);
                map = rangeMap.Destination;
            }

            minLocation = Math.Min(minLocation, value);
        }

        return minLocation;
    }

    private IEnumerable<long> parseSeeds(
        string[] input,
        Func<IEnumerable<long>, IEnumerable<long>> parseSeedNumbersFunction)
    {
        var seedsLine = input.First();
        var seedsAndNumbers = seedsLine.Split(": ");
        var seedNumbersStr = seedsAndNumbers[1].Split(" ");
        var seedNumbers = seedNumbersStr.Select(x => long.Parse(x));
        return parseSeedNumbersFunction(seedNumbers);
    }

    private IEnumerable<long> seedsAsPoints(IEnumerable<long> seeds) => seeds;

    private IEnumerable<long> seedsAsRange(IEnumerable<long> seedRanges)
    {
        for (int ii = 0; ii < seedRanges.Count(); ii += 2)
        {
            var start = seedRanges.ElementAt(ii);
            var length = seedRanges.ElementAt(ii + 1);

            for (long jj = 0; jj < length; jj++)
            {
                yield return jj + start;
            }
        }
    }

    private IDictionary<string, RangeMap> parseMaps(string[] input)
    {
        var mapsStart = input.Skip(2);
        var theMaps = new Dictionary<string, RangeMap>();

        string source = String.Empty;
        string destination = String.Empty;

        foreach (var line in mapsStart)
        {
            if (line.Contains(" map:"))
            {
                var sourcePlusDestinationAndMap = line.Split(" ");
                var sourceAndMap = sourcePlusDestinationAndMap.First().Split("-to-");

                source = sourceAndMap[0];
                destination = sourceAndMap[1];
            }

            else if (!String.IsNullOrEmpty(line))
            {
                var numberSplit = line.Split(" ");
                var destinationRange = long.Parse(numberSplit[0]);
                var sourceRange = long.Parse(numberSplit[1]);
                var length = long.Parse(numberSplit[2]);

                if (!theMaps.ContainsKey(source))
                {
                    theMaps[source] = new RangeMap(source, destination);
                }

                theMaps[source].AddMapping(
                    destinationRange,
                    sourceRange,
                    length);
            }
        }

        return theMaps;
    }

    protected override long part1ExampleExpected => 35;
    protected override long part1InputExpected => 318728750;

    protected override long part2Work(string[] input) =>
        work(input, seedsAsRange);

    protected override long part2ExampleExpected => 46;
    protected override long part2InputExpected => -1;
}
