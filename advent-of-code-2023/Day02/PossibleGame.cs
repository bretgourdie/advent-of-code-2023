namespace advent_of_code_2023.Day02;
internal class PossibleGame : ICubeBag
{
    private bool isPossible = true;
    private readonly long gameId;

    public PossibleGame(long gameId)
    {
        this.gameId = gameId;
    }

    private IDictionary<string, int> colorToMaxCubes = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };

    public void HandleCubes(string color, int count)
    {
        if (colorToMaxCubes[color] < count)
        {
            isPossible = false;
        }
    }

    public long GetAnswer() =>
        isPossible ? gameId : 0;
}
