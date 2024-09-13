namespace advent_of_code_2023.Day05;
internal class RangeMap
{
    public readonly string Source;
    public readonly string Destination;

    public readonly IList<Mapping> Mappings;

    public RangeMap(
        string source,
        string destination)
    {
        Source = source;
        Destination = destination;

        Mappings = new List<Mapping>();
    }

    public void AddMapping(
        long destinationRangeStart,
        long sourceRangeStart,
        long rangeLength)
    {
        Mappings.Add(
            new Mapping(
                destinationRangeStart,
                sourceRangeStart,
                rangeLength));
    }

    public bool DestinationInRange(
        long trying) =>
        Mappings.Any(x => destinationInRange(trying, x));

    private bool destinationInRange(
        long trying,
        Mapping mapping) =>
        mapping.DestinationRangeStart <= trying
        && trying < mapping.DestinationRangeStart + mapping.RangeLength;

    private bool sourceInRange(
        long trying,
        Mapping mapping) =>
        mapping.SourceRangeStart <= trying
        && trying < mapping.SourceRangeStart + mapping.RangeLength;

    public long GetDestination(long source)
    {
        foreach (var mapping in Mappings)
        {
            if (sourceInRange(source, mapping))
            {
                return 
                    mapping.DestinationRangeStart
                    + (source - mapping.SourceRangeStart);
            }
        }

        return source;
    }

    public long GetSource(long destination)
    {
        foreach (var mapping in Mappings)
        {
            if (destinationInRange(destination, mapping))
            {
                return
                    mapping.SourceRangeStart
                    + (destination - mapping.DestinationRangeStart);
            }
        }

        return destination;
    }

    public struct Mapping
    {
        public readonly long DestinationRangeStart;
        public readonly long SourceRangeStart;
        public readonly long RangeLength;

        public Mapping(
            long destinationRangeStart,
            long sourceRangeStart,
            long rangeLength)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }
    }
}
