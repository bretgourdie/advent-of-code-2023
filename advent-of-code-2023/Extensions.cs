namespace advent_of_code_2023;
internal static class Extensions
{
    public static char[][] To2DChar(this string[] input) =>
        input
            .Select(x => x.ToCharArray())
            .ToArray();
}
