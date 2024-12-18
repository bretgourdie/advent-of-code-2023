using System.Numerics;

namespace advent_of_code_2023.Day17;
internal class Day17 : AdventSolution
{
    protected override long part1Work(string[] input) =>
        work(input, new StepConstraint(0, 3));

    private long work(
        string[] input,
        StepConstraint constraint)
    {
        var cityBlock = input.To2DChar();

        var leastHeatLoss = findLeastHeatLoss(cityBlock, constraint);

        return leastHeatLoss;
    }

    private Vector2 getTarget(char[][] cityBlock)
    {
        return new Vector2(cityBlock[0].Length - 1, cityBlock.Length - 1);
    }

    private long findLeastHeatLoss(
        char[][] cityBlock,
        StepConstraint constraint)
    {
        IDictionary<State, long> stateAndCost = new Dictionary<State, long>();
        var toVisit = new PriorityQueue<State, long>();
        var targetStatesFound = new PriorityQueue<State, long>();

        var firstState = new State(Vector2.Zero, Direction.None, 0);
        stateAndCost[firstState] = 0;
        toVisit.Enqueue(firstState, 0);
        var target = getTarget(cityBlock);

        while (toVisit.Count > 0)
        {
            toVisit.TryDequeue(out State lowest, out long lowestCost);

            if (lowest.Position == target && checkFinalSteps(lowest, constraint))
            {
                targetStatesFound.Enqueue(lowest, lowestCost);
            }

            if (noBetter(targetStatesFound, toVisit, out long best))
            {
                return best;
            }

            foreach (var direction in Direction.Clockwise)
            {
                if (!shouldExplore(lowest, direction, cityBlock, constraint)) continue;

                var newCost = lowestCost + getCost(lowest, direction, cityBlock);
                var steps = getNextStep(lowest, direction);
                var newState = new State(getTestingPosition(lowest, direction), direction, steps);

                addIfBetterOrNotFound(
                    newState,
                    newCost,
                    stateAndCost,
                    toVisit);
            }
        }

        throw new ArgumentException("Could not find target", nameof(target));
    }

    private void addIfBetterOrNotFound(
        State newState,
        long newCost,
        IDictionary<State, long> stateAndCost,
        PriorityQueue<State, long> toVisit)
    {
        if (stateAndCost.TryGetValue(newState, out long existingCost))
        {
            if (newCost < existingCost)
            {
                addVisited(
                    newState,
                    newCost,
                    stateAndCost,
                    toVisit);
            }
        }

        else
        {
            addVisited(
                newState,
                newCost,
                stateAndCost,
                toVisit);
        }
    }

    private void addVisited(
        State newState,
        long newCost,
        IDictionary<State, long> stateAndCost,
        PriorityQueue<State, long> toVisit)
    {
        stateAndCost[newState] = newCost;
        toVisit.Enqueue(newState, newCost);
    }

    private bool shouldExplore(
        State lowest,
        Vector2 direction,
        char[][] cityBlock,
        StepConstraint constraint)
    {
        return !backwards(lowest, direction)
               && inBounds(lowest, direction, cityBlock)
               && acceptableNumberOfSteps(lowest, direction, constraint);
    }

    private bool noBetter(
        PriorityQueue<State, long> targetStates,
        PriorityQueue<State, long> toVisit,
        out long best)
    {
        if (targetStates.TryPeek(out _, out long bestTargetCost)
            && toVisit.TryPeek(out _, out long bestToVisitCost))
        {
            best = bestTargetCost;
            return bestTargetCost < bestToVisitCost;
        }

        best = default;
        return false;
    }

    private int getNextStep(
        State state,
        Vector2 direction)
    {
        if (state.Direction == direction)
        {
            return state.StepsInDirection + 1;
        }

        return 1;
    }

    private Vector2 getTestingPosition(
        State state,
        Vector2 offset)
    {
        int x = (int)state.Position.X + (int)offset.X;
        int y = (int)state.Position.Y + (int)offset.Y;

        return new Vector2(x, y);
    }

    private bool backwards(
        State state,
        Vector2 direction)
    {
        return state.Direction == direction.Opposite();
    }

    private bool checkFinalSteps(
        State state,
        StepConstraint constraint)
    {
        return constraint.Min <= state.StepsInDirection
            && state.StepsInDirection < constraint.Max;
    }

    private bool acceptableNumberOfSteps(
        State state,
        Vector2 direction,
        StepConstraint constraint)
    {
        if (state.Direction == Direction.None) return true;

        if (state.Direction == direction)
        {
            return state.StepsInDirection < constraint.Max;
        }

        else
        {
            return constraint.Min <= state.StepsInDirection;
        }
    }

    private long getCost(
        State state,
        Vector2 direction,
        char[][] cityBlock)
    {
        var testing = getTestingPosition(state, direction);
        var number = cityBlock[(int)testing.Y][(int)testing.X];
        return long.Parse(number.ToString());
    }

    private bool inBounds(
        State state,
        Vector2 direction,
        char[][] cityBlock)
    {
        var testing = getTestingPosition(state, direction);
        int x = (int)testing.X;
        int y = (int)testing.Y;

        return
            0 <= y && y < cityBlock.Length &&
            0 <= x && x < cityBlock[y].Length;
    }

    private struct StepConstraint
    {
        public readonly int Min;
        public readonly int Max;

        public StepConstraint(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }

    protected override long part1ExampleExpected => 102;
    protected override long part1InputExpected => 907;

    protected override long part2Work(string[] input) =>
        work(input, new StepConstraint(4, 10));

    protected override long part2ExampleExpected => 94;
    protected override long part2InputExpected => 1057;
}
