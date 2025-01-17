namespace advent_of_code_2023.Day19;
internal class SortSystem
{
    private readonly IDictionary<string, Workflow> workflows;
    private const string startingWorkflow = "in";

    public SortSystem(IList<string> workflowInput)
    {
        workflows = new Dictionary<string, Workflow>();

        foreach (var line in workflowInput)
        {
            var split = line.Split('{');

            var rules = new Workflow(split[0], split[1]);

            workflows.Add(split[0], rules);
        }
    }

    public bool Accept(Part part)
    {
        var workflowName = startingWorkflow;

        while (true)
        {
            var result = workflows[workflowName].Evaluate(part);

            if (result.Accepted)
            {
                return true;
            }

            else if (result.Rejected)
            {
                return false;
            }

            else
            {
                workflowName = result.Workflow;
            }
        }

        throw new ArgumentException("Cannot sort part", nameof(part));
    }

    public long GetCombinations()
    {
        long sum = 0;

        var ranges = new List<PartRange>();

        foreach (var workflow in workflows.Values)
        {
            if (workflow.ProvidesAcceptance())
            {
                var resultRanges = getCombinations(workflow);
                ranges.AddRange(resultRanges);
            }
        }

        // probably need to fold combos here
        foreach (var range in ranges)
        {
            var combos = range.GetCombinations();
            sum += combos;
        }

        return sum;
    }

    private IList<PartRange> getCombinations(Workflow workflow)
    {

    }
}
