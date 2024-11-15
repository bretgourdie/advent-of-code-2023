using System.Text;

namespace advent_of_code_2023;
internal static class Extensions
{
    public static char[][] To2DChar(this string[] input) =>
        input
            .Select(x => x.ToCharArray())
            .ToArray();

    public static string ToStringRepresentation(this char[][] input)
    {
        var sb = new StringBuilder();

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                sb.Append(input[y][x]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}
