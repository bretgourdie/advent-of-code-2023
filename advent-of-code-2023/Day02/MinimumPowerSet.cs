namespace advent_of_code_2023.Day02;
internal class MinimumPowerSet : ICubeBag
{
    private readonly IDictionary<string, int> colorToMaximumCubes;

    public MinimumPowerSet()
    {
        colorToMaximumCubes = new Dictionary<string, int>();
    }

    public void HandleCubes(string color, int count)
    {
        int currentMax;
        if (!colorToMaximumCubes.ContainsKey(color))
        {
            currentMax = count;
        }

        else
        {
            currentMax = colorToMaximumCubes[color];
        }

        colorToMaximumCubes[color] = Math.Max(
            currentMax,
            count);
    }

    public long GetAnswer() =>
        colorToMaximumCubes.Aggregate(
            1,
            (x, y) => x * y.Value);
}
