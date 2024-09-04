using System.Reflection;
using NUnit.Framework;

namespace advent_of_code_2017;

public abstract class AdventSolutionTemplate<TPart1, TPart2>
{
    private const string example = "example";
    private const string input = "input";

    private Assembly executingAssembly;
    private IList<string> manifestResourceNames;
    private string currentNamespace;

    [SetUp]
    public void SetUp()
    {
        executingAssembly = Assembly.GetExecutingAssembly();
        manifestResourceNames = executingAssembly.GetManifestResourceNames();
        currentNamespace = this.GetType().Namespace;

        Assert.That(currentNamespace, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    [TestCase(example)]
    [TestCase(input)]
    public void Part1(string file)
    {
        Assert.That(embeddedFileExists(file), $"{file} embedded file does not exist");

        Assert.That(() =>
        part(
            file,
            part1Work,
            part1ExampleExpected,
            part1InputExpected),
        Throws.Nothing);
    }


    [Test]
    [TestCase(example)]
    [TestCase(input)]
    public void Part2(string file)
    {
        Assert.That(embeddedFileExists(file), $"{file} embedded file does not exist");

        Assert.That(() =>
        part(
            file,
            part2Work,
            part2ExampleExpected,
            part2InputExpected),
        Throws.Nothing);
    }

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
        var stream = executingAssembly.GetManifestResourceStream(getEmbeddedFilename(file));
        Assert.That(stream, Is.Not.Null);

        var lines = new List<string>();
        using (var reader = new StreamReader(stream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        return lines.ToArray();
    }

    private string getEmbeddedFilename(string file)
    {
        return String.Concat(
            currentNamespace,
            ".",
            file,
            ".txt");
    }

    private bool embeddedFileExists(string file) =>
        manifestResourceNames.Contains(getEmbeddedFilename(file));
}
