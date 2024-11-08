namespace advent_of_code_2023.Day13;
internal class Grid
{
    public readonly string[] TheGrid;

    public Grid(string[] grid)
    {
        TheGrid = grid;
    }

    public long FindSymmetry()
    {
        var result = findSymmetry(TheGrid);

        if (result.Line == Result.SymmetryType.Horizontal)
        {
            return getHorizontalLineScore(result.LeftoverLines);
        }

        else if (result.Line == Result.SymmetryType.Vertical)
        {
            return getVerticalLineScore(result.LeftoverLines);
        }

        throw new NotImplementedException("No symmetry");
    }

    private Result findSymmetry(string[] grid)
    {
        var rows = checkHorizontal(grid);
        if (rows > 0)
        {
            return new Result(Result.SymmetryType.Horizontal, rows);
        }

        var columns = checkVertical(grid);
        if (columns > 0)
        {
            return new Result(Result.SymmetryType.Vertical, columns);
        }

        throw new NotImplementedException("Could not find symmetry");
    }

    private int checkHorizontal(string[] grid)
    {
        var rows = grid.Length;
        var columns = grid.First().Length;

        for (int southOfLine = 1; southOfLine < rows; southOfLine++)
        {
            bool symmetry = true;

            for (int offset = 0; southOfLine - offset - 1 >= 0 && southOfLine + offset < rows; offset++)
            {
                var northLine = grid[southOfLine - offset - 1];
                var southLine = grid[southOfLine + offset];

                for (int letterIndex = 0; letterIndex < columns; letterIndex++)
                {
                    symmetry &= northLine[letterIndex] == southLine[letterIndex];
                }
            }

            if (symmetry)
            {
                return southOfLine;
            }
        }

        return 0;
    }

    private int checkVertical(string[] grid)
    {
        var rows = grid.Length;
        var columns = grid.First().Length;

        for (int eastOfLine = 1; eastOfLine < columns; eastOfLine++)
        {
            bool symmetry = true;

            for (int offset = 0; eastOfLine - offset - 1 >= 0 && eastOfLine + offset < columns; offset++)
            {
                for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                {
                    var westLetter = grid[rowIndex][eastOfLine - offset - 1];
                    var eastLetter = grid[rowIndex][eastOfLine + offset];

                    symmetry &= westLetter == eastLetter;
                }
            }

            if (symmetry)
            {
                return eastOfLine;
            }
        }

        return 0;
    }

    private long getHorizontalLineScore(int rowsAbove) =>
        rowsAbove * 100;

    private long getVerticalLineScore(int columnsToTheLeft) =>
        columnsToTheLeft * 1;

    private struct Result
    {
        public enum SymmetryType
        {
            Horizontal,
            Vertical
        }

        public readonly SymmetryType Line;
        public int LeftoverLines;

        public Result(SymmetryType line, int leftoverLines)
        {
            Line = line;
            LeftoverLines = leftoverLines;
        }
    }
}
