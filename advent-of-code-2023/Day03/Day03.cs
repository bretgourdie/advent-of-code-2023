using advent_of_code_2017;
using System.Numerics;
using System.Text;

namespace advent_of_code_2023.Day03;
internal class Day03 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var symbolCoordinates = findSymbols(input);

        long sum = 0;

        foreach (var symbolCoordinate in symbolCoordinates)
        {
            sum += getSum(input, symbolCoordinate.X, symbolCoordinate.Y);
        }

        return sum;
    }

    protected override long part1ExampleExpected => 4361;
    protected override long part1InputExpected => 528819;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }

    private IList<Vector2> findSymbols(string[] input)
    {
        var coords = new List<Vector2>();

        for (int ii = 0; ii < input.Length; ii++)
        {
            for (int jj = 0; jj < input[ii].Length; jj++)
            {
                var letter = input[ii][jj];

                if (!int.TryParse(letter.ToString(), out int number)
                    && letter != '.')
                {
                    coords.Add(new Vector2(jj, ii));
                }
            }
        }

        return coords;
    }

    private long getSum(string[] input, float startXf, float startYf)
    {
        IDictionary<Vector2, long> coordToNumber = 
            new Dictionary<Vector2, long>();

        int startX = (int)startXf;
        int startY = (int)startYf;

        int minX = Math.Max(0, startX - 1);
        int minY = Math.Max(0, startY - 1);
        int maxX = Math.Min(input[startY].Length, startX + 1 + 1);
        int maxY = Math.Min(input.Length, startY + 1 + 1);

        ISet<Vector2> usedCoords = new HashSet<Vector2>();

        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                var letter = input[y][x];

                if (int.TryParse(letter.ToString(), out int number))
                {
                    var wholeNumber = findNumber(input, x, y);
                    var start = new Vector2(wholeNumber.StartX, wholeNumber.StartY);

                    if (!coordToNumber.ContainsKey(start))
                    {
                        coordToNumber[start] = wholeNumber.Number;
                    }
                }
            }
        }

        return coordToNumber.Sum(x => x.Value);
    }

    private ExtractedNumber findNumber(string[] input, int x, int y)
    {
        int start = x;
        while (start - 1 >= 0 && isNumber(input[y][start - 1]))
        {
            start -= 1;
        }

        int end = x + 1;
        while (end < input[y].Length && isNumber(input[y][end]))
        {
            end += 1;
        }

        var wholeNumber = input[y].Substring(start, end - start);

        return new ExtractedNumber(
            start,
            y,
            long.Parse(wholeNumber)
        );
    }

    private bool isNumber(char letter) =>
        int.TryParse(letter.ToString(), out int number);

    private struct ExtractedNumber
    {
        public readonly int StartX;
        public readonly int StartY;
        public readonly long Number;

        public ExtractedNumber(
            int x,
            int y,
            long number)
        {
            StartX = x;
            StartY = y;
            Number = number;
        }
    }
}
