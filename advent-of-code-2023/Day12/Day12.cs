using advent_of_code_2017;

namespace advent_of_code_2023.Day12;
internal class Day12 : AdventSolution
{
    private const char unknown = '?';
    private const char operational = '.';
    private const char damaged = '#';
    private static IList<char> replacements = new List<char>() { operational, damaged };

    protected override long part1Work(string[] input) =>
        work(input, 1);

    private long work(
        string[] input,
        int numberOfExpansions)
    {
        long permutations = 0;

        foreach (var line in input)
        {
            var record = expandRecord(getRecord(line), numberOfExpansions);

            permutations += getPermutations(
                record,
                expandPattern(getPattern(line), numberOfExpansions),
                record
            );
        }

        return permutations;
    }

    private string expandRecord(
        string start,
        int numberOfExpansions)
    {
        return
            String.Join(unknown,
                Enumerable.Repeat(
                    start,
                    numberOfExpansions)
            );
    }

    private IList<int> expandPattern(
        IList<int> start,
        int numberOfExpansions)
    {
        return
            Enumerable.Repeat(start, numberOfExpansions)
                .SelectMany(x => x)
                .ToList();
    }

    private long getPermutations(
        string record,
        IList<int> pattern,
        string originalRecord)
    {
        if (record.Contains(unknown))
        {
            var unknownIndex = record.IndexOf(unknown);
            long permutations = 0;

            foreach (var replacement in replacements)
            {
                var chars = record.ToCharArray();
                chars[unknownIndex] = replacement;
                var newRecord = new string(chars);

                if (patternWorks(newRecord, pattern))
                {
                    permutations += getPermutations(
                        newRecord,
                        pattern,
                        originalRecord);
                }
            }

            return permutations;
        }

        if (patternWorks(record, pattern))
        {
            return 1;
        }

        return 0;
    }

    private bool patternWorks(string record, IList<int> pattern)
    {
        var queue = new Queue<int>(pattern);

        int recordLetterIndex = 0;
        while (queue.Any()
                && recordLetterIndex < record.Length
                && record[recordLetterIndex] != unknown)
        {
            var groupGoal = queue.Dequeue();
            var groupCount = 0;

            while (recordLetterIndex < record.Length
                   && record[recordLetterIndex] != damaged
                   && record[recordLetterIndex] != unknown)
            {
                recordLetterIndex += 1;
            }

            if (recordLetterIndex == record.Length)
            {
                return false;
            }

            while (recordLetterIndex < record.Length
                   && record[recordLetterIndex] == damaged
                   && record[recordLetterIndex] != unknown)
            {
                recordLetterIndex += 1;
                groupCount += 1;
            }

            if (recordLetterIndex == record.Length)
            {
                if (groupCount != groupGoal)
                {
                    return false;
                }
            }

            else if (record[recordLetterIndex] == unknown && groupCount <= groupGoal)
            {
                return true;
            }

            if (groupCount != groupGoal)
            {
                return false;
            }
        }

        if (queue.Any())
        {
            return false;
        }

        if (recordLetterIndex + 1 < record.Length && record.Substring(recordLetterIndex + 1).Contains(damaged))
        {
            return false;
        }

        return true;
    }

    private string getRecord(string line)
    {
        return line.Split(" ").First();
    }

    private IList<int> getPattern(string line)
    {
        return
            line.Split(" ")
                .Skip(1)
                .First()
                .Split(",")
                .Select(int.Parse)
                .ToList();
    }

    protected override long part1ExampleExpected => 21;
    protected override long part1InputExpected => 8419;

    protected override long part2Work(string[] input) =>
        work(input, 5);

    protected override long part2ExampleExpected => 525152;
    protected override long part2InputExpected => -1;
}
