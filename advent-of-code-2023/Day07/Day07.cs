using advent_of_code_2017;

namespace advent_of_code_2023.Day07;
internal class Day07 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, new Jack());

    private long work(string[] input,
        IJStrategy jStrategy)
    {

        var orderedHands = getOrderedHands(getHands(input, jStrategy));
        long winnings = 0;

        for (int ii = 0; ii < orderedHands.Count; ii++)
        {
            var hand = orderedHands[ii];

            var rank = ii + 1;

            winnings += rank * hand.Bid;
        }

        return winnings;
    }

    private IList<Hand> getHands(
        string[] input,
        IJStrategy jStrategy)
    {
        var hands = new List<Hand>();

        foreach (var line in input)
        {
            var split = line.Split(" ");
            var hand = new Hand(
                split[0],
                long.Parse(split[1]),
                jStrategy);
            hands.Add(hand);
        }

        return hands;
    }

    private IList<Hand> getOrderedHands(IList<Hand> hands) =>
        hands.OrderBy(x => x.HandScore)
            .ThenBy(x => x.CardScore).ToList();

    protected override long part1ExampleExpected => 6440;
    protected override long part1InputExpected => 251106089;

    protected override long part2Work(string[] input) =>
        work(input, new Joker());

    protected override long part2ExampleExpected => 5905;
    protected override long part2InputExpected => 249620106;
}
