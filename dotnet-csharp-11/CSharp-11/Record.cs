public static class Record
{
    public static void Run()
    {
        // Records
        var personRecord = new PersonRecord("Jane", "Doe");
        var updatedRecord = personRecord with { FirstName = "Janet" };
        
        Console.WriteLine($"Record => Person Record: {personRecord.FirstName} {personRecord.LastName}");
        Console.WriteLine($"Record => Updated Record: {updatedRecord.FirstName} {updatedRecord.LastName}");

        Console.WriteLine("");
    }
}

