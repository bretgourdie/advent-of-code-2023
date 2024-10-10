using advent_of_code_2017;

namespace advent_of_code_2023.Day12;
internal class Day12 : AdventSolution
{
    private const string unknown = "?";
    private const char operational = '.';
    private const char damaged = '#';
    private static IList<char> replacements = new List<char>() { operational, damaged };

    protected override long part1Work(string[] input)
    {
        long permutations = 0;

        foreach (var line in input)
        {
            permutations += getPermutations(
                getRecord(line),
                getPattern(line));
        }

        return permutations;
    }

    private long getPermutations(
        string record,
        IList<int> pattern)
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

                permutations += getPermutations(newRecord, pattern);
            }

            return permutations;
        }

        var queue = new Queue<int>(pattern);

        int patternIndex = 0;
        while (queue.Any())
        {
            var groupGoal = queue.Dequeue();
            var groupCount = 0;

            while (patternIndex < record.Length && record[patternIndex] != damaged)
            {
                patternIndex += 1;
            }

            if (patternIndex == record.Length) return 0;

            while (patternIndex < record.Length && record[patternIndex] == damaged)
            {
                patternIndex += 1;
                groupCount += 1;
            }

            if (groupCount != groupGoal)
            {
                return 0;
            }
        }

        if (patternIndex + 1 < record.Length && record.Substring(patternIndex + 1).Contains(damaged))
        {
            return 0;
        }

        return 1;
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
    protected override long part1InputExpected => -1;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
