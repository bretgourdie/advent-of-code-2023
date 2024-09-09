namespace advent_of_code_2023.Day04;
internal interface IScorable
{
    void RecordScore(long cardNumber, long score);
    long GetScore();
}
