namespace advent_of_code_2023.Day08;
internal class Living : INavigationStrategy
{
    public IList<string> GetStart(IDictionary<string, IList<string>> map) =>
        new List<string>() { "AAA" };

    public bool IsDone(IList<string> currentLocations) =>
        currentLocations.All(x => x == "ZZZ");
}
