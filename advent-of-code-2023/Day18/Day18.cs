namespace advent_of_code_2023.Day18;
internal class Day18 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, Plan.UseInstruction);

    private long work(
        string[] input,
        Plan plan)
    {
        var instructions = parseInstructions(input, plan);

        var moat = new Moat();
        moat.Build(instructions);

        var innerArea = moat.CalculateInnerArea();
        var perimeter = moat.PerimiterLength;

        return innerArea + perimeter / 2 + 1;
    }

    private IList<Instruction> parseInstructions(
        IList<string> input,
        Plan plan) =>
        input.Select(x => new Instruction(x, plan)).ToList();

    protected override long part1ExampleExpected => 62;
    protected override long part1InputExpected => 52231;

    protected override long part2Work(string[] input) =>
        work(input, Plan.UseColor);

    protected override long part2ExampleExpected => 952408144115;
    protected override long part2InputExpected => 57196493937398;
}
