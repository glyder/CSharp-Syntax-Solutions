public static class Record
{
    public static void Run()
    {
        // Records
        var personRecord = new PersonRecord("Jane", "Doe", 28);
        var updatedRecord = personRecord with { FirstName = "Janet" };
        
        Console.WriteLine($"Record => Person Record: {personRecord.FirstName} {personRecord.LastName}");
        Console.WriteLine($"Record => Updated Record: {updatedRecord.FirstName} {updatedRecord.LastName}");

        Console.WriteLine("");
    }

    public static void Basic()
    {
        decimal result = new MoneyClass {
            Amount = 500,
            Currency = "Dollar"
        }.DoubleMoney(500);

        Console.WriteLine("");
    }

    public record MoneyRecord(decimal Amount, string Currency);
}

public class MoneyClass
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public decimal DoubleMoney(decimal amount) => amount * 2;
}