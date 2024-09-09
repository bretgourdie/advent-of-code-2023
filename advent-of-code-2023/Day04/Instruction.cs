namespace advent_of_code_2023.Day04;
internal class Instruction : IScorable
{
    private readonly IDictionary<long, long> cardNumberToCardCount;

    public Instruction(string[] input)
    {
        cardNumberToCardCount = initializeCardLookup(input.Length);
    }

    private IDictionary<long, long> initializeCardLookup(int length)
    {
        var dict = new Dictionary<long, long>();

        for (int ii = 0; ii < length; ii++)
        {
            dict[ii + 1] = 1;
        }

        return dict;
    }

    public void RecordScore(long cardNumber, long score)
    {
        for (long cardCount = 0; cardCount < cardNumberToCardCount[cardNumber]; cardCount++)
        {
            var startingCard = cardNumber + 1;
            for (long scoreCount = 0; scoreCount < score && cardNumberToCardCount.ContainsKey(startingCard + scoreCount); scoreCount++)
            {
                cardNumberToCardCount[startingCard + scoreCount] += 1;
            }
        }
    }

    public long GetScore() =>
        cardNumberToCardCount.Sum(x => x.Value);
}
