namespace advent_of_code_2023.Day19;
internal class Day19 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var parts = getParts(input);

        var sortSystem = new SortSystem(getWorkflowLines(input));

        long sum = 0;

        foreach (var part in parts)
        {
            if (sortSystem.Accept(part))
            {
                sum += part.GetSum();
            }
        }

        return sum;
    }

    private IList<string> getWorkflowLines(string[] input)
    {
        var workflowLines = new List<string>();

        foreach (var line in input)
        {
            if (line == String.Empty)
            {
                return workflowLines;
            }

            workflowLines.Add(line);
        }

        throw new ArgumentException("Cannot find end to workflow lines", nameof(input));
    }

    private IList<Part> getParts(string[] input)
    {
        var parts = new List<Part>();

        foreach (var line in input)
        {
            if (line.StartsWith("{"))
            {
                parts.Add(new Part(line));
            }
        }

        return parts;
    }

    protected override long part1ExampleExpected => 19114;
    protected override long part1InputExpected => 391132;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
