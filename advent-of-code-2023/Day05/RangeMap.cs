namespace advent_of_code_2023.Day05;
internal class RangeMap
{
    public readonly string Source;
    public readonly string Destination;

    private readonly IList<Mapping> mappings;

    public RangeMap(
        string source,
        string destination)
    {
        Source = source;
        Destination = destination;

        mappings = new List<Mapping>();
    }

    public void AddMapping(
        long destinationRangeStart,
        long sourceRangeStart,
        long rangeLength)
    {
        mappings.Add(
            new Mapping(
                destinationRangeStart,
                sourceRangeStart,
                rangeLength));
    }

    private bool sourceInRange(
        long trying,
        Mapping mapping) =>
        mapping.SourceRangeStart <= trying
        && trying < mapping.SourceRangeStart + mapping.RangeLength;

    public long GetDestination(long source)
    {
        foreach (var mapping in mappings)
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

    private struct Mapping
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
