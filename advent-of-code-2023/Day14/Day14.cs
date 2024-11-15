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

        for (int y = start(input, direction); shouldContinue(y, end(input, direction), direction); y = increment(y, direction))
        {
            for (int x = start(input[y], direction); shouldContinue(x, end(input[y], direction), direction); x = increment(x, direction))
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

    private int start<T>(IList<T> list, Vector2 direction)
    {
        if (iterateForwards(direction)) return 0;

        return list.Count - 1;
    }

    private int end<T>(IList<T> list, Vector2 direction)
    {
        if (iterateForwards(direction)) return list.Count;

        return 0;
    }

    private bool shouldContinue(int current, int bound, Vector2 direction)
    {
        if (iterateForwards(direction)) return current < bound;

        return current >= bound;
    }

    private int increment(int current, Vector2 direction)
    {
        if (iterateForwards(direction)) return current + 1;

        return current - 1;
    }

    private bool iterateForwards(Vector2 direction)
    {
        return direction == north || direction == west;
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
        const long totalIterations = 1000000000;
        var array = input.To2DChar();

        long i = 0;
        var cycleDetector = new CycleDetector();

        for (; i < totalIterations && !cycleDetector.CollisionDetected; i++)
        {
            rollRocks(array, north);
            rollRocks(array, west);
            rollRocks(array, south);
            rollRocks(array, east);

            cycleDetector.SaveState(array.ToStringRepresentation(), i);
        }

        i = cycleDetector.GetLastIteration(i, totalIterations);

        for (; i < totalIterations; i++)
        {
            rollRocks(array, north);
            rollRocks(array, west);
            rollRocks(array, south);
            rollRocks(array, east);
        }

        return calculateLoad(array);
    }

    protected override long part2ExampleExpected => 64;
    protected override long part2InputExpected => 91286;
}
