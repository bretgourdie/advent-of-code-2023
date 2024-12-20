using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2023.Day18;
internal class Day18 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var instructions = parseInstructions(input);

        var trench = digTrench(instructions);

        var moat = digLagoon(trench);

        return lagoonSize(moat);
    }

    private IList<Instruction> parseInstructions(IList<string> input) =>
        input.Select(x => new Instruction(x)).ToList();

    private IList<Hole> digTrench(IList<Instruction> instructions)
    {
        var holes = new List<Hole>();
        var position = Vector2.Zero;

        holes.Add(new Hole(position, "Blank"));

        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Holes; i++)
            {
                position += instruction.TheDirection;
                holes.Add(new Hole(position, instruction.Color));
            }
        }

        return holes;
    }

    private Hole[][] digLagoon(IList<Hole> moat)
    {
        var lagoon = create2DFromSparse(moat);

        floodFill(lagoon);

        return lagoon;
    }

    private void floodFill(Hole[][] lagoon)
    {
        var first = findFirst(lagoon) + Vector2.One;

        print(lagoon);

        Console.WriteLine("-------------");

        var stack = new Stack<Vector2>();
        stack.Push(first);

        while (stack.Any())
        {
            var popped = stack.Pop();
            var x = (int)popped.X;
            var y = (int)popped.Y;

            if (0 > y || y >= lagoon.Length) continue;
            if (0 > x || x >= lagoon[y].Length) continue;
            if (!string.IsNullOrEmpty(lagoon[y][x].Color)) continue;


            lagoon[y][x] = new Hole(x, y, "Blank");
            stack.Push(new Vector2(x + 1, y));
            stack.Push(new Vector2(x - 1, y));
            stack.Push(new Vector2(x, y + 1));
            stack.Push(new Vector2(x, y - 1));
        }

        print(lagoon);
    }

    private void print(Hole[][] lagoon)
    {
        for (int y = 0; y < lagoon.Length; y++)
        {
            for (int x = 0; x < lagoon[y].Length; x++)
            {
                Console.Write(string.IsNullOrEmpty(lagoon[y][x].Color) ? "." : "#");
            }

            Console.WriteLine();
        }
    }

    private Vector2 findFirst(Hole[][] lagoon)
    {
        for (int y = 0; y < lagoon.Length; y++)
        {
            for (int x = 0; x < lagoon[y].Length; x++)
            {
                if (!string.IsNullOrEmpty(lagoon[y][x].Color))
                {
                    return new Vector2(x, y);
                }
            }
        }

        throw new ArgumentException("Couldn't find first hole", nameof(lagoon));
    }

    private void floodFill(
        Hole[][] lagoon,
        int x,
        int y)
    {
        if (0 > y || y >= lagoon.Length) return;
        if (0 > x || x >= lagoon[y].Length) return;
        if (!string.IsNullOrEmpty(lagoon[y][x].Color)) return;

        lagoon[y][x] = new Hole(x, y, "Blank");

        floodFill(lagoon, x - 1, y);
        floodFill(lagoon, x + 1, y);
        floodFill(lagoon, x, y - 1);
        floodFill(lagoon, x, y + 1);
    }

    private Hole[][] create2DFromSparse(IList<Hole> moat)
    {
        var widthMax = moat.Max(x => x.X);
        var widthMin = moat.Min(x => x.X);
        var width = widthMax - widthMin + 1;
        var widthOffset = Math.Min(0, widthMin);

        var heightMax = moat.Max(x => x.Y);
        var heightMin = moat.Min(x => x.Y);
        var height = heightMax - heightMin + 1;
        var heightOffset = Math.Min(0, heightMin);

        var trench2D = new Hole[height - heightOffset][];
        for (int ii = 0; ii < trench2D.Length; ii++)
        {
            trench2D[ii] = new Hole[width - widthOffset];
        }

        foreach (var hole in moat)
        {
            trench2D[hole.Y - heightOffset][hole.X - widthOffset] = hole;
        }

        return trench2D;
    }

    private long lagoonSize(Hole[][] lagoon)
    {
        long count = 0;

        for (int y = 0; y < lagoon.Length; y++)
        {
            for (int x = 0; x < lagoon[y].Length; x++)
            {
                if (!string.IsNullOrEmpty(lagoon[y][x].Color))
                {
                    count += 1;
                }
            }
        }

        return count;
    }

    protected override long part1ExampleExpected => 62;
    protected override long part1InputExpected => 52231;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected => 952408144115;
    protected override long part2InputExpected => -1;
}
