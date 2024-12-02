using advent_of_code_2017;

namespace advent_of_code_2023.Day15;
internal class Day15 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        long total = 0;
        var split = input.First().Split(',');

        foreach (var token in split)
        {
            total += hash(token);
        }

        return total;
    }

    private int hash(string toHash)
    {
        int currentValue = 0;

        foreach (var letter in toHash)
        {
            var code = (int)letter;
            currentValue += code;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
    }

    protected override long part1ExampleExpected => 1320;
    protected override long part1InputExpected => 506269;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
