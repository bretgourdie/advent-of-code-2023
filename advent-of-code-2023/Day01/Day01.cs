namespace advent_of_code_2023.Day01;

internal class Day01 : AdventSolution
{

    private IDictionary<string, long> wordToNumber = new Dictionary<string, long>()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4},
        { "five", 5},
        { "six", 6},
        { "seven", 7},
        { "eight", 8},
        { "nine", 9}
    };

    private long work(
        string[] input,
        Func<int, string, string> getNumber)
    {
        long sum = 0;

        foreach (var line in input)
        {
            string firstDigit = String.Empty;
            string lastDigit = String.Empty;

            for (int ii = 0; ii < line.Length; ii++)
            {
                var number = getNumber(ii, line);

                if (number != String.Empty)
                {
                    if (firstDigit == String.Empty)
                    {
                        firstDigit = number;
                    }

                    lastDigit = number;
                }
            }

            if (firstDigit != String.Empty && lastDigit != String.Empty)
            {
                var combinedDigits = long.Parse(firstDigit + lastDigit);

                sum += combinedDigits;
            }
        }

        return sum;
    }

    private string getRealNumber(int index, string line)
    {
        var first = line.Substring(index).First().ToString();
        if (long.TryParse(first, out long number))
        {
            return first;
        }

        return String.Empty;
    }

    private string getRealOrWrittenNumber(int index, string line)
    {
        var realNumber = getRealNumber(index, line);
        if (realNumber != String.Empty)
        {
            return realNumber;
        }

        var potential = line.Substring(index);
        var starts = wordToNumber.Keys.Where(word => potential.StartsWith(word));

        var start = starts.SingleOrDefault();
        if (start != null)
        {
            return wordToNumber[start].ToString();
        }

        return String.Empty;
    }

    protected override long part1Work(string[] input) => work(input, getRealNumber);

    protected override long part1ExampleExpected => 209;
    protected override long part1InputExpected => 55208;
    protected override long part2Work(string[] input) => work(input, getRealOrWrittenNumber);

    protected override long part2ExampleExpected => 281;
    protected override long part2InputExpected => 54578;
}
