
public static class PatternMatching
{
    public static void Run()
    {
        // Pattern Matching
        First();
        GetMessage();

        Console.WriteLine("");
    }

    public static void First()
    {
        object obj = 123;
        if (obj is string s)
        {
            Console.WriteLine($"PatternMatching => Object is a string: {s}");
        }
        else if (obj is int i)
        {
            Console.WriteLine($"PatternMatching => Object is an int: {i}");
        }
    }

    public static void GetMessage()
    {
        Console.WriteLine("PatternMatching => GetMessage()");
        Console.WriteLine("-------------------------------");
        Console.WriteLine(GetMessage("Hello, world!")); // Hello, world! is too long
        Console.WriteLine(GetMessage("Hello")); // Hello is okay
        Console.WriteLine(GetMessage(42)); // 42 is positive
        Console.WriteLine(GetMessage(-1)); // -1 is zero or negative
        Console.WriteLine(GetMessage(null)); // Unknown object

        static string GetMessage(object obj) => obj switch
        {
            string s when s.Length > 10 => $"{s} is too long",
            string s => $"{s} is okay",
            int i when i > 0 => $"{i} is positive",
            int i => $"{i} is zero or negative",
            _ => "Unknown object"
        };
    }

}
