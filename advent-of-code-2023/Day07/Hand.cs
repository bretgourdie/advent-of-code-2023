namespace advent_of_code_2023.Day07;
internal class Hand
{
    public readonly string Cards;
    public readonly long HandScore;
    public readonly long CardScore;
    public readonly long Bid;

    private const long
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1;

    public Hand(
        string cards,
        long bid,
        IJStrategy jStrategy)
    {
        Cards = cards;
        HandScore = determineHandType(jStrategy.GetGroupings(cards));
        CardScore = calculateHighCardScore(cards, jStrategy.GetJCardStrengths());
        Bid = bid;
    }

    private long calculateHighCardScore(
        string cards,
        string cardStrength)
    {
        long score = 0;

        for (int ii = 0; ii < cards.Length; ii++)
        {
            var card = cards[ii];
            var cardValue = cardStrength.Length - cardStrength.IndexOf(card);
            var multiplier = (long)Math.Pow(cardStrength.Length, cards.Length - ii);

            score += cardValue * multiplier;
        }

        return score;
    }

    private long determineHandType(IEnumerable<int> grouped)
    {
        if (grouped.Any(x => x == 5)) return FiveOfAKind;
        if (grouped.Any(x => x == 4)) return FourOfAKind;
        if (grouped.Any(x => x == 3) && grouped.Any(x => x == 2))
            return FullHouse;
        if (grouped.Any(x => x == 3)) return ThreeOfAKind;
        if (grouped.Count(x => x == 2) == 2) return TwoPair;
        if (grouped.Any(x => x == 2)) return OnePair;
        return HighCard;
    }
}
