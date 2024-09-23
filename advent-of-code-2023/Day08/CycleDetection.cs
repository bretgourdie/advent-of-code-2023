namespace advent_of_code_2023.Day08;
internal class CycleDetection
{
    public bool CycleDetected = false;
    public bool CycleFound = false;
    private readonly ISet<Instance> _instancesHash;
    private readonly List<Instance> _instancesList;
    public int Length { get; private set; }

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
            _instancesList.Add(instance);
        }

        else
        {
            string start = null;
            int ii = _instancesList.Count - 1;
            int zEnd = -1;
            do
            {
                start = _instancesList[--ii].from;
                if (start.EndsWith("Z"))
                {
                    zEnd = Math.Max(zEnd, ii);
                }
            } while (from != start);

            int length = _instancesList.Count - ii + 1;
            int actualZ = length + zEnd;
            Length = actualZ;
        }
    }

    private record Instance(string from, string left, string right, int directionIndex);
}
