
public static class Ranges
{
    public static void Run()
    {
        //Ranges: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges
        ReadOnlySpan<char> input = "Hello World-";
        Console.Out.WriteLine(input.TrimEnd('-'));
        // or use an indexer to always remove the last character:


        int y = 2; Console.Out.WriteLine(input[^y]);   // --y from last char
        //numbers[^y] is the same with numbers[length - y]

        int i = 2; Console.Out.WriteLine(input[..^i]); // trim by i 

    }
}
