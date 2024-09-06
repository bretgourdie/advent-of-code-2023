namespace advent_of_code_2023.Day04;
internal interface IScorable
{
    void RecordScore(string cardNumber, long score);
    long GetScore();
}
