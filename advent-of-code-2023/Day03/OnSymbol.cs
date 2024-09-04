namespace advent_of_code_2023.Day03;
internal class OnSymbol : ISchematicState
{
    private long sum;

    public OnSymbol(long sum)
    {
        this.sum = sum;
    }

    public ISchematicState Handle(string[] schematic, int x, int y)
    {
        var letter = schematic[y][x];

        if (int.TryParse(letter.ToString(), out int number))
        {
            return new OnNumber(sum).Handle(schematic, x, y);
        }

        return this;
    }

    public long GetSum() => sum;
}
