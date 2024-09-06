using advent_of_code_2017;
using System.Numerics;

namespace advent_of_code_2023.Day03;
internal class Day03 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input,
            findAllSymbols,
            getSumOfAllSymbols);

    private long work(
        string[] input,
        Func<char, bool> isLookingForSymbol,
        Func<ISet<WholeNumber>, long> getSum)
    {
        var symbolCoordinates = findSymbols(input, isLookingForSymbol);

        long sum = 0;

        foreach (var symbolCoordinate in symbolCoordinates)
        {
            sum += getSymbolSum(
                input,
                symbolCoordinate.X,
                symbolCoordinate.Y,
                getSum);
        }

        return sum;
    }

    protected override long part1ExampleExpected => 4361;
    protected override long part1InputExpected => 528819;

    protected override long part2Work(string[] input) =>
        work(input,
            findStars,
            getGearRatio);

    protected override long part2ExampleExpected => 467835;
    protected override long part2InputExpected => 80403602;

    private IList<Vector2> findSymbols(
        string[] input,
        Func<char, bool> isLookingForSymbol)
    {
        var coords = new List<Vector2>();

        for (int ii = 0; ii < input.Length; ii++)
        {
            for (int jj = 0; jj < input[ii].Length; jj++)
            {
                var letter = input[ii][jj];

                if (!isNumber(letter)
                    && letter != '.'
                    && isLookingForSymbol(letter))
                {
                    coords.Add(new Vector2(jj, ii));
                }
            }
        }

        return coords;
    }

    private long getGearRatio(
        ISet<WholeNumber> numbers)
    {
        if (numbers.Count == 2)
            return numbers.First().Number * numbers.Skip(1).First().Number;

        return 0;
    }

    private bool findStars(char letter) => letter == '*';

    private bool findAllSymbols(char letter) => true;

    private long getSumOfAllSymbols(
        ISet<WholeNumber> numbers) =>
        numbers.Sum(x => x.Number);

    private long getSymbolSum(
        string[] input,
        float startXf,
        float startYf,
        Func<ISet<WholeNumber>, long> getSum)
    {
        ISet<WholeNumber> numbers = new HashSet<WholeNumber>();

        int startX = (int)startXf;
        int startY = (int)startYf;

        int minX = Math.Max(0, startX - 1);
        int minY = Math.Max(0, startY - 1);
        int maxX = Math.Min(input[startY].Length, startX + 1 + 1);
        int maxY = Math.Min(input.Length, startY + 1 + 1);

        for (int y = minY; y < maxY; y++)
        {
            for (int x = minX; x < maxX; x++)
            {
                var letter = input[y][x];

                if (isNumber(letter))
                {
                    var number = findWholeNumber(input, x, y);

                    if (!numbers.Contains(number))
                    {
                        numbers.Add(number);
                    }
                }
            }
        }

        return getSum(numbers);
    }

    private WholeNumber findWholeNumber(string[] input, int x, int y)
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

        return new WholeNumber(
            start,
            y,
            long.Parse(wholeNumber)
        );
    }

    private bool isNumber(char letter) =>
        int.TryParse(letter.ToString(), out _);

    private struct WholeNumber
    {
        public readonly Vector2 Coordinate;
        public readonly long Number;

        public WholeNumber(
            int x,
            int y,
            long number)
        {
            Coordinate = new Vector2(x, y);
            Number = number;
        }
    }
}
