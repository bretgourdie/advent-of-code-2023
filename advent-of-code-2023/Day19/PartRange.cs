namespace advent_of_code_2023.Day19;
internal class PartRange
{
    private readonly IDictionary<string, long> mins;
    private readonly IDictionary<string, long> maxs;

    private readonly IList<string> attributes = new[] { "x", "m", "a", "s" };
    private const long defaultMin = 1;
    private const long defaultMax = 4000;

    public PartRange()
    {
        mins = new Dictionary<string, long>();
        maxs = new Dictionary<string, long>();

        foreach (var attribute in attributes)
        {
            mins[attribute] = defaultMin;
            maxs[attribute] = defaultMax;
        }
    }

    public long GetCombinations()
    {
        long sum = 0;

        foreach (var attribute in attributes)
        {
            sum *= maxs[attribute] - mins[attribute];
        }

        return sum;
    }

    public bool EvaluateForApproval(string attribute, Condition.Operation operation, long value)
    {
        if (operation == Condition.Operation.LessThan)
        {
            maxs[attribute] = Math.Min(maxs[attribute], value);
        }

        else if (operation == Condition.Operation.GreaterThan)
        {
            mins[attribute] = Math.Max(mins[attribute], value);
        }

        if (maxs[attribute] < mins[attribute]
            || mins[attribute] > maxs[attribute])
        {
            return false;
        }

        return true;
    }
}
