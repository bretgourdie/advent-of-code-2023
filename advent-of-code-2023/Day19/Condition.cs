namespace advent_of_code_2023.Day19;
internal class Condition
{
    public readonly string Equation;
    public readonly string Attribute;
    public readonly Operation TheOperation;
    public readonly long Value;
    public readonly string Resultant;
    public readonly bool HasCondition;

    private const char conditionAndResultantSplitter = ':';

    public Condition(string equationAndResultant)
    {
        HasCondition = equationAndResultant.Contains(conditionAndResultantSplitter);

        if (HasCondition)
        {
            var split = equationAndResultant.Split(conditionAndResultantSplitter);
            Equation = split[0];
            Attribute = Equation[0].ToString();
            TheOperation = Equation[1] == '<' ? Operation.LessThan : Operation.GreaterThan;
            Value = long.Parse(Equation.Substring(2));

            Resultant = split[1];
        }

        else
        {
            Resultant = equationAndResultant;
        }
    }

    public bool Evaluate(Part part)
    {
        if (!HasCondition) return true;

        if (TheOperation == Operation.LessThan)
        {
            return part.Attributes[Attribute] < Value;
        }

        else
        {
            return part.Attributes[Attribute] > Value;
        }
    }

    public enum Operation
    {
        GreaterThan,
        LessThan
    }
}
