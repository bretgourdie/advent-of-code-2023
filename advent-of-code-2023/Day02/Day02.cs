using advent_of_code_2017;

namespace advent_of_code_2023.Day02;
internal class Day02 : AdventSolution
{

    protected override long part1Work(string[] input) =>
        work(input, x => new PossibleGame(x));

    private long work(
        string[] input,
        Func<long, ICubeBag> gameStrategy)
    {
        long sum = 0;

        foreach (var line in input)
        {
            var gameAndCubesSplit = line.Split(": ");

            var gameAndId = gameAndCubesSplit[0].Split(" ");

            var subsetGameSplit = gameAndCubesSplit[1].Split("; ");

            var strategy = gameStrategy(long.Parse(gameAndId[1]));

            foreach (var subsetGame in subsetGameSplit)
            {
                var cubesSplit = subsetGame.Split(", ");

                foreach (var cubes in cubesSplit)
                {
                    var numberAndColorSplit = cubes.Split(" ");

                    var number = numberAndColorSplit[0];
                    var color = numberAndColorSplit[1];

                    strategy.HandleCubes(color, int.Parse(number));
                }
            }

            sum += strategy.GetAnswer();
        }

        return sum;
    }

    protected override long part1ExampleExpected => 8;
    protected override long part1InputExpected => 2348;

    protected override long part2Work(string[] input) =>
        work(input, x => new MinimumPowerSet());

    protected override long part2ExampleExpected => 2286;
    protected override long part2InputExpected => 76008;
}
