namespace advent_of_code_2023.Day08;
internal class Ghost : INavigationStrategy
{
    public IList<string> GetStart(IDictionary<string, IList<string>> map) =>
        map.Keys.Where(x => x.EndsWith("A"))
            .ToList();

    public bool IsDone(IList<string> currentLocations) =>
        currentLocations.All(x => x.EndsWith("Z"));
}
