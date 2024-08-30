namespace advent_of_code_2023.Day02;
internal interface ICubeBag
{
    void HandleCubes(string color, int count);
    long GetAnswer();
}
