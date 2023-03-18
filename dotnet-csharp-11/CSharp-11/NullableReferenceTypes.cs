
public static class NullableReferenceTypes
{
    public static void Run()
    {
        // Nullable Reference Types
        string? nullableString = null;

        Console.WriteLine($"NullableReferenceTypes => Nullable String: {nullableString?.Length}");
        Console.WriteLine();
    }
}
