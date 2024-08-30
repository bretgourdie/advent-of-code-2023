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

    protected override long part1Work(string[] input)
    {
        long sum = 0;

        foreach (var line in input)
        {
            var gameAndCubesSplit = line.Split(": ");

            var gameAndId = gameAndCubesSplit[0].Split(" ");

            var subsetGameSplit = gameAndCubesSplit[1].Split("; ");

            if (isPossibleGame(subsetGameSplit))
            {
                sum += long.Parse(gameAndId[1]);
            }
        }

        return sum;
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

    protected override long part1ExampleExpected => 8;
    protected override long part1InputExpected => 2348;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
