namespace advent_of_code_2023.Day13;
internal class Grid
{
    public readonly string[] TheGrid;

    private Smudge smudge;

    public Grid(string[] grid)
    {
        TheGrid = grid;
        smudge = new Smudge();
    }

    public void SmudgeGrid()
    {
        smudge.Handle(TheGrid);
    }

    public long FindSymmetry(
        Smudging smudging)
    {
        while (true)
        {
            var result = findSymmetry(TheGrid, smudging);

            if (result.Line == Result.SymmetryType.Horizontal)
            {
                return getHorizontalLineScore(result.LeftoverLines);
            }

            else if (result.Line == Result.SymmetryType.Vertical)
            {
                return getVerticalLineScore(result.LeftoverLines);
            }

        }
    }

    private Result findSymmetry(
        string[] grid,
        Smudging smudgingStrategy)
    {
        do
        {
            if (smudgingStrategy == Smudging.Yes)
            {
                smudge.Handle(TheGrid);
            }

            var rows = checkHorizontal(grid, smudgingStrategy, smudge.X, smudge.Y);
            if (rows > 0)
            {
                return new Result(Result.SymmetryType.Horizontal, rows);
            }

            var columns = checkVertical(grid, smudgingStrategy, smudge.X, smudge.Y);
            if (columns > 0)
            {
                return new Result(Result.SymmetryType.Vertical, columns);
            }

            if (smudgingStrategy == Smudging.Yes)
            {
                smudge.Handle(TheGrid);
            }
        } while (smudgingStrategy == Smudging.Yes);

        throw new NotImplementedException("Could not find symmetry");
    }

    private int checkHorizontal(
        string[] grid,
        Smudging smudgingStrategy,
        int smudgeX,
        int smudgeY)
    {
        var rows = grid.Length;
        var columns = grid.First().Length;

        for (int southOfLine = 1; southOfLine < rows; southOfLine++)
        {
            bool symmetry = true;
            bool includesSmudge = false;

            for (int offset = 0; symmetry && southOfLine - offset - 1 >= 0 && southOfLine + offset < rows; offset++)
            {
                var northLineIndex = southOfLine - offset - 1;
                var southLineIndex = southOfLine + offset;
                var northLine = grid[northLineIndex];
                var southLine = grid[southLineIndex];

                for (int letterIndex = 0; symmetry && letterIndex < columns; letterIndex++)
                {
                    symmetry &= northLine[letterIndex] == southLine[letterIndex];
                    includesSmudge |= (smudgeY == northLineIndex || smudgeY == southLineIndex) && (smudgeX == letterIndex);
                }
            }

            if (symmetry)
            {
                if (smudgingStrategy == Smudging.No
                    || (smudgingStrategy == Smudging.Yes && includesSmudge))
                {
                    return southOfLine;
                }
            }
        }

        return 0;
    }

    private int checkVertical(
        string[] grid,
        Smudging smudgingStrategy,
        int smudgeX,
        int smudgeY)
    {
        var rows = grid.Length;
        var columns = grid.First().Length;

        for (int eastOfLine = 1; eastOfLine < columns; eastOfLine++)
        {
            bool symmetry = true;
            bool includesSmudge = false;

            for (int offset = 0; eastOfLine - offset - 1 >= 0 && eastOfLine + offset < columns; offset++)
            {
                for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                {
                    var westLetterIndex = eastOfLine - offset - 1;
                    var eastLetterIndex = eastOfLine + offset;
                    var westLetter = grid[rowIndex][westLetterIndex];
                    var eastLetter = grid[rowIndex][eastLetterIndex];

                    symmetry &= westLetter == eastLetter;
                    includesSmudge |= (smudgeY == rowIndex) && (smudgeX == westLetterIndex || smudgeX == eastLetterIndex);
                }
            }

            if (symmetry)
            {
                if (smudgingStrategy == Smudging.No
                    || (smudgingStrategy == Smudging.Yes && includesSmudge))
                {
                    return eastOfLine;
                }
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

    private class Smudge
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private bool isSmudged;

        public void Handle(string[] grid)
        {
            var existing = grid[Y][X];
            var smudged = existing == '.' ? "#" : ".";

            var firstPart = grid[Y].Substring(0, X);
            var secondPart = X + 1 < grid[Y].Length ? grid[Y].Substring(X + 1) : String.Empty;
            var newLine = firstPart + smudged + secondPart;
            grid[Y] = newLine;

            if (isSmudged)
            {
                if (X + 1 == grid[Y].Length)
                {
                    X = 0;
                    Y += 1;
                }

                else
                {
                    X += 1;
                }
            }

            isSmudged = !isSmudged;
        }
    }

    public enum Smudging
    {
        Yes,
        No
    }
}
