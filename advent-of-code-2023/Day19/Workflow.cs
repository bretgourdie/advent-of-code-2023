namespace advent_of_code_2023.Day19;
internal class Workflow
{
    private IList<Condition> conditions;

    public Workflow(string set)
    {
        conditions = new List<Condition>();

        var braceless = set
            .Replace("{", String.Empty)
            .Replace("}", String.Empty);

        var split = braceless.Split(',');

        foreach (var element in split)
        {
            conditions.Add(new Condition(element));
        }
    }

    public RuleResult Evaluate(Part part)
    {
        foreach (var condition in conditions)
        {
            if (condition.Evaluate(part))
            {
                return RuleResult.FromResultant(condition.Resultant);
            }
        }

        throw new ArgumentException("Condition does not have ending");
    }
}
