namespace advent_of_code_2023.Day09;
internal class Forward : IDirection
{
    public long AddNumber(IList<long> numbers, long newNumber)
    {
        var next = numbers.Last() + newNumber;
        numbers.Add(next);
        return next;
    }
}
