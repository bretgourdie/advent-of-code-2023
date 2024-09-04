using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2023.Day03;
internal class OnNumber : ISchematicState
{
    private bool isPartNumber;
    private long sum;
    private StringBuilder numberBuilder;

    public OnNumber(long sum)
    {
        this.sum = sum;
        numberBuilder = new StringBuilder();
    }

    public ISchematicState Handle(string[] schematic, int x, int y)
    {
        var letter = schematic[y][x];

        if (!isNumber(letter))
        {
            return new OnSymbol(GetSum()).Handle(schematic, x, y);
        }

        numberBuilder.Append(letter);

        var iiBound = Math.Min(schematic.Length, y + 1 + 1);
        var jjBound = Math.Min(schematic[y].Length, x + 1 + 1);

        for (int ii = Math.Max(0, y - 1); ii < iiBound && !isPartNumber; ii++)
        {
            for (int jj = Math.Max(0, x - 1); jj < jjBound && !isPartNumber; jj++)
            {
                var neighbor = schematic[ii][jj];

                isPartNumber |= (!isNumber(neighbor) && neighbor != '.');
            }
        }

        return this;
    }

    public long GetSum()
    {
        if (isPartNumber)
        {
            return sum + long.Parse(numberBuilder.ToString());
        }

        return sum;
    }

    private bool isNumber(char letter) => int.TryParse(letter.ToString(), out int result);
}
