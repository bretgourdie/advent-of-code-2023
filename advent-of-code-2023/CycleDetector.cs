namespace advent_of_code_2023;
internal class CycleDetector
{
    private IDictionary<string, long> stateAtIteration;
    private long cycleLength = -1;
    public bool CollisionDetected = false;

    public CycleDetector()
    {
        stateAtIteration = new Dictionary<string, long>();
    }

    public SaveResult SaveState(string state, long iterationAtState)
    {
        if (stateAtIteration.TryGetValue(state, out long cycleStart))
        {
            cycleLength = iterationAtState - cycleStart;
            CollisionDetected = true;
            return SaveResult.Collision;
        }

        stateAtIteration.Add(state, iterationAtState);
        return SaveResult.Successful;
    }

    public long GetLastIteration(long iteration, long total)
    {
        if (cycleLength <= 0) throw new Exception($"Cycle length: {cycleLength}");
        while (iteration + cycleLength < total)
        {
            iteration += cycleLength;
        }

        return iteration;
    }

    public enum SaveResult
    {
        Successful,
        Collision
    }
}
