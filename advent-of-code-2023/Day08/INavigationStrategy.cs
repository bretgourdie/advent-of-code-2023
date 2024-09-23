namespace advent_of_code_2023.Day08;
internal interface INavigationStrategy
{
    IList<string> GetStart(IDictionary<string, IList<string>> map);
}
