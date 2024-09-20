namespace advent_of_code_2023.Day07;
internal class Joker : IJStrategy
{
    public string GetJCardStrengths() => "AKQT98765432J";

    public IEnumerable<int> GetGroupings(string cards)
    {
        var jokers = cards.Count(x => x == 'J');

        var nonJokersGrouped = cards
            .Where(card => card != 'J')
            .GroupBy(card => card);

        if (nonJokersGrouped.Any())
        {
            var max = nonJokersGrouped.Max(group => group.Count());

            var largestGroup = nonJokersGrouped
                .First(group => group.Count() == max);

            var adjustedGroups = nonJokersGrouped
                .Select(group =>
                    group.Count() + (group.Key == largestGroup.Key ? jokers : 0));

            return adjustedGroups;
        }

        else // All Jokers
        {
            return new [] { cards.Length };
        }
    }
}
