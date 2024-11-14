using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using advent_of_code_2017;

namespace advent_of_code_2023.Day14;
internal class Day14 : AdventSolution
{
    private const char rock = 'O';
    private const char space = '.';
    private const char cube = '#';

    private Vector2 north = new Vector2(0, -1);
    private Vector2 south = new Vector2(0, 1);
    private Vector2 east = new Vector2(1, 0);
    private Vector2 west = new Vector2(-1, 0);

    protected override long part1Work(string[] input)
    {
        return calculateLoad(rollRocks(input.To2DChar(), north));
    }

    private char[][] rollRocks(
        char[][] input,
        Vector2 direction)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                var icon = input[y][x];

                if (icon == rock)
                {
                    int rockYIndex = y;
                    int rockXIndex = x;

                    while (true)
                    {
                        var checkY = rockYIndex + (int)direction.Y;
                        var checkX = rockXIndex + (int)direction.X;

                        if (0 <= checkY && checkY < input.Length
                            && 0 <= checkX && checkX < input[checkY].Length)
                        {
                            var checkIcon = input[checkY][checkX];

                            if (checkIcon != space)
                            {
                                break;
                            }

                            rockYIndex = checkY;
                            rockXIndex = checkX;
                        }
                        else
                        {
                            break;
                        }
                    }

                    input[y][x] = space;
                    input[rockYIndex][rockXIndex] = rock;
                }
            }
        }

        return input;
    }

    private long calculateLoad(char[][] rolled)
    {
        long result = 0;

        for (int y = 0; y < rolled.Length; y++)
        {
            for (int x = 0; x < rolled[y].Length; x++)
            {
                var icon = rolled[y][x];

                if (icon == rock)
                {
                    var load = rolled.Length - y;
                    result += load;
                }
            }
        }

        return result;
    }

    protected override long part1ExampleExpected => 136;
    protected override long part1InputExpected => 105784;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
