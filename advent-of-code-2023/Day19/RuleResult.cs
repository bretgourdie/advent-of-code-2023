namespace advent_of_code_2023.Day19;
internal struct RuleResult
{
    public readonly string Workflow;
    public readonly bool Accepted;
    public readonly bool Rejected;

    public static RuleResult ToWorkflow(string workflow) =>
        new RuleResult(workflow, false, false);

    public static RuleResult ToAccepted() =>
        new RuleResult(String.Empty, true, false);

    public static RuleResult ToRejected() =>
        new RuleResult(String.Empty, false, true);

    public static RuleResult FromResultant(string resultant)
    {
        if (resultant == "A") return ToAccepted();
        if (resultant == "R") return ToRejected();
        return ToWorkflow(resultant);
    }

    private RuleResult(
        string workflow,
        bool accepted,
        bool rejected)
    {
        Workflow = workflow;
        Accepted = accepted;
        Rejected = rejected;
    }
}
