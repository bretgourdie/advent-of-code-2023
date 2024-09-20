namespace advent_of_code_2023.Day07;
internal interface IJStrategy
{
    string GetJCardStrengths();
    public IEnumerable<int> GetGroupings(string cards);
}
