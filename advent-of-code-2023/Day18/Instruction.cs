using System.Numerics;

namespace advent_of_code_2023.Day18;
internal readonly struct Instruction
{
    public readonly Vector2 TheDirection;
    public readonly int Holes;
    public readonly string Color;

    public Instruction(string line)
    {
        var split = line.Split(' ');

        TheDirection = parseDirection(split[0]);
        Holes = int.Parse(split[1]);
        Color = parseColor(split[2]);
    }

    private Vector2 parseDirection(string letter)
    {
        if (letter == "R") return Direction.East;
        if (letter == "D") return Direction.South;
        if (letter == "U") return Direction.North;
        if (letter == "L") return Direction.West;

        throw new ArgumentException("Cannot determine direction", nameof(letter));
    }

    private string parseColor(string messyColor) =>
        messyColor.Replace("(", String.Empty).Replace(")", String.Empty);
}
