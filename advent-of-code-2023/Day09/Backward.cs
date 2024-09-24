namespace advent_of_code_2023.Day09;
internal class Backward : IDirection
{
    public long AddNumber(IList<long> numbers, long newNumber)
    {
        var previous = numbers.First() - newNumber;
        numbers.Insert(0, previous);
        return previous;
    }
}
