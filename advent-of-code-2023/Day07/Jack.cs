namespace advent_of_code_2023.Day07;
internal class Jack : IJStrategy
{
    public string GetJCardStrengths() => "AKQJT98765432";

    public IEnumerable<int> GetGroupings(string cards) =>
        cards
            .GroupBy(card => card)
            .Select(group => group.Count());
}
