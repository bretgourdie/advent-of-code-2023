using advent_of_code_2017;

namespace advent_of_code_2023.Day05;
internal class Day05 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, seedsAsPoints);

    private long work(
        string[] input,
        Func<IEnumerable<long>, IList<long>> seedParseFunction)
    {
        var seeds = parseSeeds(input, seedParseFunction);
        var rangeMaps = parseMaps(input);
        var minLocation = long.MaxValue;
        const string finalSourceToDestination = "humidity";

        var locations = rangeMaps[finalSourceToDestination];
        var locationsSet = getSortedLocations(locations);
        var locationsSetSorted = locationsSet.OrderBy(x => x);

        foreach (var location in locationsSetSorted)
        {
            var value = location;
            var map = finalSourceToDestination;

            while (map != "seed" && rangeMaps.ContainsKey(map) && rangeMaps[map].DestinationInRange(value))
            {
                map = rangeMaps[map].Source;
                var newValue = rangeMaps[map].GetSource(value);

                if (newValue == value) break;

                if (map == "seed") return value;

                value = newValue;
            }
        }

        return long.MaxValue;
    }

    private ISet<long> getSortedLocations(RangeMap locations)
    {
        var set = new SortedSet<long>();

        foreach (var mapping in locations.Mappings)
        {
            for (long ii = 0; ii < mapping.RangeLength; ii++)
            {
                set.Add(ii + mapping.DestinationRangeStart);
            }
        }

        return set;
    }

    private IList<long> parseSeeds(
        string[] input,
        Func<IEnumerable<long>, IList<long>> parseSeedNumbersFunction)
    {
        var seedsLine = input.First();
        var seedsAndNumbers = seedsLine.Split(": ");
        var seedNumbersStr = seedsAndNumbers[1].Split(" ");
        var seedNumbers = seedNumbersStr.Select(x => long.Parse(x));
        return parseSeedNumbersFunction(seedNumbers);
    }

    private IList<long> seedsAsPoints(IEnumerable<long> seeds) => seeds.ToList();

    private IList<long> seedsAsRange(IEnumerable<long> seedRanges)
    {
        var list = new List<long>();

        for (int ii = 0; ii < seedRanges.Count(); ii += 2)
        {
            var start = seedRanges.ElementAt(ii);
            var length = seedRanges.ElementAt(ii + 1);

            for (long jj = 0; jj < length; jj++)
            {
                list.Add(jj + start);
            }
        }

        return list;
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
