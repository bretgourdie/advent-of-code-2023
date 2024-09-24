﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advent_of_code_2017;

namespace advent_of_code_2023.Day09;
internal class Day09 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var readings = parseInput(input);

        long sum = 0;

        foreach (var reading in readings)
        {
            var nextValue = takeDerivative(
                reading,
                new List<IList<long>>(),
                0);
            sum += nextValue;
        }

        return sum;
    }

    private long takeDerivative(
        IList<long> reading,
        IList<IList<long>> derivations,
        int level)
    {
        derivations.Add(reading);

        if (reading.All(x => x == 0)) return 0;

        var rateOfChange = new List<long>();

        for (int ii = 0; ii < reading.Count - 1; ii++)
        {
            var change = reading[ii + 1] - reading[ii];
            rateOfChange.Add(change);
        }

        var fromLower = takeDerivative(rateOfChange, derivations, level + 1);

        var next = fromLower + derivations[level].Last();

        derivations[level].Add(next);

        return next;
    }

    private IList<IList<long>> parseInput(string[] input)
    {
        var readings = new List<IList<long>>();

        foreach (var line in input)
        {
            var numbersStr = line.Split(" ");
            var numbers = numbersStr.Select(x => long.Parse(x)).ToList();
            readings.Add(numbers);
        }

        return readings;
    }

    protected override long part1ExampleExpected => 114;
    protected override long part1InputExpected => 1884768153;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
