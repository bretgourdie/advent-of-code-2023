namespace advent_of_code_2023.Day08;
internal class CycleDetection
{
    private bool cycleDetected = false;
    public bool ZInCycleDetected = false;
    private readonly ISet<Instance> _instancesHash;
    private readonly List<Instance> _instancesList;
    public int Length { get; private set; }
    public int Start { get; private set; }

    public CycleDetection()
    {
        _instancesHash = new HashSet<Instance>();
        _instancesList = new List<Instance>();
    }

    public void Record(
        string from,
        string left,
        string right,
        int directionIndex)
    {
        var instance = new Instance(from, left, right, directionIndex);

        if (!_instancesHash.Contains(instance))
        {
            _instancesHash.Add(instance);
        }

        else
        {
            cycleDetected = true;
        }

        if (cycleDetected && !ZInCycleDetected)
        {
            if (from.EndsWith("Z"))
            {
                ZInCycleDetected = true;
                Start = _instancesList.Count - 1;

                for (; Start >= 0; Start--)
                {
                    if (_instancesList[Start] == instance) break;
                    Length += 1;
                }
            }
        }

        _instancesList.Add(instance);
    }

    private record Instance(string from, string left, string right, int directionIndex);
}
