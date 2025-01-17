namespace advent_of_code_2023.Day19;
internal class Condition
{
    public readonly string Equation;
    public readonly string Attribute;
    public readonly Operation TheOperation;
    public readonly long Value;
    public readonly string Resultant;

    private const char conditionAndResultantSplitter = ':';

    public Condition(string equationAndResultant)
    {
        if (equationAndResultant.Contains(conditionAndResultantSplitter))
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
            Attribute = String.Empty;
            Equation = String.Empty;
            TheOperation = Operation.None;
            Resultant = equationAndResultant;
        }
    }

    public bool Evaluate(Part part)
    {
        if (TheOperation == Operation.LessThan)
        {
            return part.Attributes[Attribute] < Value;
        }

        else if (TheOperation == Operation.GreaterThan)
        {
            return part.Attributes[Attribute] > Value;
        }

        else
        {
            return true;
        }
    }

    public enum Operation
    {
        GreaterThan,
        LessThan,
        None
    }
}
