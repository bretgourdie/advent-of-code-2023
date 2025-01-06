using System.Numerics;

namespace advent_of_code_2023.Day18;
internal readonly struct Instruction
{
    public readonly Vector2 TheDirection;
    public readonly int Holes;
    public readonly string Color;

    public Instruction(
        string line,
        Plan plan)
    {
        var split = line.Split(' ');

        TheDirection = parseDirection(split, plan);
        Holes = parseHoles(split, plan);
        Color = parseColor(split, plan);
    }

    private Vector2 parseDirection(
        IList<string> lineSplit,
        Plan plan)
    {
        if (plan == Plan.UseInstruction)
        {
            return parseDirectionInstruction(lineSplit[0]);
        }

        else
        {
            return parseDirectionColor(parseColorInstruction(lineSplit[2]));
        }
    }

    private string parseColor(
        IList<string> lineSplit,
        Plan plan)
    {
        if (plan == Plan.UseInstruction)
        {
            return parseColorInstruction(lineSplit[2]);
        }

        else
        {
            return String.Empty;
        }
    }

    private int parseHoles(
        IList<string> lineSplit,
        Plan plan)
    {
        if (plan == Plan.UseInstruction)
        {
            return int.Parse(lineSplit[1]);
        }

        else
        {
            var color = parseColorInstruction(lineSplit[2]);
            return getHexValue(color);
        }
    }

    private int getHexValue(string wholeColorWord)
    {
        var holePortion = wholeColorWord.Substring(0, wholeColorWord.Length - 1);

        var hexFormat = holePortion.Replace("#", "0x");

        var value = Convert.ToInt32(hexFormat, 16);

        return value;
    }

    private Vector2 parseDirectionColor(string colorWord)
    {
        if (colorWord.EndsWith("0")) return Direction.East;
        if (colorWord.EndsWith("1")) return Direction.South;
        if (colorWord.EndsWith("2")) return Direction.West;
        if (colorWord.EndsWith("3")) return Direction.North;

        throw new ArgumentException("Cannot determine direction", nameof(colorWord));
    }

    private Vector2 parseDirectionInstruction(string letter)
    {
        if (letter == "R") return Direction.East;
        if (letter == "D") return Direction.South;
        if (letter == "U") return Direction.North;
        if (letter == "L") return Direction.West;

        throw new ArgumentException("Cannot determine direction", nameof(letter));
    }

    private string parseColorInstruction(string messyColor) =>
        messyColor.Replace("(", String.Empty).Replace(")", String.Empty);
}
