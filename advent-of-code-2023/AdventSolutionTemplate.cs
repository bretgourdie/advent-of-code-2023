using NUnit.Framework;

namespace advent_of_code_2017;

public abstract class AdventSolutionTemplate<TPart1, TPart2>
{
    private const string example = "example";
    private const string input = "input";

    [SetUp]
    public void Setup()
    {
        fileCheckAssertions();
    }

    private void fileCheckAssertions()
    {
        Assert.That(File.Exists(getFilename(example)), "Example file does not exist");
        Assert.That(File.Exists(getFilename(input)), "Input file does not exist");
    }

    [Test]
    [TestCase(example)]
    [TestCase(input)]
    public void Part1(string file) =>
        Assert.That(() =>
        part(
            file,
            part1Work,
            part1ExampleExpected,
            part1InputExpected),
        Throws.Nothing);


    [Test]
    [TestCase(example)]
    [TestCase(input)]
    public void Part2(string file) =>
        Assert.That(() =>
        part(
            file,
            part2Work,
            part2ExampleExpected,
            part2InputExpected),
        Throws.Nothing);

    protected abstract TPart1 part1Work(string[] input);

    protected abstract TPart1 part1ExampleExpected { get; }
    protected abstract TPart1 part1InputExpected { get; }

    protected abstract TPart2 part2Work(string[] input);
    protected abstract TPart2 part2ExampleExpected { get; }
    protected abstract TPart2 part2InputExpected { get; }

    private void part<T>(
        string file,
        Func<string[], T> workMethod,
        T exampleExpected,
        T inputExpected)
    {
        var answer = workMethod.Invoke(getInput(file));

        switch (file)
        {
            case example:
                assertions(exampleExpected, answer);
                break;
            case input:
                assertions(inputExpected, answer);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void assertions<T>(
        T expected,
        T actual)
    {
        if (expected == null) throw new ArgumentNullException(nameof(expected));
        Assert.That(expected.Equals(actual), $"Expected {expected}; actual {actual}");
    }

    private string[] getInput(string file)
    {
        return File.ReadAllLines(
            getFilename(file)
        );
    }

    private string getFilename(string file)
    {
        return
            this.GetType().Name
            + Path.DirectorySeparatorChar
            + file
            + ".txt";
    }
}
