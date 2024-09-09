using advent_of_code_2017;

namespace advent_of_code_2023.Day04;
internal class Day04 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, new Naive());

    private long work(
        string[] input,
        IScorable scoring)
    {
        foreach (var card in input)
        {
            var cardAndContent = card.Split(": ");
            var cardAndNumber = cardAndContent.First().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var cardNumberStr = cardAndNumber.Skip(1).First();

            var winningAndHave = cardAndContent[1].Split(" | ");

            var winningNumbers = parseNumbers(winningAndHave[0]);
            var haveNumbers = parseNumbers(winningAndHave[1]);

            var intersect = winningNumbers.Intersect(haveNumbers);

            if (intersect.Any())
            {
                scoring.RecordScore(long.Parse(cardNumberStr), intersect.Count());
            }
        }

        return scoring.GetScore();
    }

    private ISet<long> parseNumbers(string numberLine) =>
        parseNumbers(numberLine.Split(" ", StringSplitOptions.RemoveEmptyEntries));

    private ISet<long> parseNumbers(IEnumerable<string> numbers) =>
        new HashSet<long>(numbers.Select(x => long.Parse(x)));

    protected override long part1ExampleExpected => 13;
    protected override long part1InputExpected => 23673;

    protected override long part2Work(string[] input) =>
        work(input, new Instruction(input));

    protected override long part2ExampleExpected => 30;
    protected override long part2InputExpected => 12263631;
}
