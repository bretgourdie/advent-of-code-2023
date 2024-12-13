using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace advent_of_code_2023.Day17;
internal class Day17 : AdventSolution
{
    protected override long part1Work(string[] input)
    {
        var cityBlock = input.To2DChar();

        var leastHeatLoss = findLeastHeatLoss(cityBlock);

        return leastHeatLoss;
    }

    private Vector2 getTarget(char[][] cityBlock)
    {
        return new Vector2(cityBlock[0].Length - 1, cityBlock.Length - 1);
    }

    private long findLeastHeatLoss(char[][] cityBlock)
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
            if (noBetter(targetStatesFound, toVisit, out long best))
            {
                return best;
            }

            toVisit.TryDequeue(out State lowest, out long lowestCost);

            foreach (var direction in Direction.Clockwise)
            {
                if (!shouldExplore(lowest, direction, cityBlock)) continue;

                var newCost = lowestCost + getCost(lowest, direction, cityBlock);
                var steps = getNextStep(lowest, direction);
                var newState = new State(getTestingPosition(lowest, direction), direction, steps);

                addIfBetterOrNotFound(
                    newState,
                    newCost,
                    stateAndCost,
                    toVisit,
                    target,
                    targetStatesFound);
            }
        }

        throw new ArgumentException("Could not find target", nameof(target));
    }

    private void addIfBetterOrNotFound(
        State newState,
        long newCost,
        IDictionary<State, long> stateAndCost,
        PriorityQueue<State, long> toVisit,
        Vector2 target,
        PriorityQueue<State, long> targetStates)
    {
        if (stateAndCost.TryGetValue(newState, out long existingCost))
        {
            if (newCost < existingCost)
            {
                addVisited(
                    newState,
                    newCost,
                    stateAndCost,
                    toVisit,
                    target,
                    targetStates);
            }
        }

        else
        {
            addVisited(
                newState,
                newCost,
                stateAndCost,
                toVisit,
                target,
                targetStates);
        }
    }

    private void addVisited(
        State newState,
        long newCost,
        IDictionary<State, long> stateAndCost,
        PriorityQueue<State, long> toVisit,
        Vector2 target,
        PriorityQueue<State, long> targetStates)
    {
        stateAndCost[newState] = newCost;
        toVisit.Enqueue(newState, newCost);

        if (newState.Position == target)
        {
            targetStates.Enqueue(newState, newCost);
        }
    }

    private bool shouldExplore(
        State lowest,
        Vector2 direction,
        char[][] cityBlock)
    {
        return !backwards(lowest, direction)
               && inBounds(lowest, direction, cityBlock)
               && threeOrLessBlocks(lowest, direction);
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

    private bool threeOrLessBlocks(
        State state,
        Vector2 direction)
    {
        if (state.Direction == direction)
        {
            return state.StepsInDirection < 3;
        }

        return true;
    }

    private void printPath(
        State target,
        IDictionary<State, State> stateToParent,
        char[][] cityBlock)
    {
        var s = new Dictionary<Vector2, string>();
        var u = stateToParent[target];

        while (stateToParent.ContainsKey(u))
        {
            var direction = ".";
            var prev = stateToParent[u];
            if (prev.Position + Direction.North == u.Position)
            {
                direction = "^";
            }
            else if (prev.Position + Direction.South == u.Position)
            {
                direction = "V";
            }
            else if (prev.Position + Direction.East == u.Position)
            {
                direction = ">";
            }
            else if (prev.Position + Direction.West == u.Position)
            {
                direction = "<";
            }

            s.Add(u.Position, direction);
            u = stateToParent[u];
        }

        for (int y = 0; y < cityBlock.Length; y++)
        {
            for (int x = 0; x < cityBlock[y].Length; x++)
            {
                Console.Write(cityBlock[y][x].ToString());
            }

            Console.WriteLine();
        }

        Console.WriteLine("---------------");

        for (int y = 0; y < cityBlock.Length; y++)
        {
            for (int x = 0; x < cityBlock[y].Length; x++)
            {
                var position = new Vector2(x, y);
                if (s.TryGetValue(position, out string direction))
                {
                    Console.Write(direction);
                }

                else
                {
                    Console.Write(cityBlock[y][x].ToString());
                }
            }

            Console.WriteLine();
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

    protected override long part1ExampleExpected => 102;
    protected override long part1InputExpected => 907;
    protected override long part2Work(string[] input)
    {
        throw new NotImplementedException();
    }

    protected override long part2ExampleExpected { get; }
    protected override long part2InputExpected { get; }
}
