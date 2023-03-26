
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using CommandLine.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

[MemoryDiagnoser]
public class SpanOfT
{
    //October 21, 2015,
    //October 26, 1985
    //November 5, 1955
    private readonly string _dateAsText = "05 11 1955";

    //Value type on the class level, cannot allocated on the heap,
    //along with it's parent when this is instantiated,
    //compiler won't let you:
    //  Error CS8345  Field or auto-implemented property cannot be
    //  of type 'ReadOnlySpan<char>' unless it is an instance member
    //  of a ref struct.
    //private ReadOnlySpan<char> _dateAsText2 = "21 11 1955";

    public void Run()
    {
        var date1 = DateWithSubstringOnHeap();
        var date2 = DateWithSpanOnStack();
        Console.WriteLine(date2);

        //TODO: ?
        //https://www.codemag.com/Article/2207031/Writing-High-Performance-Code-Using-SpanT-and-MemoryT-in-C
    }

    [Benchmark]
    public (int day, int month, int year) DateWithSubstringOnHeap()
    {
        var dayAsText = _dateAsText.Substring(0, 2);
        var monthAsText = _dateAsText.Substring(3, 2);
        var yearAsText = _dateAsText.Substring(6);

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
        //return (26, 10, 1985);
    }

    [Benchmark]
    public (int day, int month, int year) DateWithSpanOnStack()
    {
        //Guaranteed to be on the stack
        ReadOnlySpan<char> dateAsSpan = _dateAsText;

        var dayAsText = dateAsSpan.Slice(0, 2);
        var monthAsText = dateAsSpan.Slice(3, 2);
        var yearAsText = dateAsSpan.Slice(6);

        var day = int.Parse(dayAsText);
        var month = int.Parse(monthAsText);
        var year = int.Parse(yearAsText);

        return (day, month, year);
    }

    //===========================================================

    private DateTime[] dates;

    [GlobalSetup]
    public void SpanVsString()
    {
        // Populate the array with some sample dates
        dates = new DateTime[1000000];
        for (int i = 0; i < dates.Length; i++)
        {
            dates[i] = DateTime.Now.AddDays(i);
        }
        Console.SetOut(TextWriter.Null);
    }


    [GlobalCleanup]
    public void Cleanup()
    {
        // Restore the standard output stream
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
    }

    [Benchmark]
    public void SpanArray()
    {
        int startIndex = 500000;
        int count = 100000;

        ReadOnlySpan<DateTime> span = new ReadOnlySpan<DateTime>(dates, startIndex, count);

        DateTime earliestDate = DateTime.MaxValue;
        DateTime latestDate = DateTime.MinValue;
        for (int i = 0; i < span.Length; i++)
        {
            DateTime date = span[i];

            if (date < earliestDate)
            {
                earliestDate = date;
            }

            if (date > latestDate)
            {
                latestDate = date;
            }
        }

        TimeSpan difference = latestDate - earliestDate;
        Console.WriteLine($"ReadOnlySpan: The difference between the earliest and latest dates is {difference.TotalDays} days.");
    }

    [Benchmark]
    public void StringArray()
    {
        int startIndex = 500000;
        int count = 100000;

        DateTime[] subset = new DateTime[count];
        Array.Copy(dates, startIndex, subset, 0, count);

        DateTime earliestDate = DateTime.MaxValue;
        DateTime latestDate = DateTime.MinValue;

        for (int i = 0; i < subset.Length; i++)
        {
            DateTime date = subset[i];

            if (date < earliestDate)
            {
                earliestDate = date;
            }

            if (date > latestDate)
            {
                latestDate = date;
            }
        }

        TimeSpan difference = latestDate - earliestDate;
        Console.WriteLine($"New array: The difference between the earliest and latest dates is {difference.TotalDays} days.");
    }

    //=======================================================

    [Benchmark]
    public void ArrayList()
    {
        int[] arr = new[] { 0, 1, 2, 3 };
        Span<int> intSpan = arr;

        var otherSpan = arr.AsSpan();
    }

    [Benchmark]
    public void CollectionsList()
    {
        List<int> intList = new() { 0, 1, 2, 3 };
        var listSpan = CollectionsMarshal.AsSpan(intList);
    }

}


//https://code-maze.com/csharp-span-to-improve-application-performance/
//Candidate:
//  Substring => Slice

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class StringSpanBenchmark
{
    string _hamletText = File.ReadAllText("./Files/hamletActOne.txt");

    [GlobalSetup]
    public void Setup() { }

    [GlobalCleanup]
    public void Cleanup() { }


    // string func
    [Benchmark]
    public void ParseWithString()
    {
        var indexPrev = 0;
        var indexCurrent = 0;
        var rowNum = 0;

        foreach (char c in _hamletText)
        {
            if (c == '\n')
            {
                indexCurrent += 1;

                var line = _hamletText.Substring(indexPrev, indexCurrent - indexPrev);
                if (line.Equals(Environment.NewLine))
                    rowNum++;

                indexPrev = indexCurrent;

                continue;
            }

            indexCurrent++;
        }
    }

    // span func
    [Benchmark]
    public void ParseWithSpan()
    {
        var hamletSpan = _hamletText.AsSpan();

        var indexPrev = 0;
        var indexCurrent = 0;
        var rowNum = 0;

        foreach (char c in hamletSpan)
        {
            if (c == '\n')
            {
                indexCurrent += 1;

                var slice = hamletSpan.Slice(indexPrev, indexCurrent - indexPrev);
                if (slice.Equals(Environment.NewLine, StringComparison.OrdinalIgnoreCase))
                    rowNum++;

                indexPrev = indexCurrent;

                continue;
            }

            indexCurrent++;
        }
    }

    //=====================================================

}

[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser]
public class NameParserBenchmarks
{
    private const string FullName = "Steve J Gordon";
    private static readonly NameParser Parser = new NameParser();

    [Benchmark(Baseline = true)]
    public void GetLastName()
    {
        Parser.GetLastName(FullName);
    }

    [Benchmark]
    public void Substring()
    {
        Parser.Substring(FullName);
    }

    [Benchmark]
    public void Span()
    {
        Parser.Span(FullName);
    }
}

public class NameParser
{
    public string GetLastName(string fullName)
    {
        var names = fullName.Split(" ");

        var lastName = names.LastOrDefault();

        return lastName ?? string.Empty;
    }

    public string Substring(string fullName)
    {
        var lastSpaceIndex = 
            fullName.LastIndexOf(" ", StringComparison.Ordinal);

        return lastSpaceIndex == -1
            ? string.Empty
            : fullName.Substring(lastSpaceIndex + 1);
    }

    public ReadOnlySpan<char> Span(ReadOnlySpan<char> fullName)
    {
        var lastSpaceIndex = 
            fullName.LastIndexOf(" ", StringComparison.Ordinal);

        return lastSpaceIndex == -1
            ? ReadOnlySpan<char>.Empty
            : fullName.Slice(lastSpaceIndex + 1);
    }

}