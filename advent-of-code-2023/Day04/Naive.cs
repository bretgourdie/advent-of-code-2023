namespace advent_of_code_2023.Day04;
internal class Naive : IScorable
{
    private long sum = 0;

    public void RecordScore(long cardNumber, long score)
    {
        sum += (long)Math.Pow(2, score - 1);
    }

    public long GetScore() => sum;
}
