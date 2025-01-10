namespace advent_of_code_2023.Day19;
internal struct Part
{
    public readonly IDictionary<string, long> Attributes;

    public Part(string line)
    {
        Attributes = new Dictionary<string, long>();

        var braceless = line
            .Replace("{", String.Empty)
            .Replace("}", String.Empty);

        var split = braceless.Split(',');

        foreach (var element in split)
        {
            var equalSegment = element.Split('=');
            Attributes.Add(
                equalSegment[0],
                long.Parse(equalSegment[1])
            );
        }
    }

    public long GetSum() => Attributes.Sum(x => x.Value);
}
