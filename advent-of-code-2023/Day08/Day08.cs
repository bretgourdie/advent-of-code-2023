using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advent_of_code_2017;

namespace advent_of_code_2023.Day08;
internal class Day08 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var directions = input.First();

        var map = getMap(input);

        var location = "AAA";
        var destination = "ZZZ";
        long steps = 0;

        while (location != destination)
        {
            var currentDirection = (int)(steps % directions.Length);
            var direction = directions[currentDirection];
            steps += 1;

            var currentMap = map[location];

            if (direction == 'L')
            {
                location = currentMap[0];
            }

            else if (direction == 'R')
            {
                location = currentMap[1];
            }
        }

        return steps;
    }

    private IDictionary<string, IList<string>> getMap(string[] input)
    {
        var map = new Dictionary<string, IList<string>>();

        for (int ii = 2; ii < input.Length; ii++)
        {
            var line = input[ii];

            var keyAndValues = line.Split(" = ");
            var values = keyAndValues[1]
                .Replace("(", "")
                .Replace(")", "")
                .Split(", ");

            map[keyAndValues[0]] = values;
        }

        return map;
    }

    protected override long part1ExampleExpected => 6;
    protected override long part1InputExpected => 15989;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
