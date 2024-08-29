using System.Text;
using advent_of_code_2017;

namespace advent_of_code_2023.Day01;
internal class Day01 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        long sum = 0;

        foreach (var line in input)
        {
            string firstDigit = String.Empty;
            string lastDigit = String.Empty;

            foreach (var letterChar in line)
            {
                var letterString = letterChar.ToString();

                if (long.TryParse(letterString, out long number))
                {
                    if (firstDigit == String.Empty)
                    {
                        firstDigit = letterString;
                    }

                    lastDigit = letterString;
                }
            }

            var combinedDigits = long.Parse(firstDigit + lastDigit);

            sum += combinedDigits;
        }

        return sum;
    }

    protected override long part1ExampleExpected => 142;
    protected override long part1InputExpected => 55208;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
