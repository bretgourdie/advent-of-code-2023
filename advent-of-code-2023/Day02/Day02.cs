using System.Runtime.CompilerServices;
using advent_of_code_2017;

namespace advent_of_code_2023.Day02;
internal class Day02 : AdventSolution
{
    private IDictionary<string, int> colorToMaxCubes = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    protected override long part1Work(string[] input) =>
        work(input, returnPossibleGameId);

    private long work(
        string[] input,
        Func<IList<string>, long, long> sumFunction)
    {
        long sum = 0;

        foreach (var line in input)
        {
            var gameAndCubesSplit = line.Split(": ");

            var gameAndId = gameAndCubesSplit[0].Split(" ");

            var subsetGameSplit = gameAndCubesSplit[1].Split("; ");

            sum += sumFunction(subsetGameSplit, long.Parse(gameAndId[1]));
        }

        return sum;
    }

    private long returnPossibleGameId(IList<string> subsetGames, long gameId)
    {
        if (isPossibleGame(subsetGames))
        {
            return gameId;
        }

        return 0;
    }

    private bool isPossibleGame(IList<string> subsetGames)
    {
        foreach (var subsetGame in subsetGames)
        {
            var cubesSplit = subsetGame.Split(", ");

            foreach (var cubes in cubesSplit)
            {
                var numberAndColorSplit = cubes.Split(" ");

                var number = numberAndColorSplit[0];
                var color = numberAndColorSplit[1];

                if (colorToMaxCubes[color] < int.Parse(number))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private long returnMinimumPower(IList<string> subsetGames, long gameId)
    {
        IDictionary<string, int> colorToMinimum = new Dictionary<string, int>();

        foreach (var subsetGame in subsetGames)
        {
            var cubesSplit = subsetGame.Split(", ");

            foreach (var cubes in cubesSplit)
            {
                var numberAndColorSplit = cubes.Split(" ");

                var number = int.Parse(numberAndColorSplit[0]);
                var color = numberAndColorSplit[1];

                if (!colorToMinimum.ContainsKey(color))
                {
                    colorToMinimum[color] = number;
                }

                else
                {
                    colorToMinimum[color] = Math.Max(colorToMinimum[color], number);
                }
            }
        }

        long power = colorToMinimum.Aggregate(1, (x, y) => x * y.Value);
        return power;
    }

    protected override long part1ExampleExpected => 8;
    protected override long part1InputExpected => 2348;

    protected override long part2Work(string[] input) =>
        work(input, returnMinimumPower);

    protected override long part2ExampleExpected => 2286;
    protected override long part2InputExpected => 76008;
}
