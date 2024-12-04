namespace advent_of_code_2023.Day15;
internal class Day15 : AdventSolution
{
    private const string removeCommand = "-";
    private const string placeCommand = "=";

    protected override long part1Work(string[] input)
    {
        long total = 0;
        var split = input.First().Split(',');

        foreach (var token in split)
        {
            total += hash(token);
        }

        return total;
    }

    private int hash(string toHash)
    {
        int currentValue = 0;

        foreach (var letter in toHash)
        {
            var code = (int)letter;
            currentValue += code;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
    }

    private IDictionary<int, IList<Lens>> shuffleLenses(string[] input)
    {
        var boxes = new Dictionary<int, IList<Lens>>();

        foreach (var token in input)
        {
            performOperation(token, boxes);
        }

        return boxes;
    }

    private void performOperation(
        string token,
        IDictionary<int, IList<Lens>> boxes)
    {
        var command = getCommand(token);
        var label = getLabel(token, command);

        var box = hash(label);

        if (command == removeCommand)
        {
            handleRemove(label, box, boxes);
        }

        if (command == placeCommand)
        {
            var focalLength = getFocalLength(token, command);
            handlePlace(label, box, focalLength, boxes);
        }
    }

    private string getLabel(string token, string command)
    {
        return token.Substring(0, token.IndexOf(command));
    }

    private string getCommand(string token)
    {
        if (token.Contains(removeCommand)) return removeCommand;
        if (token.Contains(placeCommand)) return placeCommand;

        throw new ArgumentException("Command not found", nameof(token));
    }

    private int getFocalLength(string token, string command)
    {
        return int.Parse(token.Substring(token.IndexOf(command) + 1));
    }

    private void handleRemove(
        string label,
        int box,
        IDictionary<int, IList<Lens>> boxes)
    {
        if (!boxes.ContainsKey(box)) return;

        var existingLens = boxes[box].Where(x => x.Label == label);

        if (existingLens.Any())
        {
            var single = existingLens.Single();
            boxes[box].Remove(single);
        }
    }

    private void handlePlace(
        string label,
        int box,
        int focalLength,
        IDictionary<int, IList<Lens>> boxes)
    {
        if (!boxes.ContainsKey(box))
        {
            boxes[box] = new List<Lens>();
        }

        var lens = new Lens(label, focalLength);

        var existingLens = boxes[box].Where(x => x.Label == lens.Label);

        if (existingLens.Any())
        {
            var single = existingLens.Single();
            var index = boxes[box].IndexOf(single);
            boxes[box][index] = lens;
        }

        else
        {
            boxes[box].Add(
                new Lens(label, focalLength)
            );
        }
    }

    protected override long part1ExampleExpected => 1320;
    protected override long part1InputExpected => 506269;
    protected override long part2Work(string[] input)
    {
        var split = input.First().Split(',');
        var shuffled = shuffleLenses(split);

        var result = calculateResult(shuffled);
        return result;
    }

    private long calculateResult(IDictionary<int, IList<Lens>> boxes)
    {
        long sum = 0;

        foreach (var box in boxes)
        {
            var boxNumber = box.Key;
            var lenses = box.Value;

            long boxNumberPortion = 1 + box.Key;

            for (int ii = 0; ii < lenses.Count; ii++)
            {
                long lensSlotPortion = ii + 1;
                long focalLengthPortion = lenses[ii].Size;

                var total = boxNumberPortion * lensSlotPortion * focalLengthPortion;
                sum += total;
            }
        }

        return sum;
    }

    protected override long part2ExampleExpected => 145;
    protected override long part2InputExpected => 264021;

    private struct Lens
    {
        public string Label;
        public int Size;

        public Lens(string label, int size)
        {
            Label = label;
            Size = size;
        }
    }
}
