using advent_of_code_2017;

namespace advent_of_code_2023.Day03;
internal class Day03 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        long sum = 0;

        for (int ii = 0; ii < input.Length; ii++)
        {
            ISchematicState state = new OnSymbol(sum);

            for (int jj = 0; jj < input[ii].Length; jj++)
            {
                state = state.Handle(input, jj, ii);
            }

            sum = state.GetSum();
        }

        return sum;
    }

    protected override long part1ExampleExpected => 4361;
    protected override long part1InputExpected => 528819;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
